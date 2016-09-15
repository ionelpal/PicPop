using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using PicPop.BusinessLogic;

namespace PicPop
{
    public static class IdentityExt
    {
        public static AspNetUser GetUser(this IIdentity identity, bool profile = false)
        {
            return UsersHelper.Get(identity.Name, profile);
        }

        public static int GetRole(this IIdentity identity)
        {
            var user = UsersHelper.GetRoles(identity.Name);
            var role = user.AspNetRoles.FirstOrDefault();
            int roleID = 0;
            if (int.TryParse(role.Id, out roleID))
                return roleID;


            return 0;
        }
    }
}
