using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CTOBusinessLogic.BindingModels
{
    [DataContract]
    public class CostBindingModel
    {
        public int? Id { get; set; }
        public int WorkId { get; set; }
        public int RequestId { get; set; }
        [DataMember]
        public string CostName { get; set; }
        [DataMember]
        public decimal CostPrice { get; set; }
    }
}
