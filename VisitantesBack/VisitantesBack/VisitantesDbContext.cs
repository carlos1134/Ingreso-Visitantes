using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitantesBack.Models;

namespace VisitantesBack
{
    public class VisitantesDbContext : DbContext
    {      
        public VisitantesDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<VisitantesModel> VisitantesModelo { get; set; }
        public DbSet<PersonasModel> PersonasModelo { get; set; }

    }
}
