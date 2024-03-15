
using AlmoxarifadoSmart.API;
using AlmoxarifadoSmart.Application.Scrapers;
using AlmoxarifadoSmart.Application.Services.Implementations.Log;
using AlmoxarifadoSmart.Application.Services.Implemetations;
using AlmoxarifadoSmart.Application.Services.Implemetations.Comunicacao.Email;
using AlmoxarifadoSmart.Application.Services.Implemetations.Comunicacao.Whatsapp;
using AlmoxarifadoSmart.Application.Services.Implemetations.ProdutosServices;
using AlmoxarifadoSmart.Application.Services.Interfaces;
using AlmoxarifadoSmart.Core.Repositories;
using AlmoxarifadoSmart.Infrastructure.Persistence.Repositories;
using AlmoxarifadoSmart.Infrastructure.Repositories;
using AlmoxarifadoSmart.Infrastructure.Scrapers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<db_almoxarifadoContext>(ServiceLifetime.Transient);

// Services
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IProdutoProcessor, ProdutoProcessor>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<IReportWhatsappService, ReportWhatsappService>();
builder.Services.AddScoped<IReportEmailService, ReportEmailService>();




// Scraper 
builder.Services.AddScoped<IScraperMagazineLuiza, ScraperMagazineLuiza>();
builder.Services.AddScoped<IScraperMercadoLivre, ScraperMercadoLivre>();

// Repository
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<IBenchmarkingRepository, BenchmarkingRepository>();
builder.Services.AddScoped<IProdutoScraperRepository, ProdutoScraperRepository>();
builder.Services.AddScoped<IStoreProdutoRepository, StoreProdutoRepository>();


builder.Services.AddControllers().AddNewtonsoftJson(x =>
 x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


//builder.Services.AddCors(options =>
//{
//    var corsOrigins = builder.Configuration.GetSection("CorsOrigins").Get<string[]>();

//    options.AddDefaultPolicy(builder =>
//    {
//        foreach (var origin in corsOrigins)
//        {
//            builder.WithOrigins(origin);
//        }

//        builder.AllowAnyMethod()
//               .AllowAnyHeader();
//    });
//});


var app = builder.Build();




    app.UseSwagger();
    app.UseSwaggerUI();



app.UseCors(policy =>
{


    policy.WithOrigins("*", "http://3.145.53.73:*")
          .AllowAnyMethod()
          .AllowAnyOrigin()
          .AllowAnyHeader().WithExposedHeaders("*");
});
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
