using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Elasticity.Events;

namespace Elasticity.Domain
{
    public class SchedulerJob : AggregateRoot
    {
        public List<ISchedulerTask> Tasks { get; private set; }

        public SchedulerJob()
	    {
            // Used to create from the repository.
	    }

        public SchedulerJob(Guid id, List<ISchedulerTask> tasks)
        {
            if (tasks == null)
            {
                tasks = new List<ISchedulerTask>();
            }
            base.Apply(new JobCreated(id, tasks));
        }

        private void Handle(JobCreated evt)
        {
            this.Id = evt.JobId;
            this.Tasks = evt.Tasks;
        }

        public void AddTaskToJob(Guid taskId, SchedulerTaskState currentState,
            SchedulerTaskState desiredState, DateTimeOffset lockedUntil)
        {
            base.Apply(new TaskAddedToJob(this.Id, taskId, currentState, desiredState, lockedUntil));
        }

        public void Handle(TaskAddedToJob evt)
        {
            this.Tasks.Add(new SchedulerTask(evt.TaskId, evt.CurrentState, evt.DesiredState, evt.LockedUntil));
        }

        public void UpdateTaskCurrentState(Guid id, SchedulerTaskState state)
        {
            base.Apply(new TaskCurrentStateUpdated(id, state));
        }

        private void Handle(TaskCurrentStateUpdated evt)
        {
            ISchedulerTask task = Tasks.SingleOrDefault(t => t.Id == evt.TaskId);
            if (task != null)
            {
                task.Apply(evt);
            }
        }

        public void UpdateDesiredState(Guid id, SchedulerTaskState state)
        {
            base.Apply(new TaskDesiredStateUpdated(id, state));
        }

        private void Handle(TaskDesiredStateUpdated evt)
        {
            ISchedulerTask task = Tasks.SingleOrDefault(t => t.Id == evt.TaskId);
            if (task != null)
            {
                task.Apply(evt);
            }
        }

        public void UpdateLockedUntil(Guid id, DateTimeOffset dateTime)
        {
            base.Apply(new TaskLockedUntilUpdated(id, dateTime));
        }

        private void Handle(TaskLockedUntilUpdated evt)
        {
            ISchedulerTask task = Tasks.SingleOrDefault(t => t.Id == evt.TaskId);
            if (task != null)
            {
                task.Apply(evt);
            }
        }

        public void UpdateTaskContent(Guid taskId, object content)
        {
            base.Apply(new TaskContentUpdated(taskId, content));
        }

        public void Handle(TaskContentUpdated evt)
        {
            ISchedulerTask task = Tasks.SingleOrDefault(t => t.Id == evt.TaskId);
            if (task != null)
            {
                task.Apply(evt);
            }
        }
    }
}
