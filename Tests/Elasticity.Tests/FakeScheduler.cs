using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Elasticity.Domain;

namespace Elasticity.Tests
{
    public class FakeScheduler : Scheduler
    {
        public override void Submit(SchedulerJob job)
        {
            base.Submit(job);
        }

        public override void Respond(ISchedulerTask task)
        {
            base.Respond(task);
        }

    }
}
