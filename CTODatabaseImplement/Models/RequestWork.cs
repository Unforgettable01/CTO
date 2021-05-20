using System;
using System.Collections.Generic;
using System.Text;

namespace CTODatabaseImplement.Models
{
    public class RequestWork
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int WorkId { get; set; }
        public virtual Request Request { get; set; }
        public virtual Work Work { get; set; }
    }
}
