using InStudio.Data.Models;
using InStudio.Extensions;
using InStudio.Services.Repositories.Interfaces;
using InStudio.Services.Repositories;
using InStudio.Services.Services.Interfaces;
using InStudio.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using InStudio.Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.BearerScheme;
});
builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
        .AddCookie(IdentityConstants.ApplicationScheme, options =>
        {
            options.Events.OnSigningIn = async context =>
            {
                var userPrincipal = context.Principal;
                var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();

                var user = await userManager.GetUserAsync(userPrincipal);
                if (user != null)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    var claims = roles.Select(role => new Claim(ClaimTypes.Role, role));
                    var identity = (ClaimsIdentity)userPrincipal.Identity;
                    identity.AddClaims(claims);
                }
            };
        }).AddBearerToken(IdentityConstants.BearerScheme, options =>
        {
            options.Events.OnMessageReceived = async context =>
            {
                // Extract token from Authorization header (Bearer)
                var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
                var user = await userManager.FindByIdAsync("userId"); 
                var isValid = await userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultProvider, "CustomPurpose", token);

                if (isValid)
                {
                    // If the token is valid, authenticate the request
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                    var identity = new ClaimsIdentity(claims, IdentityConstants.BearerScheme);
                    context.Principal = new ClaimsPrincipal(identity);
                }
            };
        });


builder.Services.AddIdentityCore<User>(options =>
{
    options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
    options.ClaimsIdentity.UserNameClaimType = ClaimTypes.Name;
    options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
})
    .AddRoles<IdentityRole>()
    .AddSignInManager<SignInManager<User>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddApiEndpoints();

builder.Services.AddScoped<UserManager<User>>();
builder.Services.AddScoped<SignInManager<User>>();
builder.Services.AddScoped<RoleManager<IdentityRole>>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));



builder.Services.AddControllers(options =>
{
    options.Filters.Add(new IgnoreAntiforgeryTokenAttribute());
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});

builder.Services.AddScoped<IDesignCategoryService, DesignCategoryService>();
builder.Services.AddScoped<IDesignCategoryRepository, DesignCategoryRepository>();
builder.Services.AddScoped<IUserSubscriptionTypeRepository, UserSubscriptionTypeRepository>();
builder.Services.AddScoped<IUserSubscriptionTypeService, UserSubscriptionTypeService>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please enter token as: Bearer {token}",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.MapControllers();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapIdentityApi<User>();

app.Run();