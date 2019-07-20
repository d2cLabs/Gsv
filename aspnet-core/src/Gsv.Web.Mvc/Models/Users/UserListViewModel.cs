using System.Collections.Generic;
using Gsv.Roles.Dto;
using Gsv.Users.Dto;

namespace Gsv.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
