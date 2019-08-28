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
            CreateOutStocks();
            CreateInspects();
            CreateStocktakings();
        }

        private void CreateInStocks()
        {
            if (_context.InStocks.FirstOrDefault(x => x.CarryoutDate == DateTime.Today) == null)
            {
                _context.InStocks.AddRange(new InStock[]
                {
                    new InStock() { TenantId = _tenantId, CarryoutDate = DateTime.Today, WorkerId = 1, ShelfId = 1, Quantity = 23.12F, SourceId = 1, CreateTime = DateTime.Now },
                    new InStock() { TenantId = _tenantId, CarryoutDate = DateTime.Today, WorkerId = 1, ShelfId = 1, Quantity = 43.32F, SourceId = 2, CreateTime = DateTime.Now.AddMinutes(5) },
                    new InStock() { TenantId = _tenantId, CarryoutDate = DateTime.Today, WorkerId = 1, ShelfId = 2, Quantity = 33.12F, SourceId = 1, CreateTime = DateTime.Now.AddMinutes(10) },
                    new InStock() { TenantId = _tenantId, CarryoutDate = DateTime.Today, WorkerId = 2, ShelfId = 2, Quantity = 13.03F, SourceId = 2, CreateTime = DateTime.Now.AddMinutes(15) },
                });
                _context.SaveChanges();
            }
        }
        private void CreateOutStocks()
        {
            if (_context.OutStocks.FirstOrDefault(x => x.CarryoutDate == DateTime.Today) == null)
            {
                _context.OutStocks.AddRange(new OutStock[]
                {
                    new OutStock() { TenantId = _tenantId, CarryoutDate = DateTime.Today, WorkerId = 1, ShelfId = 1, Quantity = 83.12F, CreateTime = DateTime.Now },
                    new OutStock() { TenantId = _tenantId, CarryoutDate = DateTime.Today, WorkerId = 1, ShelfId = 1, Quantity = 33.97F, CreateTime = DateTime.Now.AddMinutes(5) },
                    new OutStock() { TenantId = _tenantId, CarryoutDate = DateTime.Today, WorkerId = 2, ShelfId = 2, Quantity = 39.17F, CreateTime = DateTime.Now.AddMinutes(10) },
                    new OutStock() { TenantId = _tenantId, CarryoutDate = DateTime.Today, WorkerId = 2, ShelfId = 2, Quantity = 23.03F, CreateTime = DateTime.Now.AddMinutes(15) },
                });
                _context.SaveChanges();
            }
        }
        private void CreateInspects()
        {
            if (_context.Inspects.FirstOrDefault(x => x.CarryoutDate == DateTime.Today) == null)
            {
                _context.Inspects.AddRange(new Inspect[]
                {
                    new Inspect() { TenantId = _tenantId, CarryoutDate = DateTime.Today, WorkerId = 1, ShelfId = 1, Purity = 97.12F, Remark = "标准金", CreateTime = DateTime.Now },
                    new Inspect() { TenantId = _tenantId, CarryoutDate = DateTime.Today, WorkerId = 1, ShelfId = 1, Purity = 96.32F, Remark = "来货", CreateTime = DateTime.Now.AddMinutes(5) },
                    new Inspect() { TenantId = _tenantId, CarryoutDate = DateTime.Today, WorkerId = 1, ShelfId = 2, Purity = 98.12F, Remark = "来货", CreateTime = DateTime.Now.AddMinutes(10) },
                    new Inspect() { TenantId = _tenantId, CarryoutDate = DateTime.Today, WorkerId = 2, ShelfId = 2, Purity = 95.03F, Remark = "现场", CreateTime = DateTime.Now.AddMinutes(15) },
                });
                _context.SaveChanges();
            }
        }

        private void CreateStocktakings()
        {
            if (_context.Stocktakings.FirstOrDefault(x => x.CarryoutDate == DateTime.Today) == null)
            {
                _context.Stocktakings.AddRange(new Stocktaking[]
                {
                    new Stocktaking() { TenantId = _tenantId, CarryoutDate = DateTime.Today, WorkerId = 1, ShelfId = 1, Inventory = 130400.24F, CreateTime = DateTime.Now },
                    new Stocktaking() { TenantId = _tenantId, CarryoutDate = DateTime.Today, WorkerId = 1, ShelfId = 2, Inventory = 24566.67F, CreateTime = DateTime.Now.AddMinutes(5) },
                    new Stocktaking() { TenantId = _tenantId, CarryoutDate = DateTime.Today, WorkerId = 1, ShelfId = 3, Inventory = 23458.34F, CreateTime = DateTime.Now.AddMinutes(10) },
                    new Stocktaking() { TenantId = _tenantId, CarryoutDate = DateTime.Today, WorkerId = 1, ShelfId = 4, Inventory = 23458.34F, CreateTime = DateTime.Now.AddMinutes(16) },
                });
                _context.SaveChanges();
            }
        }
    }
}
