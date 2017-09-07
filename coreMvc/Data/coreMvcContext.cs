using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace coreMvc.Models
{
    public class coreMvcContext : DbContext
    {
        public coreMvcContext (DbContextOptions<coreMvcContext> options)
            : base(options)
        {
        }

        public DbSet<coreMvc.Models.Movies> Movies { get; set; }
    }
}
