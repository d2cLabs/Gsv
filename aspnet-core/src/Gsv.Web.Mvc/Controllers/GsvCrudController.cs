using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.UI;
using Abp.Web.Models;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Gsv.Controllers;

namespace Gsv.Web.Controllers
{
    public abstract class GsvCrudController<TEntity, TEntityDto> : GsvCrudController<TEntity, int, TEntityDto>
        where TEntity : class, IEntity
        where TEntityDto : IEntityDto
    {
        protected GsvCrudController(IRepository<TEntity> repository)
            : base(repository)
        {
        }
    }

    public abstract class GsvCrudController<TEntity, TPrimaryKey, TEntityDto> : GsvCrudControllerBase<TEntity, TPrimaryKey, TEntityDto>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
    {
        protected GsvCrudController(IRepository<TEntity, TPrimaryKey> repository)
            : base(repository)
        {
        }

        public virtual ActionResult Index()
        {
            return View();
        }

        [DontWrapResult]
        public virtual async Task<JsonResult> GetPagedData(string id)
        {
            PagedResultDto<TEntityDto> o = await GetPagedResult(id);        // wherePhrase
            return Json(new { total = o.TotalCount, rows = o.Items });
        }

        [DontWrapResult]
        public virtual async Task<JsonResult> GetAllData()
        {
            List<TEntityDto> o = await GetListResult(null);
            return Json(new { rows = o });
        }

        [DontWrapResult]
        public virtual async Task<JsonResult> GetEdit(TPrimaryKey id)
        {
            var o = await GetEntityDto(id);
            return Json(o);
        }

        [HttpPost]
        public virtual async Task<JsonResult> Create(TEntityDto input)
        {
            try
            {
                await CreateEntity(input);
                return Json(new { result = "success", content = "记录添加成功" });
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("表操作失败", ex.Message);
            }
        }

        [HttpPost]
        public virtual async Task<JsonResult> Update(TEntityDto input)
        {
            try
            {
                await UpdateEntity(input);
                return Json(new { result = "success", content = "记录修改成功" });
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("表操作失败", ex.Message);
            }
        }

        [HttpPost]
        public virtual async Task<JsonResult> Delete(TPrimaryKey id)
        {
            try
            {
                await DeleteEntity(id);
                return Json(new { result = "success", content = "记录删除成功" });
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("表操作失败", ex.Message);
            }
        }

        [HttpPost]
        public virtual async Task<JsonResult> DeleteEntities(List<TPrimaryKey> ids)
        {
            try
            {
                foreach (TPrimaryKey id in ids) 
                {
                    await Delete(id);
                }
                return Json(new { result = "success", content = $"{ids.Count}条记录删除成功" });
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("表操作失败", ex.Message);
            }
        }

    }
}