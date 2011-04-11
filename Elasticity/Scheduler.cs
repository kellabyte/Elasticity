using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Messaging;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;

using Elasticity.Domain;

namespace Elasticity
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public abstract class Scheduler : ISchedulerJobRequestQueue, ISchedulerTaskResponseQueue
    {
        public event EventHandler<SchedulerJobRequestEventArgs> JobSubmitted;
        public event EventHandler<SchedulerTaskEventArgs> TaskResponded;

        public Scheduler()
        {
        }
        
        public virtual void Submit(SchedulerJob job)
        {
            OnJobSubmitted(this, new SchedulerJobRequestEventArgs(job));
        }

        public virtual void Respond(ISchedulerTask task)
        {
            OnTaskResponded(this, new SchedulerTaskEventArgs(task));
        }

        protected virtual void OnJobSubmitted(object sender, SchedulerJobRequestEventArgs e)
        {
            if (JobSubmitted != null)
            {
                JobSubmitted(sender, e);
            }
        }

        protected virtual void OnTaskResponded(object sender, SchedulerTaskEventArgs e)
        {
            if (TaskResponded != null)
            {
                TaskResponded(sender, e);
            }
        }
    }
}
