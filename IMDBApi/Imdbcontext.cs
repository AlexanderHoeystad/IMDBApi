using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace IMDBApi
{
    public class ImdbContext : DbContext
    {
        public ImdbContext(DbContextOptions<ImdbContext> options) : base(options) { }

        public DbSet<Name> Names { get; set; } // DbSet for Names

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Name>(entity =>
            {
                entity.HasKey(e => e.Nconst); // Specify primary key
                entity.ToView("getPerson"); // Map to the Persons table
            });

            base.OnModelCreating(modelBuilder);
        }
    }

}

