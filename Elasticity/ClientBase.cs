using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;

namespace Elasticity
{
    public class ClientBase
    {
        protected static EndpointAddress GetDefaultEndPointAddress(string key)
        {
            string endpointAddress = ConfigurationManager.AppSettings[key];
            endpointAddress = endpointAddress.Replace(".", "").Replace("$", "").Replace('\\', '/');
            endpointAddress = string.Format("net.msmq://localhost{0}", endpointAddress);
            EndpointAddress address = new EndpointAddress(endpointAddress);

            return address;
        }

        protected static T CreateClient<T>(EndpointAddress address)
        {
            return CreateClient<T>(null, address);
        }

        protected static T CreateClient<T>(Binding binding, EndpointAddress address)
        {
            if (binding == null)
            {
                binding = new NetMsmqBinding(NetMsmqSecurityMode.None);
            }

            ChannelFactory<T> factory = new ChannelFactory<T>(binding, address);
            T service = factory.CreateChannel();
            ContractDescription cd = factory.Endpoint.Contract;

            // TODO: We should only attach a TaskDataContractResolver for operations that actually require it.
            foreach (OperationDescription operationDescription in cd.Operations)
            {
                DataContractSerializerOperationBehavior serializerBehavior = operationDescription.Behaviors.Find<DataContractSerializerOperationBehavior>();
                if (serializerBehavior == null)
                {
                    serializerBehavior = new DataContractSerializerOperationBehavior(operationDescription);
                    operationDescription.Behaviors.Add(serializerBehavior);
                }
                serializerBehavior.DataContractResolver = new SchedulerTaskDataContractResolver();
            }

            return service;
        }
    }
}
