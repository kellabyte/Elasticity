using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Elasticity.Domain;

namespace Elasticity
{
    public class AgentClient : ClientBase, IAgentTaskRequestQueue
    {
        public event EventHandler<SchedulerTaskEventArgs> TaskSubmitted;

        private IAgentTaskRequestQueue taskRequestQueue = null;

        public AgentClient()
        {
            taskRequestQueue = ClientBase.CreateClient<IAgentTaskRequestQueue>(ClientBase.GetDefaultEndPointAddress("TaskRequestQueue"));
        }

        public void Submit(ISchedulerTask task)
        {
            taskRequestQueue.Submit(task);
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
