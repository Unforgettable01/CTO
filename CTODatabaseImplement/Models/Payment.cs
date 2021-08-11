using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CTODatabaseImplement.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int RequestId { get; set; }
        [Required]
        public decimal? Sum { get; set; }
        [Required]
        public DateTime? DateOfPayment { get; set; }
        public virtual Request Request { get; set; }
        public virtual Client Client { get; set; }
    }
}
