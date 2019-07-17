using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface IEmployeeExperienceService
    {
        IEnumerable<EmployeeExperienceEntity> GetEmpExpByEmpId(int EmployeeId);        
        ResultDTO CreateEmployeeExperience(EmployeeExperienceEntity employeeExperience);
        ResultDTO UpdateEmployeeExperience(int ExperienceId, EmployeeExperienceEntity employeeExperience);
        bool DeleteEmployeeExperience(int ExperienceId);
    }
}
