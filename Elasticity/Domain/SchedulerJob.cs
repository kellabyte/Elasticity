using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Elasticity.Events;

namespace Elasticity.Domain
{
    public class SchedulerJob : AggregateRoot
    {
        public SchedulerJob()
        {
            // Used to create from the repository.
        }

        public SchedulerJob(Guid id, List<ISchedulerTask> tasks)
        {
            if (tasks == null)
            {
                tasks = new List<ISchedulerTask>();
            }
            base.Apply(new JobCreated(id, tasks));
        }

        public SchedulerJobState CurrentState { get; private set; }
        public List<ISchedulerTask> Tasks { get; private set; }

        private void Handle(JobCreated evt)
        {
            this.Id = evt.JobId;
            this.Tasks = evt.Tasks;
        }

        public void Disable()
        {
            base.Apply(new JobDisabled(this.Id));
        }

        private void Handle(JobDisabled evt)
        {
            this.CurrentState = SchedulerJobState.Disabled;
        }

        public void Activate()
        {
            base.Apply(new JobActivated(this.Id));
        }

        private void Handle(JobActivated evt)
        {
            this.CurrentState = SchedulerJobState.Active;
        }
    }
}
