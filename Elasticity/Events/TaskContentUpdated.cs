using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticity.Events
{
    public class TaskContentUpdated : Event
    {
        public TaskContentUpdated(Guid taskId, object content)
        {
            this.TaskId = taskId;
            this.Content = content;
        }

        public Guid TaskId { get; private set; }
        public object Content { get; private set; }
    }
}
