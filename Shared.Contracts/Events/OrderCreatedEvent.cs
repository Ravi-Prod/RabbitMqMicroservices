namespace Shared.Contracts.Events
    {
    public record OrderCreatedEvent(Guid OrderId, string CustomerName, string Email, decimal Amount, DateTime CreatedOn);

    }
