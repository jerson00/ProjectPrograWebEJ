using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectEJ.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Apuestas> Apuestas { get; set; }
        public DbSet<Sorteos> Sorteos { get; set; }
        public DbSet<Caja> Caja { get; set; }
        public DbSet<Ganadores> Ganadores { get; set; }

    }
}