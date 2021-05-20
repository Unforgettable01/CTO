using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CTODatabaseImplement.Models
{
    public class Cost
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }

        public virtual Worker Worker { get; set; }
        [Required]
        public string CostName { get; set; }
        [Required]
        public decimal CostPrice { get; set; }
        [ForeignKey("CostId")]
        public virtual List<RequestCost> RequestCost { get; set; }
    }
}
