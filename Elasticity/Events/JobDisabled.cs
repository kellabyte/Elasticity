using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticity.Events
{
    public class JobDisabled : Event
    {
        public JobDisabled()
        {
        }

        public JobDisabled(Guid jobId)
        {
            this.JobId = jobId;
        }

        public Guid JobId { get; set; }
    }
}
