using DataModel.Generic_Repository;
using DataModel.DBLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.UnitOfWork
{
    /// <summary>
    /// Unit of Work class responsible for DB transactions
    /// </summary>
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        #region Private member variables...
        private DbLayer _dbLayer;
        private MongoRepository _mongoRepository;
        private CMS_Context _context = null;
        private GenericRepository<User> _userRepository;
        private GenericRepository<Token> _tokenRepository;
        private GenericRepository<Area> _areaRepository;
        private GenericRepository<Action> _actionRepository;
    

        private GenericRepository<Company> _companyRepository;
        private GenericRepository<City> _cityRepository;
        private GenericRepository<Country> _countryRepository;
        private GenericRepository<Department> _departmentRepository;
        private GenericRepository<Designation> _designationRepository;
        private GenericRepository<Menu> _menuRepository;
        private GenericRepository<OrganizationLevel> _organizationLevelRepository;
        private GenericRepository<Role> _roleRepository;
        private GenericRepository<RoleMenuMapping> _roleMenuMappingRepository;
        private GenericRepository<State> _stateRepository;
        private GenericRepository<UserMenuMapping> _userMenuMappingRepository;      
        private GenericRepository<Employee> _employeeRepository;
        private GenericRepository<NatureOfBusiness> _natureOfBusinessRepository;
        private GenericRepository<OwnershipTypes> _ownershipTypesRepository;
        private GenericRepository<EmployeeAcademy> _employeeAcademyRepository;
        private GenericRepository<EmployeeAddress> _employeeAddressRepository;
        //  private GenericRepository<EmployeeDocuments> _employeeDocumentsRepository;
        private GenericRepository<EmployeeExperience> _employeeExperienceRepository;
       

        #endregion

        public UnitOfWork()
        {
            _context = new CMS_Context();
        }

        #region Public Repository Creation properties...

        /// <summary>
        /// Get/Set Property for user repository.
        /// </summary>
        public GenericRepository<User> UserRepository
        {
            get
            {
                if (this._userRepository == null)
                    this._userRepository = new GenericRepository<User>(_context);
                return _userRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for token repository.
        /// </summary>
        public GenericRepository<Token> TokenRepository
        {
            get
            {
                if (this._tokenRepository == null)
                    this._tokenRepository = new GenericRepository<Token>(_context);
                return _tokenRepository;
            }
        }

        public DbLayer DbLayer
        {
            get
            {
                if (this._dbLayer == null)
                    this._dbLayer = new DbLayer();
                return _dbLayer;
            }
        }

        public MongoRepository MongoRepository
        {
            get
            {
                if (this._mongoRepository == null)
                    this._mongoRepository = new MongoRepository();
                return _mongoRepository;
            }
        }

        public GenericRepository<Action> ActionRepository
        {
            get
            {
                if (this._actionRepository == null)
                    this._actionRepository = new GenericRepository<Action>(_context);
                return _actionRepository;
            }
        }

        public GenericRepository<Area> AreaRepository
        {
            get
            {
                if (this._areaRepository == null)
                    this._areaRepository = new GenericRepository<Area>(_context);
                return _areaRepository;
            }
        }

        public GenericRepository<City> CityRepository
        {
            get
            {
                if (this._cityRepository == null)
                    this._cityRepository = new GenericRepository<City>(_context);
                return _cityRepository;
            }
        }

        public GenericRepository<Company> CompanyRepository
        {
            get
            {
                if (this._companyRepository == null)
                    this._companyRepository = new GenericRepository<Company>(_context);
                return _companyRepository;
            }
        }

        public GenericRepository<Country> CountryRepository
        {
            get
            {
                if (this._countryRepository == null)
                    this._countryRepository = new GenericRepository<Country>(_context);
                return _countryRepository;
            }
        }

        public GenericRepository<Department> DepartmentRepository
        {
            get
            {
                if (this._departmentRepository == null)
                    this._departmentRepository = new GenericRepository<Department>(_context);
                return _departmentRepository;
            }
        }

        public GenericRepository<Designation> DesignationRepository
        {
            get
            {
                if (this._designationRepository == null)
                    this._designationRepository = new GenericRepository<Designation>(_context);
                return _designationRepository;
            }
        }

        public GenericRepository<Menu> MenuRepository
        {
            get
            {
                if (this._menuRepository == null)
                    this._menuRepository = new GenericRepository<Menu>(_context);
                return _menuRepository;
            }
        }

        public GenericRepository<OrganizationLevel> OrganizationLevelRepository
        {
            get
            {
                if (this._organizationLevelRepository == null)
                    this._organizationLevelRepository = new GenericRepository<OrganizationLevel>(_context);
                return _organizationLevelRepository;
            }
        }

        public GenericRepository<Role> RoleRepository
        {
            get
            {
                if (this._roleRepository == null)
                    this._roleRepository = new GenericRepository<Role>(_context);
                return _roleRepository;
            }
        }

        public GenericRepository<RoleMenuMapping> RoleMenuMappingRepository
        {
            get
            {
                if (this._roleMenuMappingRepository == null)
                    this._roleMenuMappingRepository = new GenericRepository<RoleMenuMapping>(_context);
                return _roleMenuMappingRepository;
            }
        }

        public GenericRepository<State> StateRepository
        {
            get
            {
                if (this._stateRepository == null)
                    this._stateRepository = new GenericRepository<State>(_context);
                return _stateRepository;
            }
        }

        public GenericRepository<UserMenuMapping> UserMenuMappingRepository
        {
            get
            {
                if (this._userMenuMappingRepository == null)
                    this._userMenuMappingRepository = new GenericRepository<UserMenuMapping>(_context);
                return _userMenuMappingRepository;
            }
        }

        public GenericRepository<Employee> EmployeeRepository
        {
            get
            {
                if (this._employeeRepository == null)
                    this._employeeRepository = new GenericRepository<Employee>(_context);
                return _employeeRepository;
            }
        }     

        public GenericRepository<OwnershipTypes> OwnershipTypesRepository
        {
            get
            {
                if (this._ownershipTypesRepository == null)
                    this._ownershipTypesRepository = new GenericRepository<OwnershipTypes>(_context);
                return _ownershipTypesRepository;
            }
        }

        public GenericRepository<NatureOfBusiness> NatureOfBusinessRepository
        {
            get
            {
                if (this._natureOfBusinessRepository == null)
                    this._natureOfBusinessRepository = new GenericRepository<NatureOfBusiness>(_context);
                return _natureOfBusinessRepository;
            }
        }

        public GenericRepository<EmployeeExperience> EmployeeExperienceRepository
        {
            get
            {
                if (this._employeeExperienceRepository == null)
                    this._employeeExperienceRepository = new GenericRepository<EmployeeExperience>(_context);
                return _employeeExperienceRepository;
            }
        }

        //public GenericRepository<EmployeeDocuments> EmployeeDocumentsRepository
        //{
        //    get
        //    {
        //        if (this._employeeDocumentsRepository == null)
        //            this._employeeDocumentsRepository = new GenericRepository<EmployeeDocuments>(_context);
        //        return _employeeDocumentsRepository;
        //    }
        //}

        public GenericRepository<EmployeeAddress> EmployeeAddressRepository
        {
            get
            {
                if (this._employeeAddressRepository == null)
                    this._employeeAddressRepository = new GenericRepository<EmployeeAddress>(_context);
                return _employeeAddressRepository;
            }
        }


        public GenericRepository<EmployeeAcademy> EmployeeAcademyRepository
        {
            get
            {
                if (this._employeeAcademyRepository == null)
                    this._employeeAcademyRepository = new GenericRepository<EmployeeAcademy>(_context);
                return _employeeAcademyRepository;
            }
        }

       
        #endregion

        #region Public member methods...
        /// <summary>
        /// Save method.
        /// </summary>
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }

        #endregion

        #region Implementing IDiosposable...

        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
