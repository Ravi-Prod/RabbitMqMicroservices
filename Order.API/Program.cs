
using MassTransit;
using Order.API.Models;
using Shared.Contracts.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddMassTransit(config =>
{
    config.SetKebabCaseEndpointNameFormatter();

    config.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(builder.Configuration["RabbitMQ:HostName"], h =>
        {
            h.Username(builder.Configuration["RabbitMQ:Username"] ?? throw new InvalidOperationException("Configuration value 'RabbitMQ:Username' is required."));
            h.Password(builder.Configuration["RabbitMQ:Password"] ?? throw new InvalidOperationException("Configuration value 'RabbitMQ:Password' is required."));
        });

        configurator.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));

    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//    {
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapPost("/Orders", async (CreateOrderRequest request, IPublishEndpoint publishEndpoint) =>
{
    var orderId = Guid.NewGuid();
    var orderCreatedEvent = new OrderCreatedEvent(
        orderId,
        request.CustomerName,
        request.Email,
        request.Amount,
        DateTime.UtcNow
        );
    await publishEndpoint.Publish(orderCreatedEvent);
    return Results.Ok(new
        {
        Message = "order created successfully",
        OrderId = orderId
        });
});

app.Run();
