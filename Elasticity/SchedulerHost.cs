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
    public class SchedulerHost : ServiceHostBase
    {
        public SchedulerHost(Scheduler scheduler, SchedulerHostConfiguration configuration)
            : base(scheduler)
        {
            this.Configuration = configuration;
            this.Scheduler = scheduler;            
        }

        public SchedulerHost(Scheduler scheduler, Action<SchedulerHostConfigurator> closure) 
            : base(scheduler)
        {
            this.Scheduler = scheduler;

            SchedulerHostConfiguration configuration = new SchedulerHostConfiguration();
            SchedulerHostConfigurator configurator = new SchedulerHostConfigurator(configuration);

            closure.Invoke(configurator);

            this.Configuration = configuration;
        }
        
        public SchedulerHostConfiguration Configuration { get; private set; }
        public Scheduler Scheduler { get; private set; }
    }
}
