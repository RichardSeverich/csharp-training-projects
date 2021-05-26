using System;
using System.Collections.Generic;

namespace Medium.Posts.RabbitMQEventBus
{
    public interface ISubscriptionManager
    {
        Type GetEventByName(string eventName);

        void RegisterHandler(Type @event, Type handler);

        IEnumerable<Type> GetHandlerByEventName(string eventName);

        bool HasEvent(string eventName);
    }
}
