using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Elasticity.Domain;
using Elasticity.Events;

namespace Elasticity
{
    public class SchedulerServiceBuilder
    {
        public SchedulerServiceBuilder()
        {

        }

        public IEventAggregator EventAggregator { get; set; }
        public EventStore EventStore { get; set; }
        public IRepository<SchedulerJob> JobRepository { get; set; }

        public static implicit operator SchedulerService(SchedulerServiceBuilder builder)
        {
            return new SchedulerService(builder.EventAggregator, builder.JobRepository);
        }

        public SchedulerServiceBuilder WithEventAggregator(IEventAggregator eventAggregator)
        {
            this.EventAggregator = eventAggregator;
            return this;
        }

        public SchedulerServiceBuilder WithEventStore(EventStore eventStore)
        {
            this.EventStore = eventStore;
            return this;
        }

        public SchedulerServiceBuilder WithJobRepository(IRepository<SchedulerJob> repository)
        {
            this.JobRepository = repository;
            return this;
        }
    }
}
