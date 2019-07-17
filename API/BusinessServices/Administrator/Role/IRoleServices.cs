using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface IRoleServices
    {
        RoleEntity GetRoleById(int RoleId);
        IEnumerable<RoleEntity> GetAllRoles();
        IEnumerable<RoleEntity> GetActiveRolesById();
        ResultDTO CreateRole(RoleEntity RoleEntity);
        ResultDTO UpdateRole(int RoleId, RoleEntity RoleEntity);
        bool DeleteRole(int RoleId);
        bool ToggleActiveRole(int RoleId);
    }
}
