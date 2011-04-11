using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticity.Commands
{
    public class CreateTask : Command
    {
        public CreateTask(Guid id, int originalVersion, SchedulerTaskState currentState, SchedulerTaskState desiredState,
            DateTimeOffset lockedUntil)
        {
            this.Id = id;
            this.OriginalVersion = originalVersion;
            this.CurrentState = currentState;
            this.DesiredState = desiredState;
            this.LockedUntil = lockedUntil;
        }

        public Guid Id { get; protected set; }
        public SchedulerTaskState CurrentState { get; private set; }
        public SchedulerTaskState DesiredState { get; private set; }
        public DateTimeOffset LockedUntil { get; private set; }
    }
    
    public class CreateSchedulerTask<T> : CreateTask
    {
        public CreateSchedulerTask(Guid id, int originalVersion, SchedulerTaskState currentState, 
            SchedulerTaskState desiredState, DateTimeOffset lockedUntil, T content)
            : base(id, originalVersion, currentState, desiredState, lockedUntil)
        {
            this.Content = content;
        }

        public T Content { get; protected set; }
    }
}
