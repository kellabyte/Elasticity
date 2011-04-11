using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Elasticity.Events;

namespace Elasticity.Domain
{
    public interface ISchedulerTask
    {
        Guid Id { get; }
        SchedulerTaskState CurrentState { get; }
        SchedulerTaskState DesiredState { get; }
        DateTimeOffset LockedUntil { get; }

        //void UpdateCurrentState(SchedulerTaskState state);
        //void UpdateDesiredState(SchedulerTaskState state);
        //void UpdateLockedUntil(DateTimeOffset dateTime);

        void Apply(Event evt);
    }
}
