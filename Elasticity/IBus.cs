using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticity.Events;

namespace Elasticity
{
    public interface IBus
    {
        /// <summary>
        /// Initialize the bus.
        /// </summary>
        void Initialize();
        
        /// <summary>
        /// Shut down and dispose the bus.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Subscribes an instance to all events declared through implementation of <see cref="IHandle"/>
        /// </summary>
        /// <param name="instance">The instance to subscribe for event publication.</param>
        void Subscribe(Type messageType);

        /// <summary>
        /// Unsubscribe the instance from the events.
        /// </summary>
        /// <param name="instance">The instance to unsubscribe.</param>
        void Unsubscribe(Type messageType);

        //void Publish<T>(object message) where T : IMessage, new();        
        void Publish(object message);
    }
}
