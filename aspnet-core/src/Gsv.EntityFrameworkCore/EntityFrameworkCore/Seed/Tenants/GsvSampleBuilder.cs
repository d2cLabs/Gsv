using System.Linq;
using Microsoft.EntityFrameworkCore;
using Gsv.Types;
using System.Collections.Generic;
using Gsv.Authorization.Roles;
using Abp.Authorization.Roles;
using Gsv.Authorization;
using Abp.Authorization.Users;
using Gsv.Authorization.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Abp.Configuration;

namespace Gsv.EntityFrameworkCore.Seed.Tenants
{
    public class GsvSampleBuilder
    {
        private readonly GsvDbContext _context;
        private readonly int _tenantId;

        public GsvSampleBuilder(GsvDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateCategories();
            CreateSources();
        }

        private void CreateCategories()
        {
            if (_context.Categories.Count() == 0)
            {
                var categories = new List<Category>() 
                {
                    new Category() { TenantId = _tenantId, Cn = "01", Name = "Au999", UnitName = "克", CurrentPrice = 312.23F },
                    new Category() { TenantId = _tenantId, Cn = "02", Name = "铂金", UnitName = "克", CurrentPrice = 112.23F }
                };
                _context.Categories.AddRange(categories);
                _context.SaveChanges();
            }
        }

        private void CreateSources()
        {
            if (_context.Sources.Count() == 0)
            {
                var sources = new List<Source>() 
                {
                    new Source() { TenantId = _tenantId, Cn = "01", Name = "回笼" },
                    new Source() { TenantId = _tenantId, Cn = "02", Name = "自购" }
                };
                _context.Sources.AddRange(sources);
                _context.SaveChanges();
            }
        }
    }
}
