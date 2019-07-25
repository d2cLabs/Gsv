using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Gsv.Roles.Dto;

namespace Gsv.Roles
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PagedRoleResultRequestDto, CreateRoleDto, RoleDto>
    {
        Task<ListResultDto<PermissionDto>> GetAllPermissions();

        Task<GetRoleForEditOutput> GetRoleForEdit(EntityDto input);

        Task<ListResultDto<RoleListDto>> GetRolesAsync(GetRolesInput input);
        
        // Thle Follows is Host Tenant Manage
        Task CreateTenantRole(string tenancyName, CreateRoleDto input);
        Task UpdateRolePermissions(string tenancyName, UpdateRolePermissionsInput input);
        
        Task<ListResultDto<RoleListDto>> GetTenantRoles(string tenancyName);
        Task<List<string>> GetRolePermissionNames(string tenancyName, int roleId);
    }
}
