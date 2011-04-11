using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;

using Elasticity.Domain;

namespace Elasticity
{
    public class SchedulerClient : ClientBase, ISchedulerJobRequestQueue, ISchedulerTaskResponseQueue
    {
        public event EventHandler<SchedulerJobRequestEventArgs> JobSubmitted;
        public event EventHandler<SchedulerTaskEventArgs> TaskResponded;

        private ISchedulerJobRequestQueue jobRequestQueue = null;
        private ISchedulerTaskResponseQueue taskResponseQueue = null;

        public SchedulerClient()
        {
            jobRequestQueue = ClientBase.CreateClient<ISchedulerJobRequestQueue>(ClientBase.GetDefaultEndPointAddress("JobRequestQueue"));
            taskResponseQueue = ClientBase.CreateClient<ISchedulerTaskResponseQueue>(ClientBase.GetDefaultEndPointAddress("TaskResponseQueue"));
        }

        public void Submit(SchedulerJob job)
        {
            jobRequestQueue.Submit(job);
            OnJobSubmitted(this, new SchedulerJobRequestEventArgs(job));
        }

        public void Respond(ISchedulerTask task)
        {
            taskResponseQueue.Respond(task);
        }

        protected void OnJobSubmitted(object sender, SchedulerJobRequestEventArgs e)
        {
            if (JobSubmitted != null)
            {
                JobSubmitted(sender, e);
            }
        }

        protected void OnTaskResponded(object sender, SchedulerTaskEventArgs e)
        {
            if (TaskResponded != null)
            {
                TaskResponded(sender, e);
            }
        }
    }
}
