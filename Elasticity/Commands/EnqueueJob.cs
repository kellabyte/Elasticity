using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Elasticity.Domain;

namespace Elasticity.Commands
{
    public class EnqueueJob : Command
    {
        public EnqueueJob(Guid jobId, List<ISchedulerTask> tasks)
        {
            this.JobId = jobId;
            this.Tasks = tasks;
        }

        public Guid JobId { get; protected set; }
        public List<ISchedulerTask> Tasks { get; private set; }
    }
}
