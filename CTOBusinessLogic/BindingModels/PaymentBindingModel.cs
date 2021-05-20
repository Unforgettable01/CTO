using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CTOBusinessLogic.BindingModels
{
    public class PaymentBindingModel
    {
        public int? Id { get; set; }
        [DataMember]
        public int WorkId { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public decimal Sum { get; set; }
        [DataMember]
        public DateTime? DateOfPayment { get; set; }
    }
}
