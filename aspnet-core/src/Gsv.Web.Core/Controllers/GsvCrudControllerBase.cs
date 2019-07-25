using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Entities;
using Abp.Linq;
using System.Collections.Generic;

namespace Gsv.Controllers
{
    public abstract class GsvCrudControllerBase<TEntity, TEntityDto> : GsvCrudControllerBase<TEntity, int, TEntityDto>
        where TEntity : class, IEntity
        where TEntityDto : IEntityDto
    {
        protected GsvCrudControllerBase(IRepository<TEntity> repository)
            : base(repository)
        {

        }
    }

    public abstract class GsvCrudControllerBase<TEntity, TPrimaryKey, TEntityDto> : GsvControllerBase
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
    {
        public IAsyncQueryableExecuter AsyncQueryableExecuter { get; set; }
        protected readonly IRepository<TEntity, TPrimaryKey> _repository;

        protected GsvCrudControllerBase(IRepository<TEntity, TPrimaryKey> repository)
        {
            _repository = repository;
        }

        protected async Task<List<TEntityDto>> GetListResult(string wherePhrase)
        {
            var query = wherePhrase == null ? _repository.GetAll() : _repository.GetAll().Where(wherePhrase);
            query = query.OrderBy(GetSorting());                               // Applying Sorting
            var entities = await AsyncQueryableExecuter.ToListAsync(query);

            return new List<TEntityDto>(entities.Select(MapToEntityDto).ToList());
        }
        protected async Task<PagedResultDto<TEntityDto>> GetPagedResult(string wherePhrase)
        {
            var query = wherePhrase == null ? _repository.GetAll() : _repository.GetAll().Where(wherePhrase);
            var input = GetPagedInput();

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            if (!string.IsNullOrWhiteSpace(input.Sorting))
                query = query.OrderBy(input.Sorting);                               // Applying Sorting
            query = query.Skip(input.SkipCount).Take(input.MaxResultCount);     // Applying Paging

            var entities = await AsyncQueryableExecuter.ToListAsync(query);

            return new PagedResultDto<TEntityDto>(
                totalCount,
                entities.Select(MapToEntityDto).ToList()
            );
        }

        protected async Task<TEntityDto> GetEntityDto(TPrimaryKey id)
        {
            var entity = await _repository.GetAsync(id);
            return MapToEntityDto(entity);
        }
        
        protected async Task<TEntityDto> CreateEntity(TEntityDto input)
        {
            var entity = MapToEntity(input);

            await _repository.InsertAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }

        protected async Task<TEntityDto> UpdateEntity(TEntityDto input)
        {
            var entity = await _repository.GetAsync(input.Id);

            MapToEntity(input, entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }

        protected async Task<TEntityDto> DeleteEntity(TPrimaryKey id)
        {
            var entity = await _repository.GetAsync(id);
            await _repository.DeleteAsync(id);
            return MapToEntityDto(entity);
        }

        #region private methods

        /// <summary>
        /// Maps <see cref="TEntity"/> to <see cref="TEntityDto"/>.
        /// It uses <see cref="IObjectMapper"/> by default.
        /// It can be overrided for custom mapping.
        /// </summary>
        private TEntityDto MapToEntityDto(TEntity entity)
        {
            return ObjectMapper.Map<TEntityDto>(entity);
        }

        /// <summary>
        /// Maps <see cref="TEntityDto"/> to <see cref="TEntity"/> to create a new entity.
        /// It uses <see cref="IObjectMapper"/> by default.
        /// It can be overrided for custom mapping.
        /// </summary>
        private TEntity MapToEntity(TEntityDto input)
        {
            return ObjectMapper.Map<TEntity>(input);
        }

        /// <summary>
        /// Maps <see cref="TUpdateInput"/> to <see cref="TEntity"/> to update the entity.
        /// It uses <see cref="IObjectMapper"/> by default.
        /// It can be overrided for custom mapping.
        /// </summary>
        protected virtual void MapToEntity(TEntityDto input, TEntity entity)
        {
            ObjectMapper.Map(input, entity);
        }

        private PagedAndSortedResultRequestDto GetPagedInput()
        {
            PagedAndSortedResultRequestDto input = new PagedAndSortedResultRequestDto();
            input.Sorting = GetSorting();
            input.MaxResultCount = int.Parse(Request.Form["rows"]);
            input.SkipCount = (int.Parse(Request.Form["page"]) - 1) * input.MaxResultCount;
            return input;
        }

        private string GetSorting()
        {
            return $"{Request.Form["sort"]} {Request.Form["order"]}";
        }
        #endregion
    }
}