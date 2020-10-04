using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetCoreWebShell.Entities;

namespace NetCoreWebShell
{
    public class Context : DbContext
    {
        public DbSet<Input> Inputs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=WebShellDB;Trusted_Connection=True;connect timeout=200;");
        }
    }
}
