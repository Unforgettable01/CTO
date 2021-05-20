using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CTODatabaseImplement.Models
{
    public class Worker
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

        [ForeignKey("WorkerId")]
        public virtual List<Cost> Costs { get; set; }
        [ForeignKey("WorkerId")]
        public virtual List<Work> Works { get; set; }
    }
}
