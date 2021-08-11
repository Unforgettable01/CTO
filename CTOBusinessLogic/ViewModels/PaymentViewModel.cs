using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace CTOBusinessLogic.ViewModels
{
    [DataContract]
    public class PaymentViewModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int RequestId { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        [DisplayName("Сумма оплаты")]
        public decimal Sum { get; set; }
        [DataMember]
        [DisplayName("Дата оплаты")]
        public DateTime? DateOfPayment { get; set; }
    }
}
