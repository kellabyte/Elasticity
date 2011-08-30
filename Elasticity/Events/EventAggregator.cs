namespace Elasticity.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Elasticity.Extensions;

    ///// <summary>
    ///// Enables loosely-coupled publication of and subscription to events.
    ///// </summary>
    //public interface IEventAggregator
    //{
    //    /// <summary>
    //    /// Subscribes an instance to all events declared through implementations of <see cref="IHandle{T}"/>
    //    /// </summary>
    //    /// <param name="instance">The instance to subscribe for event publication.</param>
    //    void Subscribe(object instance);

    //    /// <summary>
    //    /// Unsubscribes the instance from all events.
    //    /// </summary>
    //    /// <param name="instance">The instance to unsubscribe.</param>
    //    void Unsubscribe(object instance);

    //    /// <summary>
    //    /// Publishes a message.
    //    /// </summary>
    //    /// <typeparam name="T">The type of message being published.</typeparam>
    //    /// <param name="message">The message instance.</param>
    //    void Publish<T>(T message);
    //}

    ///// <summary>
    ///// Enables loosely-coupled publication of and subscription to events.
    ///// </summary>
    //public class EventAggregator : IEventAggregator
    //{
    //    //static readonly ILog Log = LogManager.GetLog(typeof(EventAggregator));
    //    readonly List<WeakReference> subscribers = new List<WeakReference>();

    //    /// <summary>
    //    /// Subscribes an instance to all events declared through implementations of <see cref="IHandle{T}"/>
    //    /// </summary>
    //    /// <param name="instance">The instance to subscribe for event publication.</param>
    //    public void Subscribe(object instance)
    //    {
    //        lock (subscribers)
    //        {
    //            if (subscribers.Any(reference => reference.Target == instance))
    //                return;

    //            //Log.Info("Subscribing {0}.", instance);
    //            subscribers.Add(new WeakReference(instance));
    //        }
    //    }

    //    /// <summary>
    //    /// Unsubscribes the instance from all events.
    //    /// </summary>
    //    /// <param name="instance">The instance to unsubscribe.</param>
    //    public void Unsubscribe(object instance)
    //    {
    //        lock (subscribers)
    //        {
    //            var found = subscribers
    //                .FirstOrDefault(reference => reference.Target == instance);

    //            if (found != null)
    //                subscribers.Remove(found);
    //        }
    //    }

    //    /// <summary>
    //    /// Publishes a message.
    //    /// </summary>
    //    /// <typeparam name="TMessage">The type of message being published.</typeparam>
    //    /// <param name="message">The message instance.</param>
    //    public void Publish<TMessage>(TMessage message)
    //    {
    //        WeakReference[] toNotify;
    //        lock (subscribers)
    //            toNotify = subscribers.ToArray();

    //        //Execute.OnUIThread(() =>
    //        //{
    //            //Log.Info("Publishing {0}.", message);
    //            var dead = new List<WeakReference>();

    //            foreach (var reference in toNotify)
    //            {
    //                var target = reference.Target as IHandle<TMessage>;

    //                if (target != null)
    //                {
    //                    target.Handle(message);
    //                }
    //                else if (target == null && typeof(TMessage) == typeof(Event))
    //                {
    //                    dynamic dynamicTarget = reference.Target.AsDynamic();
    //                    dynamicTarget.Handle(message);
    //                }
    //                else if (!reference.IsAlive)
    //                {
    //                    dead.Add(reference);
    //                }
    //            }
    //            if (dead.Count > 0)
    //            {
    //                lock (subscribers)
    //                    dead.Apply(x => subscribers.Remove(x));
    //            }
    //        //});
    //    }
    //}
















    ///// <summary>
    /////   Enables loosely-coupled publication of and subscription to events.
    ///// </summary>
    //public interface IEventAggregator
    //{
    //    /// <summary>
    //    ///   Subscribes an instance to all events declared through implementations of <see cref = "IHandle{T}" />
    //    /// </summary>
    //    /// <param name = "instance">The instance to subscribe for event publication.</param>
    //    void Subscribe(object instance);

    //    /// <summary>
    //    ///   Unsubscribes the instance from all events.
    //    /// </summary>
    //    /// <param name = "instance">The instance to unsubscribe.</param>
    //    void Unsubscribe(object instance);

    //    /// <summary>
    //    ///   Publishes a message.
    //    /// </summary>
    //    /// <param name = "message">The message instance.</param>
    //    void Publish(object message);
    //}

    ///// <summary>
    ///// Enables loosely-coupled publication of and subscription to events.
    ///// </summary>
    //public class EventAggregator : IEventAggregator
    //{
    //    //static readonly ILog Log = LogManager.GetLog(typeof(EventAggregator));
    //    readonly List<Handler> handlers = new List<Handler>();

    //    /// <summary>
    //    ///   Subscribes an instance to all events declared through implementations of <see cref = "IHandle{T}" />
    //    /// </summary>
    //    /// <param name = "instance">The instance to subscribe for event publication.</param>
    //    public void Subscribe(object instance)
    //    {
    //        lock (handlers)
    //        {
    //            if (handlers.Any(x => x.Matches(instance)))
    //                return;

    //            //Log.Info("Subscribing {0}.", instance);
    //            handlers.Add(new Handler(instance));
    //        }
    //    }

    //    /// <summary>
    //    ///  Unsubscribes the instance from all events.
    //    /// </summary>
    //    /// <param name = "instance">The instance to unsubscribe.</param>
    //    public void Unsubscribe(object instance)
    //    {
    //        lock (handlers)
    //        {
    //            var found = handlers.FirstOrDefault(x => x.Matches(instance));

    //            if (found != null)
    //                handlers.Remove(found);
    //        }
    //    }

    //    /// <summary>
    //    ///   Publishes a message.
    //    /// </summary>
    //    /// <param name = "message">The message instance.</param>
    //    public void Publish(object message)
    //    {
    //        Handler[] toNotify;
    //        lock (handlers)
    //            toNotify = handlers.ToArray();

    //        //Execute.OnUIThread(() =>
    //        //{
    //            //Log.Info("Publishing {0}.", message);
    //            var messageType = message.GetType();

    //            var dead = toNotify.Where(handler => !handler.Handle(messageType, message));

    //            if (dead.Any())
    //            {
    //                lock (handlers)
    //                    dead.Apply(x => handlers.Remove(x));
    //            }
    //        //});
    //    }

    //    class Handler
    //    {
    //        readonly WeakReference reference;
    //        readonly Dictionary<Type, MethodInfo> supportedHandlers = new Dictionary<Type, MethodInfo>();

    //        public Handler(object handler)
    //        {
    //            reference = new WeakReference(handler);

    //            var interfaces = handler.GetType().GetInterfaces()
    //                .Where(x => typeof(IHandle).IsAssignableFrom(x) && x.IsGenericType);

    //            foreach (var @interface in interfaces)
    //            {
    //                var type = @interface.GetGenericArguments()[0];
    //                var method = @interface.GetMethod("Handle");
    //                supportedHandlers[type] = method;
    //            }
    //        }

    //        public bool Matches(object instance)
    //        {
    //            return reference.Target == instance;
    //        }

    //        public bool Handle(Type messageType, object message)
    //        {
    //            var target = reference.Target;
    //            if (target == null)
    //                return false;

    //            foreach (var pair in supportedHandlers)
    //            {
    //                if (pair.Key.IsAssignableFrom(messageType))
    //                {
    //                    pair.Value.Invoke(target, new[] { message });
    //                    return true;
    //                }
    //            }

    //            return true;
    //        }
    //    }
    //}






    public class ServiceBus : EventAggregator
    {
        private readonly IBus bus = null;

        public ServiceBus(IBus bus)
        {
            this.bus = bus;
        }

        public override void Publish(object message)
        {
            base.Publish(message);
            bus.Publish(message);
        }

        public override void Publish(object message, Action<Action> marshal)
        {
            base.Publish(message, marshal);
            bus.Publish(message);
        }

        public override void Subscribe(object instance)
        {
            base.Subscribe(instance);

            var interfaces = instance.GetType().GetInterfaces()
                    .Where(x => typeof(IHandle).IsAssignableFrom(x) && x.IsGenericType);

            foreach (var @interface in interfaces)
            {
                var messageType = @interface.GetGenericArguments()[0];                
                bus.Subscribe(messageType);
            }
        }

        public override void Unsubscribe(object instance)
        {
            base.Unsubscribe(instance);

            var interfaces = instance.GetType().GetInterfaces()
                    .Where(x => typeof(IHandle).IsAssignableFrom(x) && x.IsGenericType);

            foreach (var @interface in interfaces)
            {
                var messageType = @interface.GetGenericArguments()[0];
                bus.Unsubscribe(messageType);
            }
        }
    }










    /// <summary>
    ///   Enables loosely-coupled publication of and subscription to events.
    /// </summary>
    public interface IEventAggregator
    {
        /// <summary>
        ///   Gets or sets the default publication thread marshaller.
        /// </summary>
        /// <value>
        ///   The default publication thread marshaller.
        /// </value>
        Action<System.Action> PublicationThreadMarshaller { get; set; }

        /// <summary>
        ///   Subscribes an instance to all events declared through implementations of <see cref = "IHandle{T}" />
        /// </summary>
        /// <param name = "instance">The instance to subscribe for event publication.</param>
        void Subscribe(object instance);

        /// <summary>
        ///   Unsubscribes the instance from all events.
        /// </summary>
        /// <param name = "instance">The instance to unsubscribe.</param>
        void Unsubscribe(object instance);

        /// <summary>
        ///   Publishes a message.
        /// </summary>
        /// <param name = "message">The message instance.</param>
        /// <remarks>
        ///   Uses the default thread marshaller during publication.
        /// </remarks>
        void Publish(object message);

        /// <summary>
        ///   Publishes a message.
        /// </summary>
        /// <param name = "message">The message instance.</param>
        /// <param name = "marshal">Allows the publisher to provide a custom thread marshaller for the message publication.</param>
        void Publish(object message, Action<System.Action> marshal);
    }

    /// <summary>
    ///   Enables loosely-coupled publication of and subscription to events.
    /// </summary>
    public class EventAggregator : IEventAggregator
    {
        /// <summary>
        ///   The default thread marshaller used for publication;
        /// </summary>
        public static Action<System.Action> DefaultPublicationThreadMarshaller = action => action();

        readonly List<Handler> handlers = new List<Handler>();

        /// <summary>
        ///   Initializes a new instance of the <see cref = "EventAggregator" /> class.
        /// </summary>
        public EventAggregator()
        {
            PublicationThreadMarshaller = DefaultPublicationThreadMarshaller;
        }

        /// <summary>
        ///   Gets or sets the default publication thread marshaller.
        /// </summary>
        /// <value>
        ///   The default publication thread marshaller.
        /// </value>
        public Action<System.Action> PublicationThreadMarshaller { get; set; }

        /// <summary>
        ///   Subscribes an instance to all events declared through implementations of <see cref = "IHandle{T}" />
        /// </summary>
        /// <param name = "instance">The instance to subscribe for event publication.</param>
        public virtual void Subscribe(object instance)
        {
            lock (handlers)
            {
                if (handlers.Any(x => x.Matches(instance)))
                    return;

                handlers.Add(new Handler(instance));
            }
        }

        /// <summary>
        ///   Unsubscribes the instance from all events.
        /// </summary>
        /// <param name = "instance">The instance to unsubscribe.</param>
        public virtual void Unsubscribe(object instance)
        {
            lock (handlers)
            {
                var found = handlers.FirstOrDefault(x => x.Matches(instance));

                if (found != null)
                    handlers.Remove(found);
            }
        }

        /// <summary>
        ///   Publishes a message.
        /// </summary>
        /// <param name = "message">The message instance.</param>
        /// <remarks>
        ///   Does not marshall the the publication to any special thread by default.
        /// </remarks>
        public virtual void Publish(object message)
        {
            Publish(message, PublicationThreadMarshaller);
        }

        /// <summary>
        ///   Publishes a message.
        /// </summary>
        /// <param name = "message">The message instance.</param>
        /// <param name = "marshal">Allows the publisher to provide a custom thread marshaller for the message publication.</param>
        public virtual void Publish(object message, Action<System.Action> marshal)
        {
            Handler[] toNotify;
            lock (handlers)
                toNotify = handlers.ToArray();

            marshal(() =>
            {
                var messageType = message.GetType();

                var dead = toNotify
                    .Where(handler => !handler.Handle(messageType, message))
                    .ToList();

                if (dead.Any())
                {
                    lock (handlers)
                    {
                        dead.Apply(x => handlers.Remove(x));
                    }
                }
            });
        }

        protected class Handler
        {
            readonly WeakReference reference;
            readonly Dictionary<Type, MethodInfo> supportedHandlers = new Dictionary<Type, MethodInfo>();

            public Handler(object handler)
            {
                reference = new WeakReference(handler);

                var interfaces = handler.GetType().GetInterfaces()
                    .Where(x => typeof(IHandle).IsAssignableFrom(x) && x.IsGenericType);

                foreach (var @interface in interfaces)
                {
                    var type = @interface.GetGenericArguments()[0];
                    var method = @interface.GetMethod("Handle");
                    supportedHandlers[type] = method;
                }
            }

            public bool Matches(object instance)
            {
                return reference.Target == instance;
            }

            public bool Handle(Type messageType, object message)
            {
                var target = reference.Target;
                if (target == null)
                    return false;

                foreach (var pair in supportedHandlers)
                {
                    if (pair.Key.IsAssignableFrom(messageType))
                    {
                        pair.Value.Invoke(target, new[] { message });
                        return true;
                    }
                }

                return true;
            }
        }
    }    
}