using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticity
{
    public class SchedulerHostConfiguration
    {
        public SchedulerHostConfiguration()
        {

        }

        public SchedulerHostConfiguration(string jobRequestQueue, string taskResponseQueue)
        {
            this.JobRequestQueue = jobRequestQueue;
            this.TaskResponseQueue = taskResponseQueue;
        }

        public string JobRequestQueue { get; set; }
        public string TaskResponseQueue { get; set; }
    }
}
