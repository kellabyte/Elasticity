using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;

namespace Elasticity
{
    public class ServiceHostBase : ServiceHost
    {
        public ServiceHostBase(object singletonInstance)
            : base(singletonInstance)
        {
            foreach (ServiceEndpoint endpoint in this.Description.Endpoints)
            {
                AttachDataContractResolver(endpoint);
            }
        }

        private void AttachDataContractResolver(ServiceEndpoint endpoint)
        {
            ContractDescription cd = endpoint.Contract;

            foreach (OperationDescription operationDescription in cd.Operations)
            {
                DataContractSerializerOperationBehavior serializerBehavior =
                    operationDescription.Behaviors.Find<DataContractSerializerOperationBehavior>();

                if (serializerBehavior == null)
                {
                    serializerBehavior = new DataContractSerializerOperationBehavior(operationDescription);
                    operationDescription.Behaviors.Add(serializerBehavior);
                }
                serializerBehavior.DataContractResolver = new SchedulerTaskDataContractResolver();
            }
        }
    }
}
