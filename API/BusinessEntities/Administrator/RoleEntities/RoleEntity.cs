using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class RoleEntity
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int OrganizationLevelId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        public OrganizationLevelEntity OrganizationLevel { get; set; }
    }
}
