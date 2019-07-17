using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class OrganizationLevelEntity
    {
        public string Code { get; set; }
        public int OrganizationLevelId { get; set; }
        public string LevelName { get; set; }
        public int Parent { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ParentName { get; set; }

    }
}
