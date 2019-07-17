using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class MappingEntity
    {
        public int MRPId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string RawMaterialId { get; set; }
        public string RawMaterialName { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }

    }
}
