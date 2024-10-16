using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace IMDBApi
{
    public class IMDBDbContext : DbContext
    {
        public IMDBDbContext(DbContextOptions<IMDBDbContext> options) : base(options) { }

        public DbSet<Name> Names { get; set; } // DbSet for Names
        public DbSet<Title> Titles { get; set; } // DbSet for Titles
        public DbSet<Crew> Crews { get; set; } // DbSet for Crews

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Name>(entity =>
            {
                entity.HasKey(e => e.Nconst); // Specify primary key
                entity.ToView("getPerson"); // Map to the Persons table
              
            });
            modelBuilder.Entity<Title>(entity =>
            {
                entity.HasKey(e => e.Tconst); // Specify primary key
                entity.ToView("getTitles"); // Map to the Titles table
            });
            



            base.OnModelCreating(modelBuilder);
        }
    }

}

