using DataModel.DBLayer;
using DataModel.Generic_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.UnitOfWork
{
    public interface IUnitOfWork
    {
        DbLayer DbLayer { get; }
        MongoRepository MongoRepository { get; }
        GenericRepository<User> UserRepository { get; }
        GenericRepository<Token> TokenRepository { get; }
        GenericRepository<Action> ActionRepository { get; }
        GenericRepository<Area> AreaRepository { get; }
        GenericRepository<City> CityRepository { get; }
        GenericRepository<Company> CompanyRepository { get; }
        GenericRepository<Country> CountryRepository { get; }
        GenericRepository<Department> DepartmentRepository { get; }
        GenericRepository<Designation> DesignationRepository { get; }
        GenericRepository<Menu> MenuRepository { get; }
        GenericRepository<OrganizationLevel> OrganizationLevelRepository { get; }
        GenericRepository<Role> RoleRepository { get; }
        GenericRepository<RoleMenuMapping> RoleMenuMappingRepository { get; }
        GenericRepository<State> StateRepository { get; }
        GenericRepository<UserMenuMapping> UserMenuMappingRepository { get; }      
        GenericRepository<Employee> EmployeeRepository { get; }  
        GenericRepository<OwnershipTypes> OwnershipTypesRepository { get; }
        GenericRepository<NatureOfBusiness> NatureOfBusinessRepository { get; }
        GenericRepository<EmployeeAcademy> EmployeeAcademyRepository { get; }
        GenericRepository<EmployeeAddress> EmployeeAddressRepository { get; }
        // GenericRepository<EmployeeDocuments> EmployeeDocumentsRepository { get; }
        GenericRepository<EmployeeExperience> EmployeeExperienceRepository { get; }
       


        /// <summary>
        /// Save method.
        /// </summary>
        void Save();
    }
}
