using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MuniBarva.COMMON;
using MuniBarva.COMMON.Interfaces;
using MuniBarva.DAO;
using MuniBarva.DAO.Interfaces;
using MuniBarva.SERVICES;
using MuniBarva.SERVICES.Interfaces;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Municipalidad de Barva", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    Array.Empty<string>()
                }
            });
});

//Registro de todas las dependencias
builder.Services.AddScoped<ILoginDao, LoginDao>();
builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddScoped<IEmployeesService, EmployeesService>();
builder.Services.AddScoped<IEmployeesDAO, EmployeesDAO>();

builder.Services.AddScoped<IEncrypt, Encrypt>();

builder.Services.AddScoped<ISendEmail, SendEmail>();

builder.Services.AddScoped<ISettingsDao, SettingsDao>();
builder.Services.AddScoped<ISettingsService, SettingsService>();

builder.Services.AddScoped<IConfigKey, ConfigKey>();

builder.Services.AddScoped<IPermsDAO, PermsDAO>();
builder.Services.AddScoped<IPermsService, PermsService>();

builder.Services.AddScoped<IMenuDao, MenusDAO>();
builder.Services.AddScoped<IMenuService, MenusService>();

builder.Services.AddScoped<IRoleDAO, RolsDAO>();
builder.Services.AddScoped<IRolsService, RolsService>();

builder.Services.AddSingleton<MasterDao>();

//MANEJOMDO JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:KEY"].ToString())),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

//Manejo del cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MUNIBARVA", policy => { policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsEnvironment(environment))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseCors("MUNIBARVA");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
