using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaHW.Persistence;
using Microsoft.AspNetCore.Identity;

namespace CinemaHW.Persistence
{
    public class CinemaHWDbContext : IdentityDbContext<Users , IdentityRole<String>, String>
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actors> Actors { get; set; }
        public DbSet<Places> Places { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Programs> Program { get; set; }
        public DbSet<Rent> Rent { get; set; }
        public DbSet<MoviesImage> MoviesImages { get; set; }
        public CinemaHWDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder
                .Entity<Users>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();


        }
    }
}
