using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CTODatabaseImplement.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        public string FIO { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string NumberPhone { get; set; }
        [Required]

        [ForeignKey("ClientId")]
        public virtual List<Request> Requests { get; set; }
        [ForeignKey("ClientId")]
        public virtual List<Payment> Payments { get; set; }

    }
}
