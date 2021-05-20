﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CTODatabaseImplement.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int WorkId { get; set; }
        public int ClientId { get; set; }
        [Required]
        public decimal Sum { get; set; }
        [Required]
        public DateTime? DateOfPayment { get; set; }
        public virtual Work Work { get; set; }
        public virtual Client Client { get; set; }
    }
}
