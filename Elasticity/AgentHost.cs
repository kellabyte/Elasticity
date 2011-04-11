using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;

namespace Elasticity
{
    public class AgentHost : ServiceHostBase
    {
        public AgentHost(Agent agent, AgentHostConfiguration configuration)
            : base(agent)
        {
            this.Configuration = configuration;
            this.Agent = agent;
        }

        public AgentHost(Agent agent, Action<AgentHostConfigurator> closure)
            : base(agent)
        {
            this.Agent = agent;

            AgentHostConfiguration configuration = new AgentHostConfiguration();
            AgentHostConfigurator configurator = new AgentHostConfigurator(configuration);

            closure.Invoke(configurator);

            this.Configuration = configuration;
        }

        public AgentHostConfiguration Configuration { get; private set; }
        public Agent Agent { get; private set; }
    }
}
