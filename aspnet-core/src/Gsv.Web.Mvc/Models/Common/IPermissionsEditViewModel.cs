using System.Collections.Generic;
using Gsv.Roles.Dto;

namespace Gsv.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}