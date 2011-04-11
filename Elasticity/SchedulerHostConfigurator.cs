using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticity
{
    public class SchedulerHostConfigurator
    {
        private readonly SchedulerHostConfiguration configuration = null;

        public SchedulerHostConfigurator(SchedulerHostConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string JobRequestQueue
        {
            set { configuration.JobRequestQueue = value; }
        }

        public string TaskResponseQueue
        {
            set { configuration.TaskResponseQueue = value; }
        }
    }
}
