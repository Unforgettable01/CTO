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
        public decimal? RequestSum { get; set; }
        public DateTime? DateOfRequest { get; set; }

        [ForeignKey("RequestId")]
        public virtual List<RequestWork> RequestWorks { get; set; }
        public virtual Client Client { get; set; }
    }
}
