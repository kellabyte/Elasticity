using Elasticity.Commands;
using Elasticity.Domain;
using Elasticity.Events;

namespace Elasticity.CommandHandlers
{
    public class SchedulerJobCommandHandlers : 
        IHandle<EnqueueJob>,
        IHandle<DisableJob>,
        IHandle<ActivateJob>
    {
        private IRepository<SchedulerJob> repository = null;

        public SchedulerJobCommandHandlers(IRepository<SchedulerJob> repository)
        {
            this.repository = repository;
        }

        public void Handle(EnqueueJob command)
        {
            SchedulerJob job = new SchedulerJob(command.JobId, command.Tasks);
            repository.Save(job);
        }

        public void Handle(DisableJob command)
        {
            SchedulerJob job = repository.GetById(command.JobId);
            job.Disable();
            repository.Save(job);
        }

        public void Handle(ActivateJob command)
        {
            SchedulerJob job = repository.GetById(command.JobId);
            job.Activate();
            repository.Save(job);
        }
    }
}
