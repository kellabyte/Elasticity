using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticity.Events
{
    public class InMemoryEventStore : EventStore
    {
        private struct EventDescriptor
        {
            public readonly Event EventData;
            public readonly Guid Id;
            public readonly int Version;

            public EventDescriptor(Guid id, Event eventData, int version)
            {
                EventData = eventData;
                Version = version;
                Id = id;
            }
        }

        public InMemoryEventStore(IEventAggregator publisher)
            : base(publisher)
        {
        }

        private readonly Dictionary<Guid, List<EventDescriptor>> current = new Dictionary<Guid, List<EventDescriptor>>();

        public override List<Event> GetEventsForAggregate(Guid aggregateId)
        {
            List<EventDescriptor> eventDescriptors;
            if (!current.TryGetValue(aggregateId, out eventDescriptors))
            {
                throw new AggregateNotFoundException();
            }
            return eventDescriptors.Select(desc => desc.EventData).ToList();
        }

        protected override int GetVersionForAggregate(Guid aggregateId)
        {
            List<EventDescriptor> eventDescriptors;
            if (!current.TryGetValue(aggregateId, out eventDescriptors))
            {
                return 0;
            }
            else
            {
                return eventDescriptors[eventDescriptors.Count - 1].Version;
            }
        }

        protected override void SaveEvent(Guid aggregateId, Event evt, int version)
        {
            List<EventDescriptor> eventDescriptors;
            if (!current.TryGetValue(aggregateId, out eventDescriptors))
            {
                eventDescriptors = new List<EventDescriptor>();
                current.Add(aggregateId, eventDescriptors);
            }
            eventDescriptors.Add(new EventDescriptor(aggregateId, evt, version));
        }
    }
}
