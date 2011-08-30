using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Elasticity.Domain;
using Elasticity.Events;

namespace Elasticity.Domain
{
    public class SchedulerService : 
        IHandle<JobCreated>,
        IHandle<JobActivated>,
        IHandle<JobDisabled>
    {
        private IEventAggregator eventAggregator;
        private IRepository<SchedulerJob> jobRepository;
        private List<SchedulerJob> jobs;
        
        public SchedulerService(IEventAggregator eventAggregator, IRepository<SchedulerJob> jobRepository)
        {
            this.eventAggregator = eventAggregator;
            this.jobRepository = jobRepository;
            this.eventAggregator.Subscribe(this);
            this.jobs = new List<SchedulerJob>();
        }

        public void Handle(JobCreated message)
        {
            SchedulerJob job = jobRepository.GetById(message.JobId);
            lock (jobs)
            {
                jobs.Add(job);
            }
        }

        public void Handle(JobActivated message)
        {
            // TODO: Not sure if I need to handle this or not yet.
        }

        public void Handle(JobDisabled message)
        {
            List<SchedulerJob> jobs = new List<SchedulerJob>(this.jobs);
            SchedulerJob job = jobs.Where(x => x.Id == message.JobId).SingleOrDefault();
            
            lock (jobs)
            {
                if (job != null)
                {
                    jobs.Remove(job);
                }
            }
        }
    }
}
