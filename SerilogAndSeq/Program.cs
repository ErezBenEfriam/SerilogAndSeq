using Serilog;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = new ConfigurationBuilder() //for optionally reading configuration from appsettings.json
.AddJsonFile("appsettings.json").Build();


Log.Logger = new LoggerConfiguration() //exsit in appsettings.json too
          .WriteTo.Seq("http://localhost:5341/")  //Download seq from here https://datalust.co/Download
          .WriteTo.File("log.txt")
          .WriteTo.File(new JsonFormatter(), "log.json")
          .CreateLogger();



builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
