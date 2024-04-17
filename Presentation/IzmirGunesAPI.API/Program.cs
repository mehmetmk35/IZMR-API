using IzmirGunesAPI.API.Extensions;
using IzmirGunesAPI.Application;
using IzmirGunesAPI.Infrastructure;
using IzmirGunesAPI.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddPersistanceServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//cors hatasý için eklendi 
builder.Services.AddCors(options =>
options.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:4200", "https://localhost:4200", "http://127.0.0.1:4200", "https://127.0.0.1:4200").AllowAnyHeader().AllowAnyMethod()
));
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true, //Oluþturulacak token deðerini kimlerin/hangi originlerin/sitelerin kullanýcý belirlediðimiz deðerdir. -> www.bilmemne.com
            ValidateIssuer = true, //Oluþturulacak token deðerini kimin daðýttýný ifade edeceðimiz alandýr. -> www.myapi.com
            ValidateLifetime = true, //Oluþturulan token deðerinin süresini kontrol edecek olan doðrulamadýr.
            ValidateIssuerSigningKey = true, //Üretilecek token deðerinin uygulamamýza ait bir deðer olduðunu ifade eden suciry key verisinin doðrulanmasýdýr.

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false
        };
    });
//serilog'u devreye alýyoruz
// Kullanýcý adýný al


Logger log = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("NetsisSQLConnection"), "TBLS4INLOG", autoCreateSqlTable: true,
    columnOptions: new ColumnOptions
    {
        Store = new Collection<StandardColumn>
            {
                StandardColumn.Id,
                StandardColumn.MessageTemplate,
                StandardColumn.Level,
                StandardColumn.TimeStamp,
                StandardColumn.Exception,
                StandardColumn.LogEvent,

               
                // Diðer sütunlarý eklemeye devam edebilirsiniz, istemediðiniz sütunlarý çýkararak
            },
        AdditionalColumns = new Collection<SqlColumn>
            {
                new SqlColumn { ColumnName = "Username", DataType = SqlDbType.NVarChar, DataLength = 100 },
                new SqlColumn { ColumnName = "Company", DataType = SqlDbType.NVarChar, DataLength = 100 }
            },
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

app.UseAuthentication();
 
app.MapControllers();
app.Use(async (context, next) =>
{
    //var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
    var username = context;
    //LogContext.PushProperty("user_name", username);
    await next();
});
app.Run();
