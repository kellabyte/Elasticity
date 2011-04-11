using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticity.Events
{
    public class EventStore : IEventStore
    {
        //private readonly IEventPublisher publisher;
        private readonly IEventAggregator publisher;

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

        public EventStore(IEventAggregator publisher)
        {
            this.publisher = publisher;
        }

        private readonly Dictionary<Guid, List<EventDescriptor>> current = new Dictionary<Guid, List<EventDescriptor>>();

        public void SaveEvents(Guid aggregateId, IEnumerable<Event> events, int expectedVersion)
        {
            List<EventDescriptor> eventDescriptors;
            if (!current.TryGetValue(aggregateId, out eventDescriptors))
            {
                eventDescriptors = new List<EventDescriptor>();
                current.Add(aggregateId, eventDescriptors);
            }
            else if (eventDescriptors[eventDescriptors.Count - 1].Version != expectedVersion && expectedVersion != -1)
            {
                throw new ConcurrencyException();
            }

            var i = expectedVersion;
            foreach (var evt in events)
            {
                i++;
                evt.Version = i;
                eventDescriptors.Add(new EventDescriptor(aggregateId, evt, i));

                publisher.Publish<Event>(evt);
            }
        }

        public List<Event> GetEventsForAggregate(Guid aggregateId)
        {
            List<EventDescriptor> eventDescriptors;
            if (!current.TryGetValue(aggregateId, out eventDescriptors))
            {
                throw new AggregateNotFoundException();
            }
            return eventDescriptors.Select(desc => desc.EventData).ToList();
        }
    }
}
