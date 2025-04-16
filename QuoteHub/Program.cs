using QuoteHub.Core;
using Serilog;
using Serilog.Templates;
using Serilog.Events;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using QuoteHub.Core.Database;
using System.Text.Json.Serialization;
using QuoteHub.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new LowerCaseControllerRouteConvention());
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB connection setup
string connectionString = builder.Configuration.GetConnectionString("DBConnection") ?? string.Empty;
//Console.WriteLine($"Connection string: {connectionString}");

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    );
});

// Add service collection
builder.Services.AddServiceCollection();

// Logging service Serilog
builder.Logging.AddSerilog();
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console( // Add this line to log to console
        new ExpressionTemplate(
            "{ {Timestamp: @t, Level: @l, TracebackId: @tr, RequestMethod, RequestPath, StatusCode, SourceContext, Data:{EventId: @i, RequestId}, Message: @m, Exception: @x} }\n\n"
        )
    )
    .WriteTo.File(
    new ExpressionTemplate(
        "{ {Timestamp: @t, Level: @l, TracebackId: @tr, RequestMethod, RequestPath, StatusCode, SourceContext, Data:{EventId: @i, RequestId}, Message: @m, Exception: @x} }\n\n"
    ),
    path: "logs/log-.ndjson",
    rollingInterval: RollingInterval.Day,
    restrictedToMinimumLevel: LogEventLevel.Information
    ).CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();

public class LowerCaseControllerRouteConvention : IApplicationModelConvention
{
    public void Apply(ApplicationModel application)
    {
        foreach (ControllerModel controller in application.Controllers)
        {
            string controllerName = controller.ControllerName.ToLower();
            foreach (SelectorModel selector in controller.Selectors)
            {
                if (selector.AttributeRouteModel?.Template != null)
                {
                    selector.AttributeRouteModel.Template = selector.AttributeRouteModel.Template.Replace("[controller]", controllerName);
                }
            }
        }
    }
}