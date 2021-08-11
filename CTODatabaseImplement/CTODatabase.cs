using CTODatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTODatabaseImplement
{
    public class CTODatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CTODatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Client> Clients { set; get; }
        public virtual DbSet<Worker> Workers { set; get; }
        public virtual DbSet<Request> Requests { set; get; }
        public virtual DbSet<Work> Works { set; get; }
        public virtual DbSet<Payment> Payments { set; get; }
        public virtual DbSet<Cost> Costs { set; get; }
        public virtual DbSet<RequestWork> RequestWorks { set; get; }
    }
}
