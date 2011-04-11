using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticity
{
    public class AgentHostConfigurator
    {
        private readonly AgentHostConfiguration configuration = null;

        public AgentHostConfigurator(AgentHostConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string TaskRequestQueue
        {
            set { configuration.TaskRequestQueue = value; }
        }
    }
}
