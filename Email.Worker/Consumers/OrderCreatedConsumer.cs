using MassTransit;
using Shared.Contracts.Events;

namespace Email.Worker.Consumers
    {
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
        {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
            {
            var message = context.Message;
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("OrderCreatedEvent received");
            Console.WriteLine($"Order ID      : {message.OrderId}");
            Console.WriteLine($"Customer Name : {message.CustomerName}");
            Console.WriteLine($"Email         : {message.Email}");
            Console.WriteLine($"Amount        : {message.Amount}");
            Console.WriteLine($"Created At    : {message.CreatedOn}");
            Console.WriteLine("----------------------------------------");
            await Task.CompletedTask;
            }
        }
    }
