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
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
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