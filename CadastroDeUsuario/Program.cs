using CadastroDeUsuario.Authorization;
using CadastroDeUsuario.Data;
using CadastroDeUsuario.Models;
using CadastroDeUsuario.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var ConectionString = builder.Configuration["ConnectionStrings:CreateCountUser"];
builder.Services.AddDbContext<UserContext>(options => options.UseMySql(ConectionString, ServerVersion.AutoDetect(ConectionString)));

// Add auto mapper 
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Configuration of Identity
builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<UserContext>()
    .AddDefaultTokenProviders();

// token services

// Add scoped
// user serveces
builder.Services
    .AddScoped<UserServices>();
// user ->
// Token services
builder.Services
    .AddScoped<TokenServices>();

// validando a idade com authorization 
builder.Services.AddScoped<IAuthorizationHandler, AuthorizationOldHandeler>();

builder.Services.AddAuthentication(options => { options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; })
    .AddJwtBearer(options => { options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters 
    { 
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey"])),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

// Add controllers
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization(options => options.AddPolicy("AuthorizationBirthday", policy =>
{
    // Autorizar so quando o usario tiver 18 anos
    policy.AddRequirements(new AuthorizationOld(18));
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
