//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.Serialization;
//using System.Text;
//using System.Xml;
//using System.Xml.Schema;
//using System.Xml.Serialization;

//namespace Elasticity
//{
//    [DataContract]
//    public class SchedulerTask<T> : ISchedulerTask
//    {
//        public SchedulerTask() : this(Guid.NewGuid())
//        {            
//        }

//        public SchedulerTask(Guid id)
//        {
//            this.ID = id;
//        }

//        public static implicit operator T(SchedulerTask<T> that)
//        {
//            return that.Content;
//        }

//        [DataMember]
//        public Guid ID { get; set; }

//        [DataMember]
//        public SchedulerTaskState CurrentState { get; set; }

//        [DataMember]
//        public SchedulerTaskState DesiredState { get; set; }

//        [DataMember]
//        public DateTimeOffset LockedUntil { get; set; }

//        [DataMember]
//        public T Content { get; set; }
//    }
//}
