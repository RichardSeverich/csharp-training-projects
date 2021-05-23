using Medium.Publications.Domain.Events;
using System.Collections.Generic;

namespace Medium.Publications.Domain.Entities
{
    public class Entity
    {
        private List<IDomainEvent> _events;

        public IReadOnlyCollection<IDomainEvent> Events => _events?.AsReadOnly();

        public void AddEvent(IDomainEvent @event)
        {
            _events ??= new List<IDomainEvent>();
            _events.Add(@event);
        }

        public void ClearEvents()
        {
            _events.Clear();
        }

    }
}
