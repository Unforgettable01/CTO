using System;
using System.Collections.Generic;
using System.Text;

namespace CTOBusinessLogic.BindingModels
{
    public class CostBindingModel
    {
        public int? Id { get; set; }
        public int? RequestId { get; set; }
        public int WorkerId { get; set; }
        public string CostName { get; set; }
        public decimal CostPrice { get; set; }
    }
}
