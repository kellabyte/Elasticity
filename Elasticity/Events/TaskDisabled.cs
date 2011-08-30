using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticity.Events
{
    public class TaskDisabled : Event
    {
        public TaskDisabled()
        {
        }

        public TaskDisabled(Guid taskId)
        {
            this.TaskId = taskId;
        }

        public Guid TaskId { get; set; }
    }
}
