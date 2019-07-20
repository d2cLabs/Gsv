using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Gsv.Authorization.Roles;
using Gsv.Authorization.Users;
using Gsv.MultiTenancy;

namespace Gsv.EntityFrameworkCore
{
    public class GsvDbContext : AbpZeroDbContext<Tenant, Role, User, GsvDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public GsvDbContext(DbContextOptions<GsvDbContext> options)
            : base(options)
        {
        }
    }
}
