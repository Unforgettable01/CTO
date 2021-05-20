using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace CTOBusinessLogic.ViewModels
{
    [DataContract]
    public class RequestViewModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        public int? PaymentId { get; set; }
        [DataMember]
        [DisplayName("Название заявки")]
        public string RequestName { get; set; }
        [DataMember]
        [DisplayName("Стоимость заявки")]
        public decimal RequestCost { get; set; }
        [DataMember]
        [DisplayName("Сумма к оплате")]
        public decimal? RequestSumToPayment { get; set; }
        [DisplayName("Работы")]
        public List<WorkViewModel> Works { get; set; }

        [DataMember]
        [DisplayName("Дата начала")]
        public DateTime? DateFrom { get; set; }

        [DataMember]
        [DisplayName("Дата окончания")]
        public DateTime? DateTo { get; set; }
        [DataMember]
        public Dictionary<int, (string, decimal)> RequestWorks { get; set; }
        public Dictionary<int, (string, decimal)> RequestCosts { get; set; }
        public List<CostViewModel> Costs { get; set; }
        [DisplayName("Номера")]
        public List<int> WorkId { get; set; }
    }
}
