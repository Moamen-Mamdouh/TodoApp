using FluentValidation;
using MediatR;
using NitajTodoApp.Application.Behaviors;
using NitajTodoApp.Configurations;
using NitajTodoApp.Middlewares;
using NLog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders().AddNLog("nlog.config");

builder.Services.AddAppServicesDIConfig();

builder.Services.AddMapsterConfig();

builder.Services.AddMediatR(cfg
    => cfg.RegisterServicesFromAssembly(NitajTodoApp.Application.AssemblyReference.Assembly));

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggerPipeLineBehavior<,>));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

builder.Services.AddValidatorsFromAssembly(
    NitajTodoApp.Application.AssemblyReference.Assembly,
    includeInternalTypes: true);

builder.Services.AddDbConfig();

builder.Services
    .AddControllers()
    .AddApplicationPart(NitajTodoApp.Presentation.AssemblyReference.Assembly);

builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandlerMiddleware>();

builder.Services.AddProblemDetails();

var app = builder.Build();

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
