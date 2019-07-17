using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class DepartmentEntity
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public int CompanyId { get; set; }
        public int ParentId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public string ParentName { get; set; }


        public DepartmentCompanyDTO Company { get; set; }
    }

    public class DepartmentCompanyDTO
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}