using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Elasticity.Commands;
using Elasticity.CommandHandlers;
using Elasticity.Domain;
using Elasticity.Events;

namespace Elasticity.Tests
{
    [TestClass]
    public class DomainTests
    {
        public class FakeDatabase :
            IHandle<JobCreated>,
            IHandle<TaskAddedToJob>,
            IHandle<TaskContentUpdated>
        {
            public FakeDatabase()
            {
                Jobs = new List<JobCreated>();
                TasksAdded = new List<TaskAddedToJob>();
            }

            public List<JobCreated> Jobs { get; private set; }
            public List<TaskAddedToJob> TasksAdded { get; private set; }

            public void Handle(JobCreated evt)
            {
                Jobs.Add(evt);
            }

            public void Handle(TaskAddedToJob evt)
            {
                // TODO: Have a problem here. How do I get access to the generic Content property?
                TasksAdded.Add(evt);
            }

            public void Handle(TaskContentUpdated evt)
            {

            }
        }

        [TestMethod]
        public void TestMethod1()
        {
            IEventAggregator events = new EventAggregator();
            EventStore store = new EventStore(events);
            Repository<SchedulerJob> repository = new Repository<SchedulerJob>(store);

            //SchedulerService service = new SchedulerServiceBuilder()
            //    .WithEventAggregator(events)
            //    .WithEventStore(store)
            //    .WithJobRepository(repository);
            
            events.Subscribe(new SchedulerJobCommandHandlers(repository));

            FakeDatabase readModel = new FakeDatabase();
            events.Subscribe(readModel);

            CreateJob cmd1 = new CreateJob(Guid.NewGuid());
            events.Publish<CreateJob>(cmd1);

            AddTaskToJob<FakeTaskInfo> cmd2 = new AddTaskToJob<FakeTaskInfo>(
                1, // HACK: Why do I have to pass version to avoid concurrency exception?
                cmd1.JobId,
                Guid.NewGuid(),
                SchedulerTaskState.Disabled,
                SchedulerTaskState.Disabled,
                DateTime.Now.Add(TimeSpan.FromMinutes(10)),
                new FakeTaskInfo(Guid.NewGuid()));

            events.Publish<AddTaskToJob>(cmd2);

            //UpdateTaskContent<string> cmd3 = new UpdateTaskContent<string>(cmd1.JobId, cmd2.TaskId, "hello");
            //events.Publish<UpdateTaskContent>(cmd3);

            Assert.AreEqual<int>(1, readModel.Jobs.Count);
            Assert.AreEqual<Guid>(cmd1.JobId, readModel.Jobs[0].JobId);
            Assert.AreEqual<int>(1, readModel.TasksAdded.Count);
            Assert.AreEqual<Guid>(cmd2.TaskId, readModel.TasksAdded[0].TaskId);
        }
    }
}
