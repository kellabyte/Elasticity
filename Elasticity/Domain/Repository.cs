using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Elasticity.Events;

namespace Elasticity.Domain
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot, new()
    {
        private readonly IEventStore storage;

        public Repository(IEventStore storage)
        {
            this.storage = storage;
        }

        public void Save(AggregateRoot aggregate)
        {
            storage.SaveEvents(aggregate.Id, aggregate.GetUncommittedChanges(), aggregate.Version);
        }

        public T GetById(Guid id)
        {
            var obj = new T();
            var e = storage.GetEventsForAggregate(id);
            obj.LoadsFromHistory(e);
            return obj;
        }
    }
}
