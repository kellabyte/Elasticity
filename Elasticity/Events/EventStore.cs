using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticity.Events
{
    public abstract class EventStore : IEventStore
    {
        private readonly IEventAggregator publisher;
        
        public EventStore(IEventAggregator publisher)
        {
            this.publisher = publisher;
        }

        public void SaveEvents(Guid aggregateId, IEnumerable<Event> events, int expectedVersion)
        {
            int aggregateVersion = GetVersionForAggregate(aggregateId);
            if (aggregateVersion == 0)
            {

            }
            else if (aggregateVersion != expectedVersion && expectedVersion != -1)
            {
                throw new ConcurrencyException();
            }

            var i = expectedVersion;
            foreach (var evt in events)
            {
                i++;
                evt.Version = i;
                SaveEvent(aggregateId, evt, i);
                publisher.Publish(evt);
            }
        }

        public abstract List<Event> GetEventsForAggregate(Guid aggregateId);
        protected abstract int GetVersionForAggregate(Guid aggregateId);
        protected abstract void SaveEvent(Guid aggregateId, Event evt, int version);
    }
}
