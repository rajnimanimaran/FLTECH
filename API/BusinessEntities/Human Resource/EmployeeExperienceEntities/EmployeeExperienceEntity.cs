using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
   public class EmployeeExperienceEntity
    {

        public int ExperienceId { get; set; }
        public int CompanyId { get; set; }
        public int EmployeeId { get; set; }
        public string Organization { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public double YearsofExperience { get; set; }
        public string Remarks { get; set; }       
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
      
    }

    public class EmployeeExperienceCompanyDTO
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }

    public class EmployeeExperienceEmployeeDTO
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
    }
}
