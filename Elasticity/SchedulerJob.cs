//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.Serialization;
//using System.Text;

//namespace Elasticity
//{
//    [DataContract]
//    public class SchedulerJob
//    {
//        public SchedulerJob() 
//            : this(Guid.NewGuid(), null)
//        {
            
//        }

//        public SchedulerJob(Guid id) 
//            : this(id, null)
//        {
            
//        }

//        public SchedulerJob(List<ISchedulerTask> tasks)
//            : this(Guid.NewGuid(), tasks)
//        {
            
//        }

//        public SchedulerJob(Guid id, List<ISchedulerTask> tasks)
//        {
//            this.ID = id;

//            if (tasks == null)
//                this.Tasks = new List<ISchedulerTask>();
//            else
//                this.Tasks = tasks;
//        }

//        [DataMember]
//        public Guid ID { get; set; }        

//        [DataMember]
//        public List<ISchedulerTask> Tasks { get; set; }
//    }
//}
