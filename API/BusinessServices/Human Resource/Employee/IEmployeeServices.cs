using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface IEmployeeServices
    {
        IEnumerable<EmployeeEntity> GetAllEmployee(int employeeId);
        bool CreateEmployee(EmployeeEntity obj);

        //IEnumerable<EmpByDesigOutputDTO> GetAllCompanyEmployees(int DesignationId);
        //EmployeeEntity GetEmployeeById(int EmployeeId);
        //int CreateEmployee(EmployeeEntity employeeEntity);
        //bool UpdateEmployee(int EmployeeId, EmployeeEntity employeeEntity, EmployeeAddressEntity empaddrsEntity);
        //bool DeleteEmployee(int EmployeeId);
        //bool ToggleActiveEmployee(int EmployeeId);
        //bool EmployeeProPicUpload(EmpProPicUploadInputDTO obj);
    }
}
