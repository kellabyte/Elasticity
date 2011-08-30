using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Elasticity.Events;

namespace Elasticity.Domain
{    
    public class SchedulerTask : Entity<Guid>, ISchedulerTask
    {
        public SchedulerTask()
            : this(Guid.NewGuid(), SchedulerTaskState.Disabled, SchedulerTaskState.Disabled, DateTimeOffset.MaxValue)
        {
            
        }

        public SchedulerTask(Guid id, SchedulerTaskState currentState, SchedulerTaskState desiredState, 
            DateTimeOffset lockedUntil)
            : base(id)
        {
            this.CurrentState = currentState;
            this.DesiredState = desiredState;
            this.LockedUntil = lockedUntil;
        }

        public SchedulerTaskState CurrentState { get; private set; }
        public SchedulerTaskState DesiredState { get; private set; }
        public DateTimeOffset LockedUntil { get; private set; }

        public void ActivateCurrent()
        {
            this.CurrentState = SchedulerTaskState.Active;
        }

        public void DisableCurrent()
        {
            this.CurrentState = SchedulerTaskState.Disabled;
        }

        public void ActivateDesired()
        {
            this.DesiredState = SchedulerTaskState.Active;
        }

        public void DisableDesired()
        {
            this.DesiredState = SchedulerTaskState.Disabled;
        }

        public void UpdateLockedUntil(DateTimeOffset dateTime)
        {
            this.LockedUntil = dateTime;
        }
    }

    public class SchedulerTask<T> : SchedulerTask
    {
        public SchedulerTask(Guid id, SchedulerTaskState currentState, SchedulerTaskState desiredState, 
            DateTimeOffset lockedUntil, T content)
            : base(id, currentState, desiredState, lockedUntil)
        {
            this.Content = content;
        }

        public T Content { get; protected set; }

        public static implicit operator T(SchedulerTask<T> that)
        {
            return that.Content;
        }
    }
}
