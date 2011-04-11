using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticity.Events
{
    public class TaskAddedToJob : Event
    {
        public TaskAddedToJob(Guid jobID, Guid taskId, SchedulerTaskState currentState,
            SchedulerTaskState desiredState, DateTimeOffset lockedUntil)
        {
            this.JobId = jobID;
            this.TaskId = taskId;
            this.CurrentState = currentState;
            this.DesiredState = desiredState;
            this.LockedUntil = lockedUntil;
        }

        public Guid JobId { get; private set; }
        public Guid TaskId { get; private set; }
        public SchedulerTaskState CurrentState { get; private set; }
        public SchedulerTaskState DesiredState { get; private set; }
        public DateTimeOffset LockedUntil { get; private set; }
    }
}
