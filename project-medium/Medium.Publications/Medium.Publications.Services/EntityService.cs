using MediatR;
using Medium.Publications.Domain.Entities;

namespace Medium.Publications.Services
{
    public class EntityService<TEntity> where TEntity : Entity
    {
        private readonly IMediator _mediator;

        public EntityService(IMediator mediator)
        {
            this._mediator = mediator;
        }

        protected void PublishEvents(TEntity entity)
        {
            if (entity.Events == null)
            {
                return;
            }

            foreach (var @event in entity.Events)
            {
                _mediator.Publish(@event);
            }

            entity.ClearEvents();
        }
    }
}
