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
    // [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
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
        public DbSet<Shelf> Shelves { get; set; }

        // Staffing
        public DbSet<Worker> Workers { get; set; }

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
                b.HasIndex(e => new { e.TenantId, e.Cn }).IsUnique();
            });
            modelBuilder.Entity<Source>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.Cn }).IsUnique();
            });

            // Objects
            modelBuilder.Entity<Capital>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.Cn }).IsUnique();
            });
            modelBuilder.Entity<Place>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.Cn }).IsUnique();
            });
            modelBuilder.Entity<Object>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.CapitalId, e.PlaceId, e.CategoryId }).IsUnique();
            });
            modelBuilder.Entity<CargoType>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.PlaceId, e.CategoryId, e.TypeName }).IsUnique();
            });
            modelBuilder.Entity<Shelf>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.Name, e.CargoTypeId }).IsUnique();
            });

            // Staffing
            modelBuilder.Entity<Worker>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.Cn }).IsUnique();
            });

            // Tasks
            modelBuilder.Entity<InStock>(b => 
            {
                b.HasIndex(e => new { e.TenantId, e.CarryoutDate, e.ShelfId });
            });
            modelBuilder.Entity<OutStock>(b => 
            {
                b.HasIndex(e => new { e.TenantId, e.CarryoutDate, e.ShelfId });
            });
            modelBuilder.Entity<Inspect>(b => 
            {
                b.HasIndex(e => new { e.TenantId, e.CarryoutDate, e.ShelfId });
            });
            modelBuilder.Entity<Stocktaking>(b => 
            {
                b.HasIndex(e => new { e.TenantId, e.CarryoutDate, e.ShelfId });
            });

            // ForeignKey Conflict
            modelBuilder.Entity<CargoType>()
                .HasOne(b => b.Place).WithMany().OnDelete(DeleteBehavior.Restrict);
 
        }
    }
}
