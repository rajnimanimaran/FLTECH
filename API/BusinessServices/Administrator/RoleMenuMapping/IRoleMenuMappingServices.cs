using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface IRoleMenuMappingServices
    {
        // RoleMenuMappingEntity GetRoleMenuMappingById(int RoleMenuMappingId);
        // IEnumerable<RoleMenuMappingEntity> GetMenuMappingByRoleId(int roleId);

        bool CreateRoleMenuMapping(int roleId, IEnumerable<BusinessEntities.MenuItemsEntity> menuMapDetails);

        IEnumerable<BusinessEntities.MenuItemsEntity> GetRoleMenuMapDetails(int roleId);
    }
}
