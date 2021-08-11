using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CTODatabaseImplement.Models
{
    public class Work
    {
        public int Id { get; set; }
        [Required]
        public string WorkName { get; set; }
        [Required]
        public decimal WorkPrice { get; set; }

        [ForeignKey("WorkId")]
        public virtual List<RequestWork> RequestWorks { get; set; }
        [ForeignKey("WorkId")]
        public virtual List<Cost> Costs { get; set; }
    }
}
