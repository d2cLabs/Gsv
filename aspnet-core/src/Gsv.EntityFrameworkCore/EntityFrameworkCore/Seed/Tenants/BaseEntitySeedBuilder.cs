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
using Gsv.Staffing;
using Gsv.Objects;

namespace Gsv.EntityFrameworkCore.Seed.Tenants
{
    public class BaseEntitySeedBuilder
    {
        private readonly GsvDbContext _context;
        private readonly int _tenantId;

        public BaseEntitySeedBuilder(GsvDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            // Types
            CreateCategories();
            CreateSources();

            // Objects
            CreateCapitals();
            CreatePlaces();
            CreateObjects();
            CreateCargoTypes();
            CreatePlaceShelves();

            // Staffing
            CreateWorkers();
        }

        private void CreateCategories()
        {
            if (_context.Categories.Count() == 0)
            {
                _context.Categories.AddRange(new Category[]
                {
                    new Category() { TenantId = _tenantId, Cn = "01", Name = "Au999", UnitName = "克", CurrentPrice = 312.23F },
                    new Category() { TenantId = _tenantId, Cn = "02", Name = "铂金", UnitName = "克", CurrentPrice = 112.23F }
                });
                _context.SaveChanges();
            }
        }

        private void CreateSources()
        {
            if (_context.Sources.Count() == 0)
            {
                _context.Sources.AddRange(new Source[] 
                {
                    new Source() { TenantId = _tenantId, Cn = "01", Name = "自购" },
                    new Source() { TenantId = _tenantId, Cn = "02", Name = "回货" },
                });
                _context.SaveChanges();
            }
        }

        // Objects
        private void CreateCapitals()
        {
            if (_context.Capitals.Count() == 0)
            {
                _context.Capitals.AddRange(new Capital[] 
                {
                    new Capital() { TenantId = _tenantId, Cn = "A01", Name = "深圳平安银行" },
                    new Capital() { TenantId = _tenantId, Cn = "B01", Name = "山东金控投资" },
                });
                _context.SaveChanges();
            }            
        }

        private void CreatePlaces()
        {
            if (_context.Places.Count() == 0)
            {
                _context.Places.AddRange(new Place[] 
                {
                    new Place() { TenantId = _tenantId, Cn = "1001", Name = "意大隆展厅" },
                    new Place() { TenantId = _tenantId, Cn = "2001", Name = "佰利德工厂" },
                });
                _context.SaveChanges();
            }            
        }

        private void CreateObjects()
        {
            if (_context.Objects.Count() == 0)
            {
                _context.Objects.AddRange(new Object[] 
                {
                    new Object() { TenantId = _tenantId, CapitalId = 1, PlaceId = 1, CategoryId = 1, Quantity = 120000, RiskRatio = 125000  },
                    new Object() { TenantId = _tenantId, CapitalId = 1, PlaceId = 1, CategoryId = 2, Quantity = 15000, RiskRatio = 16500  },
                    new Object() { TenantId = _tenantId, CapitalId = 2, PlaceId = 1, CategoryId = 1, Quantity = 98980, RiskRatio = 102000  },
                });
                _context.SaveChanges();
            }            
        }
        
        private void CreateCargoTypes()
        {
            if (_context.CargoTypes.Count() == 0)
            {
                _context.CargoTypes.AddRange(new CargoType[] 
                {
                    new CargoType() { TenantId = _tenantId, PlaceId = 1, CategoryId = 1, TypeName = "饰金", Ratio = 0.90F },
                    new CargoType() { TenantId = _tenantId, PlaceId = 1, CategoryId = 1, TypeName = "K金", Ratio = 0.75F },
                    new CargoType() { TenantId = _tenantId, PlaceId = 1, CategoryId = 2, TypeName = "铂金饰品", Ratio = 0.92F },
                    new CargoType() { TenantId = _tenantId, PlaceId = 2, CategoryId = 1, TypeName = "原料金", Ratio = 1.0F },
                });
                _context.SaveChanges();
            }            
        }

        private void CreatePlaceShelves()
        {
            if (_context.PlaceShelves.Count() == 0)
            {
                _context.PlaceShelves.AddRange(new PlaceShelf[] 
                {
                    new PlaceShelf() { TenantId = _tenantId, PlaceId = 1, CargoTypeId = 1, Name = "饰金柜台" },
                    new PlaceShelf() { TenantId = _tenantId, PlaceId = 1, CargoTypeId = 2, Name = "K金柜台" },
                    new PlaceShelf() { TenantId = _tenantId, PlaceId = 1, CargoTypeId = 3, Name = "铂金柜台" },

                    new PlaceShelf() { TenantId = _tenantId, PlaceId = 2, CargoTypeId = 4, Name = "机加" },
                    new PlaceShelf() { TenantId = _tenantId, PlaceId = 2, CargoTypeId = 4, Name = "五组" },
                    new PlaceShelf() { TenantId = _tenantId, PlaceId = 2, CargoTypeId = 4, Name = "八组" },
                });
                _context.SaveChanges();
            }            
        }

        // Staffings
        private void CreateWorkers()
        {
            if (_context.Workers.Count() == 0)
            {
                _context.Workers.AddRange(new Worker[] 
                {
                    new Worker { TenantId = _tenantId, Cn = "91493", Name = "李涛", Password = "123456", PlaceList = "1001" },
                    new Worker { TenantId = _tenantId, Cn = "12569", Name = "陈东", Password = "123456", PlaceList = "1001" },
                    new Worker { TenantId = _tenantId, Cn = "65273", Name = "田家铭", Password = "123456", PlaceList = "1001" },
                    new Worker { TenantId = _tenantId, Cn = "64122", Name = "任国锋", Password = "123456", PlaceList = "2001" },
                    new Worker { TenantId = _tenantId, Cn = "65196", Name = "叶海平", Password = "123456", PlaceList = "1001" },
                    new Worker { TenantId = _tenantId, Cn = "12683", Name = "章涛", Password = "123456", PlaceList = "1001" },
                    new Worker { TenantId = _tenantId, Cn = "90005", Name = "测试", Password = "123456", PlaceList = "1001|2001" },
                });
                _context.SaveChanges();
            }
        }
    }
}
