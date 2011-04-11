using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticity.Commands
{
    public abstract class UpdateTaskContent : Command
    {
        public UpdateTaskContent(Guid jobId, Guid taskId)
        {
            this.JobId = jobId;
            this.TaskId = taskId;
        }

        public Guid JobId { get; protected set; }
        public Guid TaskId { get; protected set; }
        public object Content { get; protected set; }
    }

    public class UpdateTaskContent<T> : UpdateTaskContent, IContent
    {
        public UpdateTaskContent(Guid jobId, Guid taskId, T content)
            : base(jobId, taskId)
        {
            this.Content = content;
        }

        public new T Content 
        { 
            get
            {
                return (T)base.Content;
            }
            protected set
            {
                base.Content = (T)value;
            }
        }
    }
}
