//using System;
//using System.Text;
//using System.Collections.Generic;
//using System.Linq;
//using System.Messaging;
//using System.Threading;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace Elasticity.Tests
//{
//    [TestClass]
//    public class AgentQueueTests
//    {
//        private void host_Opening(object sender, EventArgs e)
//        {
//            AgentHost host = sender as AgentHost;
//            if (host != null)
//            {
//                if (string.IsNullOrEmpty(host.Configuration.TaskRequestQueue) == false && MessageQueue.Exists(host.Configuration.TaskRequestQueue) == false)
//                    MessageQueue.Create(host.Configuration.TaskRequestQueue, true);
//            }
//        }

//        [TestMethod]
//        public void SubmitTaskTest()
//        {
//            using (ManualResetEvent reset = new ManualResetEvent(false))
//            {
//                AgentHost host = null;
//                try
//                {
//                    TestAgent agent = new TestAgent();
//                    host = new AgentHost(agent, c =>
//                    {
//                        c.TaskRequestQueue = @".\private$\TaskRequest";
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

//                    agent.TaskSubmitted += (sender, e) =>
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

//                    AgentClient client = new AgentClient();
//                    client.Submit(task);

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
