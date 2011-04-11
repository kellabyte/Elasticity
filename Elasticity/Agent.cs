using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Messaging;
using System.ServiceModel;
using System.Text;

using Elasticity.Domain;

namespace Elasticity
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public abstract class Agent : IAgentTaskRequestQueue
    {
        public event EventHandler<SchedulerTaskEventArgs> TaskSubmitted;

        public Agent()
        {
        }
       
        public void Submit(ISchedulerTask task)
        {
            OnTaskSubmitted(this, task);
        }

        protected virtual void OnTaskSubmitted(object sender, ISchedulerTask task)
        {
            if (TaskSubmitted != null)
            {
                TaskSubmitted(sender, new SchedulerTaskEventArgs(task));
            }
        }
    }
}
