using MassTransit;
using Shared.Contracts.Events;

namespace Inventory.API.Consumer
    {
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
        {


        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
            {
            var message = context.Message;
            Console.WriteLine("========================================");
            Console.WriteLine("Inventory Service");
            Console.WriteLine("OrderCreatedEvent received");
            Console.WriteLine($"Order ID : {message.OrderId}");
            Console.WriteLine($"Amount   : {message.Amount}");
            Console.WriteLine("Inventory processing...");
            Console.WriteLine("Inventory reserved successfully");
            Console.WriteLine("========================================");
            await Task.CompletedTask;
            }
        }
    }
