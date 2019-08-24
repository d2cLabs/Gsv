using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq;
using Gsv.Tasks.Dto;

namespace Gsv.Tasks
{
    [AbpAuthorize]
    public class InStockAppService : GsvAppServiceBase, IInStockAppService
    {
        public IAsyncQueryableExecuter AsyncQueryableExecuter { get; set; }
        private readonly IRepository<InStock> _repository;

        public InStockAppService(IRepository<InStock> repository)
        {
            _repository = repository;
        }

        public async Task<List<InStockDto>> GetInStocksAsync(int placeId, int categoryId)
        {
            var query = _repository.GetAllIncluding(x => x.PlaceShelf, x=> x.PlaceShelf.CargoType, x => x.CreateWorker);
            query = query.Where(x => x.CarryoutDate == DateTime.Today && x.PlaceShelf.PlaceId == placeId && x.PlaceShelf.CargoType.CategoryId == categoryId);
            var entities = await AsyncQueryableExecuter.ToListAsync(query);
            
            return ObjectMapper.Map<List<InStockDto>>(entities);
        }
                
    }

}