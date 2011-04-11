using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticity.Events
{
    public class TaskCurrentStateUpdated : Event
    {
        public TaskCurrentStateUpdated(Guid taskId, SchedulerTaskState state)
        {
            this.TaskId = taskId;
            this.State = state;
        }

        public Guid TaskId { get; private set; }
        public SchedulerTaskState State { get; private set; }
    }
}
