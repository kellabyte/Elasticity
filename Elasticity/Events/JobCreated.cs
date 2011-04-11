using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Elasticity.Domain;

namespace Elasticity.Events
{
    public class JobCreated : Event
    {
        public JobCreated(Guid jobId, List<ISchedulerTask> tasks) 
        {
            this.JobId = jobId;
            this.Tasks = tasks;
        }

        public Guid JobId { get; private set; }
        public List<ISchedulerTask> Tasks { get; protected set; }
    }
}
