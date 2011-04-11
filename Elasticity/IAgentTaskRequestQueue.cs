using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

using Elasticity.Domain;

namespace Elasticity
{
    [ServiceContract]
    public interface IAgentTaskRequestQueue
    {
        event EventHandler<SchedulerTaskEventArgs> TaskSubmitted;

        [OperationContract(IsOneWay = true)]
        void Submit(ISchedulerTask task);
    }
}
