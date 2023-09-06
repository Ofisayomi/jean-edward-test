using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jean_edwards.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace jean_edwards.Database
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) {
        }
        public DbSet<MovieResult> MovieResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MovieResult>().HasIndex(x=> x.ImdbId);
        }
    }
}