using AutoMapper;
using Dev.Bussines.Interfaces;
using Dev.Bussines.Notificacoes;
using Dev.Bussines.Service;
using Dev.Data.Context;
using Dev.Data.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RestApi.Configuration;
using RestApi.Data;
using RestApi.Extensions;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connection));
builder.Services.AddDbContext<MeuDbContext>(x => x.UseSqlServer(connection));
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddErrorDescriber<IdentityMesagePortugues>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var appSettigns = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettigns);

var appsSettings = appSettigns.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appsSettings.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = appsSettings.ValidoEm,
        ValidIssuer = appsSettings.Emissor

    };
});



builder.Services.Configure<ApiBehaviorOptions>(x => x.SuppressModelStateInvalidFilter = true);

builder.Services.AddAutoMapper(typeof(Program));
ConfigurarInjecaoDeDependencia(builder);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
static WebApplicationBuilder ConfigurarInjecaoDeDependencia(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<IFornecedorRepository, FornecedorRepository>();
    builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
    builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

    //builder.Services.AddScoped<IUSer,aspNetUser>
    builder.Services.AddScoped<INotificador, Notificador>();
    builder.Services.AddScoped<IFornecedorService, FornecedorService>();
    builder.Services.AddScoped<IProdutoService, ProdutoService>();
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    builder.Services.AddScoped<IUSer, AspNetUser>();
    return builder;
}

