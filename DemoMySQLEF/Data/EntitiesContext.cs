using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DemoMySqlEF.Models;

    public class EntitiesContext : DbContext
    {
        public EntitiesContext (DbContextOptions<EntitiesContext> options)
            : base(options)
        {
        }

        public DbSet<DemoMySqlEF.Models.Student> Student { get; set; }
    }
