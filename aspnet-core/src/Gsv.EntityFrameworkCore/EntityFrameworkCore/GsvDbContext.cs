using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Gsv.Authorization.Roles;
using Gsv.Authorization.Users;
using Gsv.MultiTenancy;
using Gsv.Types;
using Gsv.Objects;
using Gsv.Staffing;
using Gsv.Tasks;

namespace Gsv.EntityFrameworkCore
{
    public class GsvDbContext : AbpZeroDbContext<Tenant, Role, User, GsvDbContext>
    {
        /* Define a DbSet for each entity of the application */
        // Types
        public DbSet<Category> Categories { get; set; }
        public DbSet<Source> Sources { get; set; }
        
        // Objects
        public DbSet<Capital> Capitals { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Object> Objects { get; set; }
        public DbSet<CargoType> CargoTypes { get; set; }
        public DbSet<PlaceShelf> PlaceShelves { get; set; }

        // Staffing
        public DbSet<Worker> Workers { get; set; }
        public DbSet<PlaceWorker> PlaceWorkers { get; set; }

        // Tasks
        public DbSet<Inspect> Inspects { get; set; }
        public DbSet<InStock> InStocks { get; set; }
        public DbSet<OutStock> OutStocks { get; set; }
        public DbSet<Stocktaking> Stocktakings { get; set; }
        
        public GsvDbContext(DbContextOptions<GsvDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Types
            modelBuilder.Entity<Category>(b =>
            {
                b.HasIndex(e => new { e.Cn }).IsUnique();
            });
            modelBuilder.Entity<Source>(b =>
            {
                b.HasIndex(e => new { e.Cn }).IsUnique();
            });

            // Objects
            modelBuilder.Entity<Capital>(b =>
            {
                b.HasIndex(e => new { e.Cn }).IsUnique();
            });
            modelBuilder.Entity<Place>(b =>
            {
                b.HasIndex(e => new { e.Cn }).IsUnique();
            });
            modelBuilder.Entity<Object>(b =>
            {
                b.HasIndex(e => new { e.CapitalId, e.PlaceId, e.CategoryId }).IsUnique();
            });
            modelBuilder.Entity<CargoType>(b =>
            {
                b.HasIndex(e => new { e.PlaceId, e.CategoryId, e.TypeName }).IsUnique();
            });
            modelBuilder.Entity<PlaceShelf>(b =>
            {
                b.HasIndex(e => new { e.Name, e.CargoTypeId }).IsUnique();
            });

            // Staffing
            modelBuilder.Entity<Worker>(b =>
            {
                b.HasIndex(e => new { e.Cn }).IsUnique();
            });

            // Tasks
        }
    }
}
