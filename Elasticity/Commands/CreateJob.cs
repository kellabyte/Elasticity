using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Elasticity.Domain;

namespace Elasticity.Commands
{
    public class CreateJob : Command
    {
        public CreateJob(Guid jobId)
            : this (jobId, null)
        {
        }

        public CreateJob(Guid jobId, List<ISchedulerTask> tasks)
        {
            this.JobId = jobId;
            this.Tasks = tasks;
        }

        public Guid JobId { get; protected set; }
        public List<ISchedulerTask> Tasks { get; private set; }
    }
}
