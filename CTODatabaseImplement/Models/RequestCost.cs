using System;
using System.Collections.Generic;
using System.Text;

namespace CTODatabaseImplement.Models
{
    public class RequestCost
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int CostId { get; set; }
        public virtual Request Request { get; set; }
        public virtual Cost Cost { get; set; }
    }
}
