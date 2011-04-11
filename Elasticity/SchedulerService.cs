using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Elasticity.Domain;
using Elasticity.Events;

namespace Elasticity
{
    public class SchedulerService
    {
        private IEventAggregator eventAggregator;
        private IRepository<SchedulerJob> jobRepository;
        
        // TODO: Should include an EventStore in here too.
        public SchedulerService(IEventAggregator eventAggregator, IRepository<SchedulerJob> jobRepository)
        {
            this.eventAggregator = eventAggregator;
            this.jobRepository = jobRepository;
        }
    }
}
