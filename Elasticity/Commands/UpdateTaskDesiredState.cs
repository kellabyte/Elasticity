using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticity.Commands
{
    public class UpdateTaskDesiredState : Command
    {
        public UpdateTaskDesiredState(Guid jobId, Guid taskId, SchedulerTaskState desiredState)
        {
            this.JobId = jobId;
            this.TaskId = taskId;
            this.DesiredState = desiredState;
        }

        public Guid JobId { get; protected set; }
        public Guid TaskId { get; protected set; }
        public SchedulerTaskState DesiredState { get; protected set; }
    }
}
