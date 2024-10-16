using IMDBApi;
using Microsoft.EntityFrameworkCore;
using System;

public class IMDBDbContext : DbContext
{
    public IMDBDbContext(DbContextOptions<IMDBDbContext> options) : base(options) { }
    public DbSet<Title> Titles { get; set; }
    public DbSet<Crew> Crews { get; set; }
    public DbSet<Name> Names { get; set; }
}

