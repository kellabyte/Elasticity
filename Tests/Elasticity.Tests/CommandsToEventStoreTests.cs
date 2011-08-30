using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

using Elasticity.Commands;
using Elasticity.CommandHandlers;
using Elasticity.Domain;
using Elasticity.Events;

namespace Elasticity.Tests
{
    [TestClass]
    public class CommandsToEventStoreTests
    {
        private SchedulerJobCommandHandlers schedulerJobCommandHandlers;
        private IRepository<SchedulerJob> repository;
        private IEventStore eventStore;

        [TestInitialize()]
        public void Arrange()
        {
            // Arrange.
            eventStore = MockRepository.GenerateMock<IEventStore>();
            repository = new Repository<SchedulerJob>(eventStore);
            schedulerJobCommandHandlers = new SchedulerJobCommandHandlers(repository);
        }

        [TestMethod]
        public void HandleEnqueueJobCommandEventStoreSavesJob()
        {
            // Arrange
            List<ISchedulerTask> tasks = new List<ISchedulerTask>();
            ISchedulerTask task = MockRepository.GenerateMock<ISchedulerTask>();
            tasks.Add(task);

            EnqueueJob command = new EnqueueJob(Guid.NewGuid(), tasks);

            // Act
            schedulerJobCommandHandlers.Handle(command);

            // Assert
            eventStore.AssertWasCalled(x => x.SaveEvents(
                Arg<Guid>.Matches(s => s.Equals(command.JobId)),

                Arg<IEnumerable<Event>>.Matches(s => 
                    s.Count() == 1 && 
                    ((JobCreated)s.ElementAt(0)).JobId.Equals(command.JobId) &&
                    ((JobCreated)s.ElementAt(0)).Tasks.Equals(tasks)),

                Arg<int>.Matches(s => s == 1)
            ));
        }

        [TestMethod]
        public void HandleActivateJobCommandEventStoreSavesJob()
        {
            // Arrange
            List<ISchedulerTask> tasks = new List<ISchedulerTask>();
            ISchedulerTask task = MockRepository.GenerateMock<ISchedulerTask>();
            tasks.Add(task);            

            ActivateJob command = new ActivateJob(Guid.NewGuid());

            List<Event> events = new List<Event>() ;
            eventStore.Stub(x => x.GetEventsForAggregate(command.JobId)).Return(new List<Event>()
            {
                new JobCreated(command.JobId, tasks)
            });

            // Act
            schedulerJobCommandHandlers.Handle(command);

            // Assert
            eventStore.AssertWasCalled(x => x.SaveEvents(
                Arg<Guid>.Matches(s => s.Equals(command.JobId)),

                Arg<IEnumerable<Event>>.Matches(s =>
                    s.Count() == 1 &&
                    ((JobActivated)s.ElementAt(0)).JobId.Equals(command.JobId)),

                Arg<int>.Matches(s => s == 2)
            ));
        }

        [TestMethod]
        public void HandleDisableJobCommandEventStoreSavesJob()
        {
            // Arrange
            List<ISchedulerTask> tasks = new List<ISchedulerTask>();
            ISchedulerTask task = MockRepository.GenerateMock<ISchedulerTask>();
            tasks.Add(task);

            DisableJob command = new DisableJob(Guid.NewGuid());

            List<Event> events = new List<Event>();
            eventStore.Stub(x => x.GetEventsForAggregate(command.JobId)).Return(new List<Event>()
            {
                new JobCreated(command.JobId, tasks)
            });

            // Act
            schedulerJobCommandHandlers.Handle(command);

            // Assert
            eventStore.AssertWasCalled(x => x.SaveEvents(
                Arg<Guid>.Matches(s => s.Equals(command.JobId)),

                Arg<IEnumerable<Event>>.Matches(s =>
                    s.Count() == 1 &&
                    ((JobDisabled)s.ElementAt(0)).JobId.Equals(command.JobId)),

                Arg<int>.Matches(s => s == 2)
            ));
        }
    }
}
