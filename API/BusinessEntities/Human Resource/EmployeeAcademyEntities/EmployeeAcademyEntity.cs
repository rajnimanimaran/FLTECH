using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class EmployeeAcademyEntity
    {
        public int AcademyId { get; set; }
        public int CompanyId { get; set; }
        public int EmployeeId { get; set; }
        public string Graduation { get; set; }
        public string Degree { get; set; }
        public string Specialization { get; set; }
        public string University { get; set; }
        public string Percentage { get; set; }
        public string YearofPassing { get; set; }
        public string DocAttachment { get; set; }
        public string Remarks { get; set; }
       
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
      
    }

    public class EmployeeAcademyCompanyDTO
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }

    public class EEmployeeAcademyEmployeeDTO
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
    }
}
