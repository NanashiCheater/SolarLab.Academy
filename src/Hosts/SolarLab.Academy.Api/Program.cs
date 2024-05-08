using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SolarLab.Academy.Api.Controllers;
using SolarLab.Academy.AppServices.Categories.Repositories;
using SolarLab.Academy.AppServices.Categories.Services;
using SolarLab.Academy.AppServices.Files.Repositories;
using SolarLab.Academy.AppServices.Files.Services;
using SolarLab.Academy.AppServices.Users.Repositories;
using SolarLab.Academy.AppServices.Users.Services;
using SolarLab.Academy.AppServices.Validators;
using SolarLab.Academy.ComponentRegistrar;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.DataAccess;
using SolarLab.Academy.DataAccess.Categories.Repository;
using SolarLab.Academy.DataAccess.Files.Repository;
using SolarLab.Academy.DataAccess.User.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolarLab.Academy.AppServices.Anouncements.Services;
using SolarLab.Academy.AppServices.Anouncements.Repositories;
using SolarLab.Academy.DataAccess.Announcements.Repository;
using SolarLab.Academy.Domain.Users.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Humanizer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using SolarLab.Academy.AppServices.Comments.Services;
using SolarLab.Academy.AppServices.Comments.Repositories;
using SolarLab.Academy.DataAccess.Comments.Repository;
using Microsoft.CodeAnalysis.Options;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



builder.Services.AddScoped<DbContext>(s => s.GetRequiredService<ApplicationDbContext>());

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddServices();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IAnnouncementService, AnnouncementService>();
builder.Services.AddTransient<IAnnouncementImageService, AnnouncementImageService>();
builder.Services.AddTransient<IUserRoleService, UserRoleService>();
builder.Services.AddTransient<ICommentService, CommentService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
builder.Services.AddScoped<IAnnouncementImageRepository, AnnouncementImageRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName.Replace("+", "_"));
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Мой первый API", Version = "V1" });
    options.IncludeXmlComments(Path.Combine(Path.Combine(AppContext.BaseDirectory,
        $"{typeof(UserController).Assembly.GetName().Name}.xml")));
    options.IncludeXmlComments(Path.Combine(Path.Combine(AppContext.BaseDirectory,
        $"{typeof(UserDto).Assembly.GetName().Name}.xml")));

    
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddAuthorization(options => options.DefaultPolicy =
    new AuthorizationPolicyBuilder
            (JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddFluentValidationAutoValidation(o => o.DisableDataAnnotationsValidation = true);
builder.Services.AddValidatorsFromAssembly(typeof(CreateUserValidator).Assembly);

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
    options.User.RequireUniqueEmail = true;

});

builder.Services.AddSerilog((services, config) =>
{
    config.ReadFrom.Configuration(builder.Configuration)
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .WriteTo.Console()
        .WriteTo.Debug();
});


builder.Services.AddMemoryCache();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "local";
});




var app = builder.Build();      


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseAuthentication();
app.UseAuthorization();
app.UseCors("cors");
app.MapControllers();

app.Run();

public partial class Program {}