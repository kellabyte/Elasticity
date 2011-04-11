//using System;
//using System.Text;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Messaging;
//using System.ServiceModel;
//using System.ServiceModel.Description;
//using System.Threading;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//using Elasticity;

//namespace Elasticity.Tests
//{
//    /// <summary>
//    /// Summary description for RequestQueueTests
//    /// </summary>
//    [TestClass]
//    public class SchedulerQueueTests
//    {
//        public SchedulerQueueTests()
//        {
//        }

//        private TestContext testContextInstance;

//        /// <summary>
//        ///Gets or sets the test context which provides
//        ///information about and functionality for the current test run.
//        ///</summary>
//        public TestContext TestContext
//        {
//            get
//            {
//                return testContextInstance;
//            }
//            set
//            {
//                testContextInstance = value;
//            }
//        }

//        #region Additional test attributes
//        //
//        // You can use the following additional attributes as you write your tests:
//        //
//        // Use ClassInitialize to run code before running the first test in the class
//        // [ClassInitialize()]
//        // public static void MyClassInitialize(TestContext testContext) { }
//        //
//        // Use ClassCleanup to run code after all tests in a class have run
//        // [ClassCleanup()]
//        // public static void MyClassCleanup() { }
//        //
//        // Use TestInitialize to run code before running each test 
//        // [TestInitialize()]
//        // public void MyTestInitialize() { }
//        //
//        // Use TestCleanup to run code after each test has run
//        // [TestCleanup()]
//        // public void MyTestCleanup() { }
//        //
//        #endregion

//        private void host_Opening(object sender, EventArgs e)
//        {
//            SchedulerHost host = sender as SchedulerHost;
//            if (host != null)
//            {
//                if (string.IsNullOrEmpty(host.Configuration.JobRequestQueue) == false && MessageQueue.Exists(host.Configuration.JobRequestQueue) == false)
//                    MessageQueue.Create(host.Configuration.JobRequestQueue, true);
//                if (string.IsNullOrEmpty(host.Configuration.TaskResponseQueue) == false && MessageQueue.Exists(host.Configuration.TaskResponseQueue) == false)
//                    MessageQueue.Create(host.Configuration.TaskResponseQueue, true);
//            }
//        }

//        [TestMethod]
//        public void SubmitJobTest()
//        {
//            using (ManualResetEvent reset = new ManualResetEvent(false))
//            {
//                SchedulerHost host = null;
//                try
//                {
//                    TestScheduler scheduler = new TestScheduler();
//                    host = new SchedulerHost(scheduler, c => 
//                    {
//                        c.JobRequestQueue = @".\private$\JobRequest";
//                        c.TaskResponseQueue = @".\private$\TaskResponse";
//                    });

//                    host.Opening += new EventHandler(host_Opening);
//                    host.Open();

//                    // Sleep for a bit so that the WCF service can drain the queue before we submit our item to test against.
//                    System.Threading.Thread.Sleep(1000);

//                    Guid requestGuid = Guid.NewGuid();
//                    SchedulerJob requestedJob = new SchedulerJob(requestGuid);
                    
//                    Guid taskGuid = Guid.NewGuid();
//                    SchedulerTaskState currentState = SchedulerTaskState.Active;
//                    SchedulerTaskState desiredState = SchedulerTaskState.Active;
//                    DateTimeOffset lockedUntil = new DateTimeOffset(1985, 07, 31, 1, 2, 3, new TimeSpan(-5, 0, 0));
//                    Guid infoGuid = Guid.NewGuid();
//                    TestTaskInfo info = new TestTaskInfo(infoGuid);

//                    SchedulerTask<TestTaskInfo> task = new SchedulerTask<TestTaskInfo>(taskGuid)
//                    {
//                        Content = info,
//                        CurrentState = currentState,
//                        DesiredState = desiredState,
//                        LockedUntil = lockedUntil,

//                    };
//                    requestedJob.Tasks.Add(task);

//                    scheduler.JobSubmitted += (sender, e) =>
//                    {
//                        // Test what we submitted is the one we get back.
//                        Assert.AreEqual<Guid>(requestGuid, e.Job.ID);
//                        Assert.AreEqual<int>(e.Job.Tasks.Count, 1);
//                        Assert.AreEqual<Guid>(taskGuid, e.Job.Tasks[0].ID);
//                        Assert.AreEqual<SchedulerTaskState>(e.Job.Tasks[0].CurrentState, currentState);
//                        Assert.AreEqual<SchedulerTaskState>(e.Job.Tasks[0].DesiredState, desiredState);
//                        Assert.AreEqual<DateTimeOffset>(e.Job.Tasks[0].LockedUntil, lockedUntil);

//                        // Test for casting from SchedulerTask<T>
//                        SchedulerTask<TestTaskInfo> task2 = e.Job.Tasks[0] as SchedulerTask<TestTaskInfo>;
//                        Assert.IsNotNull(task2);
//                        Assert.AreEqual<Guid>(info.InfoID, task2.Content.InfoID);

//                        reset.Set();
//                    };

//                    SchedulerClient client = new SchedulerClient();
//                    client.Submit(requestedJob);

//                    Assert.AreEqual<bool>(true, reset.WaitOne(1000));
//                }
//                finally
//                {
//                    if (host != null)
//                        host.Close();
//                }                
//            }
//        }

//        [TestMethod]
//        public void TaskResponseTest()
//        {
//            using (ManualResetEvent reset = new ManualResetEvent(false))
//            {
//                SchedulerHost host = null;
//                try
//                {
//                    TestScheduler scheduler = new TestScheduler();
//                    host = new SchedulerHost(scheduler, x =>
//                    {
//                        x.JobRequestQueue = @".\private$\JobRequest";
//                        x.TaskResponseQueue = @".\private$\TaskResponse";
//                    });

//                    host.Opening += new EventHandler(host_Opening);
//                    host.Open();

//                    // Sleep for a bit so that the WCF service can drain the queue before we submit our item to test against.
//                    System.Threading.Thread.Sleep(1000);

//                    Guid taskGuid = Guid.NewGuid();
//                    SchedulerTaskState currentState = SchedulerTaskState.Active;
//                    SchedulerTaskState desiredState = SchedulerTaskState.Active;
//                    DateTimeOffset lockedUntil = new DateTimeOffset(1985, 07, 31, 1, 2, 3, new TimeSpan(-5, 0, 0));
//                    Guid infoGuid = Guid.NewGuid();
//                    TestTaskInfo info = new TestTaskInfo(infoGuid);

//                    SchedulerTask<TestTaskInfo> task = new SchedulerTask<TestTaskInfo>(taskGuid)
//                    {
//                        Content = info,
//                        CurrentState = currentState,
//                        DesiredState = desiredState,
//                        LockedUntil = lockedUntil,
//                    };

//                    scheduler.TaskResponded += (sender, e) =>
//                    {
//                        Assert.AreEqual<Guid>(taskGuid, e.Task.ID);
//                        Assert.AreEqual<SchedulerTaskState>(e.Task.CurrentState, currentState);
//                        Assert.AreEqual<SchedulerTaskState>(e.Task.DesiredState, desiredState);
//                        Assert.AreEqual<DateTimeOffset>(e.Task.LockedUntil, lockedUntil);

//                        // Test for casting from SchedulerTask<T>
//                        SchedulerTask<TestTaskInfo> task2 = e.Task as SchedulerTask<TestTaskInfo>;
//                        Assert.IsNotNull(task2);
//                        Assert.AreEqual<Guid>(info.InfoID, task2.Content.InfoID);

//                        reset.Set();
//                    };

//                    SchedulerClient client = new SchedulerClient();
//                    client.Respond(task);

//                    Assert.AreEqual<bool>(true, reset.WaitOne(1000));
//                }
//                finally
//                {
//                    if (host != null)
//                        host.Close();
//                }
//            }
//        }
//    }    
//}
