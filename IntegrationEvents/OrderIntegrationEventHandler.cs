using Jiangyi.EventBus.Abstractions;

namespace Jiangyi.Test;

public class OrderIntegrationEventHandler1 : IIntegrationEventHandler<OrderIntegrationEvent>
{
    public async Task Handle(OrderIntegrationEvent @event)
    {
        await Task.Delay(2000);
        Console.WriteLine("{0} hanlder1: {1}", DateTime.Now, @event.OrderId);
    }
}

public class OrderIntegrationEventHandler2 : IIntegrationEventHandler<OrderIntegrationEvent>
{
    public async Task Handle(OrderIntegrationEvent @event)
    {
        await Task.Delay(3000);
        Console.WriteLine("{0} hanlder2: {1}", DateTime.Now, @event.OrderId);
    }
}