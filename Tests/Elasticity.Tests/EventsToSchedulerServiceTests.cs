using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Elasticity.CommandHandlers;
using Elasticity.Domain;
using Elasticity.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Elasticity.Tests
{
    [TestClass]
    public class EventsToSchedulerServiceTests
    {
        private IEventAggregator eventAggregator = null;
        private IEventStore eventStore = null;
        private IRepository<SchedulerJob> repository = null;
        private SchedulerService scheduler = null;

        [TestInitialize()]
        public void Arrange()
        {
            // Arrange.
            eventAggregator = new EventAggregator();
            eventStore = new InMemoryEventStore(eventAggregator);
            repository = new Repository<SchedulerJob>(eventStore);
            scheduler = new SchedulerService(new EventAggregator(), repository);
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
