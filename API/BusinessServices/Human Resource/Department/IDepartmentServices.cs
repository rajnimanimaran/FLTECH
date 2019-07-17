using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface IDepartmentServices
    {
        DepartmentEntity GetDepartmentById(int DepartmentId);
        IEnumerable<DepartmentEntity> GetAllDepartments(int CompanyId);
        IEnumerable<DepartmentEntity> GetAllCompanyDepartment();
        IEnumerable<DepartmentEntity> GetDepartmentByCompanyId(int CompanyId);
        IEnumerable<DepartmentEntity> GetDepOfParentCompanies(GetParentListDTO obj);
        //  IEnumerable<DepartmentEntity> GetActiveDepartmentsById(int CompanyId);
        ResultDTO CreateDepartment(DepartmentEntity departmentEntity);
        ResultDTO UpdateDepartment(int DepartmentId, DepartmentEntity departmentEntity);
        bool DeleteDepartment(int DepartmentId);
        bool ToggleActiveDepartment(int DepartmentId);
    }
}

