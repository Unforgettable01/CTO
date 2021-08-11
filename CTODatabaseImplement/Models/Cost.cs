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
        [Required]
        public string CostName { get; set; }
        [Required]
        public decimal CostPrice { get; set; }
        public int WorkId { get; set; }
        public int RequestId { get; set; }

        public virtual Work Work { get; set; }
        public virtual Request Request { get; set; }


        [ForeignKey("CostId")]
        public virtual List<Request> Requests{ get; set; }

    }
}
