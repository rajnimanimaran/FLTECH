using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface IEmployeeAcademyService
    {

        IEnumerable<EmployeeAcademyEntity> GetEmpAcademyByEmpId(int EmployeeId);
        EmployeeAcademyEntity GetEmpAcademyInfoById(int id);
        ResultDTO CreateEmployeeAcademy(EmployeeAcademyEntity employeeAcademyEntity);
        ResultDTO UpdateEmployeeAcademy(int AcademyId, EmployeeAcademyEntity employeeAcademyEntity);
        bool DeleteEmployeeAcademy(int AcademyId);

    }
}
