using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticity.Commands
{
    public class UpdateTaskLockedUntil : Command
    {
        public UpdateTaskLockedUntil(Guid jobId, Guid taskId, DateTimeOffset lockedUntil)
        {
            this.JobId = jobId;
            this.TaskId = taskId;
            this.LockedUntil = lockedUntil;
        }

        public Guid JobId { get; protected set; }
        public Guid TaskId { get; protected set; }
        public DateTimeOffset LockedUntil { get; protected set; }
    }
}
