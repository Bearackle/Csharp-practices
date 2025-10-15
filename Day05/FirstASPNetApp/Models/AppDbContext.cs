using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace FirstASPNetApp.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public AppDbContext()
        {
            
        }
    }
}