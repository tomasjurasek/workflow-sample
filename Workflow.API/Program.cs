using Workflow.API.Workflow;
using WorkflowCore.Interface;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddWorkflow(s => s.UseSqlServer("Server=DESKTOP-JRDGL5U\\SQLEXPRESS;Database=master;Integrated Security=true;Trusted_Connection=Yes;TrustServerCertificate=True", true, true)
                                   .UseSqlServerLocking("Server=DESKTOP-JRDGL5U\\SQLEXPRESS;Database=master;Integrated Security=true;Trusted_Connection=Yes;TrustServerCertificate=True"));


builder.Services.AddWorkflowMiddleware<PreWorkflowMetricsMiddleware>();
builder.Services.AddWorkflowMiddleware<PostWorkflowMetricsMiddleware>();


builder.Services.AddOpenTelemetry()
                    .WithMetrics(metrics => metrics
                        .ConfigureResource(s => s.AddService("CheckoutOrderService"))
                                .AddMeter("*")
                                .AddConsoleExporter());
    


var app = builder.Build();

var host = app.Services.GetService<IWorkflowHost>();
host.RegisterWorkflow<PostCheckoutOrderProcess, CheckoutOrderCompleted>();
host.Start();

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
