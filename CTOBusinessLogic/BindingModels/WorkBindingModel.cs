using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CTOBusinessLogic.BindingModels
{
    [DataContract]
    public class WorkBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int WorkerId { get; set; }
        public int RequestId { get; set; }

        [DataMember]
        public string WorkName { get; set; }
        [DataMember]
        public decimal WorkPrice { get; set; }
    }
}
