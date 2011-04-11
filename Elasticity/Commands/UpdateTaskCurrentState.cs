using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticity.Commands
{
    public class UpdateTaskCurrentState : Command
    {
        public UpdateTaskCurrentState(Guid jobId, Guid taskId, SchedulerTaskState currentState)
        {
            this.JobId = jobId;
            this.TaskId = taskId;
            this.CurrentState = currentState;
        }

        public Guid JobId { get; protected set; }
        public Guid TaskId { get; protected set; }
        public SchedulerTaskState CurrentState { get; protected set; }
    }
}
