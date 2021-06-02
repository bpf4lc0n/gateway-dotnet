using Abp.Authorization;
using Gateway.Authorization.Roles;
using Gateway.Authorization.Users;

namespace Gateway.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
