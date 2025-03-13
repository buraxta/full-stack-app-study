using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.models;
using Microsoft.EntityFrameworkCore;

namespace backend.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Todo> Todos { get; set; }
    }
}
