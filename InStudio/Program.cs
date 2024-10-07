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
using InStudio.Common.Services;
using InStudio.Common.Services.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InStudio.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddScoped<IScopeContext, ScopeContext>();
builder.Services.AddScoped<IScopeContextSetter, ScopeContext>();
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
                var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
                var user = await userManager.FindByIdAsync("userId"); 
                var isValid = await userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultProvider, "CustomPurpose", token);
              
                var scopeContextSetter = context.HttpContext.RequestServices.GetRequiredService<IScopeContextSetter>();
                scopeContextSetter.SetUser(Guid.Parse(user.Id), user.UserName);
                if (isValid)
                {
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
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IProjectOfferRepository, ProjectOfferRepository>();
builder.Services.AddScoped<IProjectOfferService, ProjectOfferService>();
builder.Services.AddScoped<IProjectImageRepository, ProjectImageRepository>();
builder.Services.AddScoped<IProjectImageService, ProjectImageService>();
builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IUserProfileDesignCategoryRepository, UserProfileDesignCategoryRepository>();
builder.Services.AddScoped<IUserProfileDesignCategoryService, UserProfileDesignCategoryService>();



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
app.Lifetime.ApplicationStarted.Register(async () =>
{
    using var scope = app.Services.CreateScope();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    if (!await roleManager.RoleExistsAsync("User"))
    {
        await roleManager.CreateAsync(new IdentityRole("User"));
    }
    if (!await roleManager.RoleExistsAsync("Designer"))
    {
        await roleManager.CreateAsync(new IdentityRole("Designer"));
    }
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }
});


app.MapPost("/registerWithRole", async (UserManager<User> userManager, RoleManager<IdentityRole> roleManager, RegisterDto model) =>
{
    if (model == null || string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
    {
        return Results.BadRequest("Invalid registration details.");
    }

    var user = new User { UserName = model.Email, Email = model.Email };
    var result = await userManager.CreateAsync(user, model.Password);

    if (result.Succeeded)
    {
        var roleToAssign = model.Role switch
        {
            "Designer" => "Designer",
            "Admin" => "Admin",
            _ => "User"
        };

        var roleResult = await userManager.AddToRoleAsync(user, roleToAssign);

        if (!roleResult.Succeeded)
        {
            return Results.BadRequest("Failed to assign role.");
        }

        return Results.Ok("User registered and role assigned successfully.");
    }

    return Results.BadRequest(result.Errors);
});

app.Run();