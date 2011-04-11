using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Elasticity.Domain;

namespace Elasticity
{
    public class SchedulerJobRequestEventArgs : EventArgs
    {
        public SchedulerJobRequestEventArgs(SchedulerJob job)
        {
            this.Job = job;
        }

        public SchedulerJob Job { get; set; }
    }
}
