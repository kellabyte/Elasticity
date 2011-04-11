using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

using Elasticity.Domain;

namespace Elasticity
{
    [ServiceContract]
    public interface ISchedulerJobRequestQueue
    {
        event EventHandler<SchedulerJobRequestEventArgs> JobSubmitted;

        [OperationContract(IsOneWay = true)]
        void Submit(SchedulerJob job);
    }
}
