using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Elasticity.Tests
{
    [DataContract]
    [SchedulerTaskContent]
    public class FakeTaskInfo
    {
        public FakeTaskInfo() : this(Guid.NewGuid())
        {
            
        }

        public FakeTaskInfo(Guid infoID)
        {
            this.InfoID = infoID;
        }

        [DataMember]
        public Guid InfoID { get; set; }
    }
}
