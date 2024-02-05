using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RelaxKundura3.Models;

namespace RelaxKundura3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Kategori> Kategoris { get; set; }
        public DbSet<Urun> Uruns { get; set; }
        public DbSet<Sepet> Sepetts { get; set; }
        public DbSet<Sepetler> Sepetlers { get; set; }
    }
}
