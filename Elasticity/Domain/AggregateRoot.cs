using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Elasticity.Events;
using Elasticity.Extensions;

namespace Elasticity.Domain
{
    public abstract class AggregateRoot
    {
        private readonly List<Event> changes = new List<Event>();

        public Guid Id { get; protected set; }
        public int Version { get; internal set; }

        public IEnumerable<Event> GetUncommittedChanges()
        {
            return changes;
        }

        public void MarkChangesAsCommitted()
        {
            changes.Clear();
        }

        public void LoadsFromHistory(IEnumerable<Event> history)
        {
            foreach (var e in history)
            {
                Apply(e, false);
            }
        }

        protected void Apply(Event evt)
        {
            Apply(evt, true);
        }

        private void Apply(Event evt, bool isNew)
        {
            evt.Version = GetNewEventVersion();
            this.AsDynamic().Handle(evt);
            if (isNew)
            {
                changes.Add(evt);
            }
        }

        private int GetNewEventVersion()
        {
            return ++Version;
        }
    }
}
