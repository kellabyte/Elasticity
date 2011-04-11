using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticity
{
    public class AgentHostConfiguration
    {
        public AgentHostConfiguration()
        {

        }

        public AgentHostConfiguration(string taskRequestQueue)
        {
            this.TaskRequestQueue = taskRequestQueue;
        }

        public string TaskRequestQueue { get; set; }
    }
}
