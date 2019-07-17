using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface IOrganizationLevelServices
    {
        OrganizationLevelEntity GetOrganizationLevelById(int OrganizationLevelId);
        IEnumerable<OrganizationLevelEntity> GetAllOrganizationLevel();
        ResultDTO CreateOrganizationLevel(OrganizationLevelEntity OrganizationLevelEntity);
        ResultDTO UpdateOrganizationLevel(int OrganizationLevelId, OrganizationLevelEntity OrganizationLevelEntity);
        bool DeleteOrganizationLevel(int OrganizationLevelId);
        bool ToggleActiveOrganizationLevel(int OrganizationLevelId);
    }
}
