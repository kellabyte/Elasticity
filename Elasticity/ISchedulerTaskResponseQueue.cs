using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

using Elasticity.Domain;

namespace Elasticity
{
    [ServiceContract]
    public interface ISchedulerTaskResponseQueue
    {
        event EventHandler<SchedulerTaskEventArgs> TaskResponded;

        [OperationContract(IsOneWay = true)]
        void Respond(ISchedulerTask task);
    }
}
