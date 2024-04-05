using IzmirGunesAPI.Persistence;
using IzmirGunesAPI.Application;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using Serilog.Core;
using Microsoft.AspNetCore.HttpLogging;
using IzmirGunesAPI.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddPersistanceServices();
builder.Services.AddApplicationServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//cors hatasý için eklendi 
builder.Services.AddCors(options =>
options.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:4200", "https://localhost:4200", "http://127.0.0.1:4200", "https://127.0.0.1:4200").AllowAnyHeader().AllowAnyMethod()
));
builder.Services.AddSwaggerGen();
//serilog'u devreye alýyoruz
// Kullanýcý adýný al
 

Logger log = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("SQLConnection"), "TBLS4INLOG", autoCreateSqlTable: true,
    columnOptions: new ColumnOptions
    {
        Store = new Collection<StandardColumn>
            {
                StandardColumn.Id,
                StandardColumn.MessageTemplate,
                StandardColumn.Level,
                StandardColumn.TimeStamp,
                StandardColumn.Exception,
                StandardColumn.LogEvent
               
                // Diðer sütunlarý eklemeye devam edebilirsiniz, istemediðiniz sütunlarý çýkararak
            }
    })

      .MinimumLevel.Information()
    .CreateLogger();
builder.Host.UseSerilog(log);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});
// app.UseHttpLogging(); eklendi  yapýlan requestleri log ile takip ediyorz 
//serilog'u devreye alýyoruz 

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());//global exception
app.UseSerilogRequestLogging();//eklendiði satýrdan sonra loglamaya baþlar 
app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
