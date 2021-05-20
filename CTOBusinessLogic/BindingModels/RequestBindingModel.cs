using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CTOBusinessLogic.BindingModels
{
    [DataContract]
    public class RequestBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public int? PaymentId { get; set; }
        [DataMember]
        public string RequestName { get; set; }
        [DataMember]
        public decimal RequestCost { get; set; }

        [DataMember]
        public DateTime? DateFrom { get; set; }

        [DataMember]
        public DateTime? DateTo { get; set; }
        [DataMember]
        public Dictionary<int, (string, decimal)> RequestWorks { get; set; }
        public Dictionary<int, (string, decimal)> RequestCosts { get; set; }
    }
}
