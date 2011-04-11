using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticity.Commands
{
    public abstract class AddTaskToJob : Command, IContent
    {
        public AddTaskToJob(int originalVersion, Guid jobID, Guid taskId, SchedulerTaskState currentState, 
            SchedulerTaskState desiredState, DateTimeOffset lockedUntil)
        {
            this.OriginalVersion = originalVersion;
            this.JobId = jobID;
            this.TaskId = taskId;
            this.CurrentState = currentState;
            this.DesiredState = desiredState;
            this.LockedUntil = lockedUntil;
        }

        public int OriginalVersion { get; private set; }
        public Guid JobId { get; private set; }
        public Guid TaskId { get; private set; }
        public SchedulerTaskState CurrentState { get; private set; }
        public SchedulerTaskState DesiredState { get; private set; }
        public DateTimeOffset LockedUntil { get; private set; }

        public object Content { get; protected set; }
    }

    public class AddTaskToJob<T> : AddTaskToJob
    {
        public AddTaskToJob(int originalVersion, Guid jobID, Guid taskId, SchedulerTaskState currentState, 
            SchedulerTaskState desiredState, DateTimeOffset lockedUntil, T content)
            : base(originalVersion, jobID, taskId, currentState, desiredState, lockedUntil)
        {
            this.Content = content;
        }

        public new T Content 
        {
            get
            {
                return (T)base.Content;
            }
            private set
            {
                base.Content = value;
            }
        }
    }
}
