using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace CTOBusinessLogic.ViewModels
{
    [DataContract]
    public class WorkViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int WorkekId { get; set; }

        [DataMember]
        [DisplayName("Название работы")]
        public string WorkName { get; set; }

        [DataMember]
        [DisplayName("Цена")]
        public decimal WorkPrice { get; set; }
    }
}
