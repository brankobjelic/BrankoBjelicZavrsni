using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BrankoBjelicZavrsni.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Agency>().HasData(
                new Agency () { Id  = 1, Name = "Naj nekretnine", YearFounded = 2005},
                new Agency() { Id = 2, Name = "dupleks nekretnine", YearFounded = 2010},
                new Agency() { Id = 3, Name = "Fast nekretnine", YearFounded = 2005}
                );
            builder.Entity<Advertisement>().HasData(
                new Advertisement() { Id = 1, Name = "Komforna porodicna kuca", EstateType = "Kuca", YearConstructed = 1987, EstatePrice = 110000, AgencyId = 3},
                new Advertisement() { Id = 2, Name  = "Stan na ekstra lokaciji", EstateType = "Stan", YearConstructed = 1979, EstatePrice = 80000, AgencyId = 1},
                new Advertisement() { Id = 3, Name = "Moderan dupleks", EstateType = "Dupleks stan", YearConstructed = 2020, EstatePrice  = 220000, AgencyId = 2},
                new Advertisement() { Id = 4, Name = "Povoljna vikendica", EstateType = "vikendica", YearConstructed = 1971, EstatePrice = 50000, AgencyId = 1},
                new Advertisement() { Id = 5, Name = "Stan u sirem centru", EstateType = "Stan", YearConstructed = 1955, EstatePrice = 90000, AgencyId = 3}
                );

            base.OnModelCreating(builder);
        }
    }
}
