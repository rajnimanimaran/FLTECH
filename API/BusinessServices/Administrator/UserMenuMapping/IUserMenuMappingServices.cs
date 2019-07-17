using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface IUserMenuMappingServices
    {
        // RoleMenuMappingEntity GetRoleMenuMappingById(int RoleMenuMappingId);
        // IEnumerable<RoleMenuMappingEntity> GetMenuMappingByRoleId(int roleId);

        bool CreateUserMenuMapping(long userId, IEnumerable<BusinessEntities.MenuItemsEntity> menuMapDetails);

        IEnumerable<BusinessEntities.MenuItemsEntity> GetUserMenuMapDetails(long userId);
    }
}
