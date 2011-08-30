using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticity.Commands
{
    public class DisableJob
    {
        public DisableJob(Guid jobId)
        {
            this.JobId = jobId;
        }

        public Guid JobId { get; set; }
    }
}
