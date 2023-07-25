using CadastroDeUsuario.Data;
using CadastroDeUsuario.Models;
using CadastroDeUsuario.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var ConectionString = builder.Configuration.GetConnectionString("CreateCountUser");
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

// Add controllers
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
