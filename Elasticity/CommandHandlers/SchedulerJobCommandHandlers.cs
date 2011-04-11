using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Elasticity.Commands;
using Elasticity.Domain;
using Elasticity.Events;

namespace Elasticity.CommandHandlers
{
    public class SchedulerJobCommandHandlers : 
        IHandle<CreateJob>,
        IHandle<AddTaskToJob>
    {
        public SchedulerJobCommandHandlers(IRepository<SchedulerJob> repository)
        {
            this.Repository = repository;
        }

        public IRepository<SchedulerJob> Repository { get; protected set; }

        public void Handle(CreateJob message)
        {
            SchedulerJob job = new SchedulerJob(message.JobId, message.Tasks);
            Repository.Save(job, message.OriginalVersion);
        }

        public void Handle(AddTaskToJob command)
        {
            SchedulerJob job = Repository.GetById(command.JobId);
            if (job != null)
            {
                job.AddTaskToJob(command.TaskId, command.CurrentState, command.DesiredState, command.LockedUntil);
                Repository.Save(job, command.OriginalVersion);
            }
        }

        public void Handle(UpdateTaskCurrentState command)
        {
            SchedulerJob job = Repository.GetById(command.JobId);
            if (job != null)
            {
                job.UpdateTaskCurrentState(command.TaskId, command.CurrentState);
                Repository.Save(job, command.OriginalVersion);
            }
        }

        public void Handle(UpdateTaskDesiredState command)
        {
            SchedulerJob job = Repository.GetById(command.JobId);
            if (job != null)
            {
                job.UpdateDesiredState(command.TaskId, command.DesiredState);
                Repository.Save(job, command.OriginalVersion);
            }
        }

        public void Handle(UpdateTaskLockedUntil command)
        {
            SchedulerJob job = Repository.GetById(command.JobId);
            if (job != null)
            {
                job.UpdateLockedUntil(command.TaskId, command.LockedUntil);
                Repository.Save(job, command.OriginalVersion);
            }
        }

        public void Handle(UpdateTaskContent command)
        {
            SchedulerJob job = Repository.GetById(command.JobId);
            if (job != null)
            {
                job.UpdateTaskContent(command.TaskId, command.Content);
                Repository.Save(job, command.OriginalVersion);
            }
        }
    }
}
