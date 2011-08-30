using Elasticity.Domain;

namespace Elasticity.Domain
{
    public interface ISchedulerService
    {
        void EnqueueJob(SchedulerJob job);
        void DisableJob(SchedulerJob job);
        void ActivateJob(SchedulerJob job);
    }
}