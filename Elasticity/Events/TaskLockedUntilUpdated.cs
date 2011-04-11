using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticity.Events
{
    public class TaskLockedUntilUpdated : Event
    {
        public TaskLockedUntilUpdated(Guid taskId, DateTimeOffset dateTime)
        {
            this.TaskId = taskId;
            this.LockedUntil = dateTime;
        }

        public Guid TaskId { get; private set; }
        public DateTimeOffset LockedUntil { get; private set; }
    }
}
