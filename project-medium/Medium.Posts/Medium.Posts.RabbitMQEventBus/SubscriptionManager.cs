using System;
using System.Collections.Generic;
using System.Linq;

namespace Medium.Posts.RabbitMQEventBus
{
    public class SubscriptionManager : ISubscriptionManager
    {
        public readonly IList<Type> _registeredEvents;

        public readonly IDictionary<string, IList<Type>> _registeredHandler;

        public SubscriptionManager()
        {
            _registeredEvents = new List<Type>();
            _registeredHandler = new Dictionary<string, IList<Type>>();
        }


        public Type GetEventByName(string eventName)
        {
            return _registeredEvents.First(e => e.Name == eventName);
        }

        public IEnumerable<Type> GetHandlerByEventName(string eventName)
        {
            return _registeredHandler[eventName];
        }

        public bool HasEvent(string eventName)
        {
            return _registeredEvents.Any(e => e.Name == eventName);
        }

        public void RegisterHandler(Type @event, Type handler)
        {
            var eventName = @event.Name;

            if (!_registeredEvents.Contains(@event)) {
                _registeredEvents.Add(@event);
            }

            if (!_registeredHandler.ContainsKey(eventName))
            {
                _registeredHandler[eventName] = new List<Type>();
            }

            if (!_registeredHandler[eventName].Contains(handler)) {
                _registeredHandler[eventName].Add(handler);
            }
        }
    }
}
