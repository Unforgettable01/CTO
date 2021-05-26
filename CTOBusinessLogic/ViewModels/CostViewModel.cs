using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace CTOBusinessLogic.ViewModels
{
    [DataContract]
    public class CostViewModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int WorkerId { get; set; }
        [DataMember]
        [DisplayName("Название затраты")]
        public string CostName { get; set; }
        [DataMember]
        [DisplayName("Цена")]
        public decimal CostPrice { get; set; }
        [DisplayName("Имя работника")]
        public string WorkerName { get; set; }
    }
}
