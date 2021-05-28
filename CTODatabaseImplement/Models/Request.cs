using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CTODatabaseImplement.Models
{
    public class Request
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        [Required]
        public string RequestName { get; set; }
        [Required]
        public decimal? RequestCost { get; set; }
        [Required]
        public DateTime? DateFrom { get; set; }
        [Required]
        public DateTime? DateTo { get; set; }

        [ForeignKey("RequestId")]
        public virtual List<RequestWork> RequestWorks { get; set; }
        [ForeignKey("RequestId")]
        public virtual List<RequestCost> RequestCosts { get; set; }
        public virtual Client Client { get; set; }
    }
}
