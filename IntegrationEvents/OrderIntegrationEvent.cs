using Jiangyi.EventBus.Events;

namespace Jiangyi.Test;

public record OrderIntegrationEvent : IntegrationEvent
{
    public int OrderId { get; }

    public OrderIntegrationEvent(int orderId)
    {
        OrderId = orderId;
    }
}