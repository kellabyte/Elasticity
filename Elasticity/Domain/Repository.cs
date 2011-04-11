using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Elasticity.Events;

namespace Elasticity.Domain
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot, new() //shortcut you can do as you see fit with new()
    {
        private readonly IEventStore storage;

        public Repository(IEventStore storage)
        {
            this.storage = storage;
        }

        public void Save(AggregateRoot aggregate, int expectedVersion)
        {
            storage.SaveEvents(aggregate.Id, aggregate.GetUncommittedChanges(), expectedVersion);
        }

        public T GetById(Guid id)
        {
            var obj = new T(); //lots of ways to do this
            var e = storage.GetEventsForAggregate(id);
            obj.LoadsFromHistory(e);
            return obj;
        }
    }
}
