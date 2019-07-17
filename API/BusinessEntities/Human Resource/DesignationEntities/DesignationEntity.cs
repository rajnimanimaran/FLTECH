using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class DesignationEntity
    {
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public int Superior { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public string DesignationCode { get; set; }
        public string SuperiorName { get; set; }

        public DesignationCompanyDTO Company { get; set; }
        public DesignationDepartmentDTO Department { get; set; }

    }

    public class DesignationCompanyDTO
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }

    public class DesignationDepartmentDTO
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }

}
