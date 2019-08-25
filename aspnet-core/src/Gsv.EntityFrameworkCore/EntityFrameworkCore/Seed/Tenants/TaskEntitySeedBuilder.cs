using System.Linq;
using Gsv.Types;
using Gsv.Staffing;
using Gsv.Objects;
using System;
using Gsv.Tasks;

namespace Gsv.EntityFrameworkCore.Seed.Tenants
{
    public class TaskEntitySeedBuilder
    {
        private readonly GsvDbContext _context;
        private readonly int _tenantId;

        public TaskEntitySeedBuilder(GsvDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateInStocks();
        }

        private void CreateInStocks()
        {
            if (_context.InStocks.FirstOrDefault(x => x.CarryoutDate == DateTime.Today) == null)
            {
                _context.InStocks.AddRange(new InStock[]
                {
                    new InStock() { TenantId = _tenantId, CarryoutDate = DateTime.Today, WorkerId = 1, ShelfId = 1, Quantity = 23.12F, SourceId = 1, CreateTime = DateTime.Now },
                    new InStock() { TenantId = _tenantId, CarryoutDate = DateTime.Today, WorkerId = 2, ShelfId = 2, Quantity = 43.32F, SourceId = 2, CreateTime = DateTime.Now.AddMinutes(5) },
                });
                _context.SaveChanges();
            }
        }
    }
}
