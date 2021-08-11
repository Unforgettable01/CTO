using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace CTOBusinessLogic.ViewModels
{
    [DataContract]
    public class RequestViewModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        [DisplayName("Название заявки")]
        public string RequestName { get; set; }
        [DataMember]
        [DisplayName("Стоимость заявки")]
        public decimal RequestSum { get; set; }
        public DateTime? DateFrom { get; set; }
        [DataMember]
        [DisplayName("Дата окончания")]
        public DateTime? DateTo { get; set; }
        public Dictionary<int, (string, decimal)> RequestWorks { get; set; }
        public  List<int> WorkId { get; set; }


    }
}
