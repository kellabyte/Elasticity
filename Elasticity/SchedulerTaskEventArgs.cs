using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Elasticity.Domain;

namespace Elasticity
{
    public class SchedulerTaskEventArgs : EventArgs
    {
        public ISchedulerTask Task { get; set; }

        public SchedulerTaskEventArgs(ISchedulerTask task)
        {
            this.Task = task;
        }
    }
}
