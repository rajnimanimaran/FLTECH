using AutoMapper;
using BusinessEntities;
using BusinessServices;
using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessServices
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public BusinessEntities.DepartmentEntity GetDepartmentById(int DepartmentId)
        {
            var department = _unitOfWork.DepartmentRepository.GetByID(DepartmentId);
            if (department != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Company, DepartmentCompanyDTO>();
                    cfg.CreateMap<Department, DepartmentEntity>();

                });

                IMapper mapper = config.CreateMapper();

                var departmentModel = mapper.Map<Department, DepartmentEntity>(department);
                return departmentModel;
            }
            return null;
        }

        public IEnumerable<BusinessEntities.DepartmentEntity> GetDepartmentByCompanyId(int CompanyId)
        {
            var departmentList = _unitOfWork.DepartmentRepository.GetMany(a => a.CompanyId == CompanyId && a.IsActive == true).ToList();
            if (departmentList.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Company, DepartmentCompanyDTO>();
                    cfg.CreateMap<Department, DepartmentEntity>();

                });

                IMapper mapper = config.CreateMapper();
                var DeptModel = mapper.Map<List<Department>, List<DepartmentEntity>>(departmentList);
                return DeptModel;
            }
            return new List<DepartmentEntity>();
        }

        /// <summary>
        /// Returns all departments for given companyId and its sub companies departments
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public IEnumerable<BusinessEntities.DepartmentEntity> GetAllDepartments(int companyId)
        {
            var companyList = _unitOfWork.CompanyRepository.GetAll();

            var userCompanies = new List<Company>();
            userCompanies.Add(companyList.First(c => c.CompanyId == companyId));
            userCompanies.AddRange(CommonService.getSubCompanyList(companyList, companyId));

            var compIdLsit = from company in userCompanies
                             select (company.CompanyId);

            var allDepartments = _unitOfWork.DepartmentRepository.GetAll().OrderByDescending(x => x.DepartmentId).ToList();
            var departments = allDepartments.Where(d => compIdLsit.Contains(d.CompanyId)).ToList();
            if (departments.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Company, DepartmentCompanyDTO>();
                    cfg.CreateMap<Department, DepartmentEntity>();
                });
                IMapper mapper = config.CreateMapper();
                var departmentList = mapper.Map<List<Department>, List<DepartmentEntity>>(departments);
                foreach (var d in departmentList)
                {
                    if (d.ParentId > 0)
                        d.ParentName = allDepartments.Where(dep => dep.DepartmentId == d.ParentId).First().DepartmentName;
                    else
                        d.ParentName = String.Empty;
                }
                return departmentList;
            }
            return null;
        }

        public IEnumerable<DepartmentEntity> GetAllCompanyDepartment()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll().ToList();
            if (departments.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Company, DepartmentCompanyDTO>();
                    cfg.CreateMap<Department, DepartmentEntity>();
                });
                IMapper mapper = config.CreateMapper();

                var departmentList = mapper.Map<List<Department>, List<DepartmentEntity>>(departments).Where(dept => dept.IsActive == true);

                //foreach (var item in employeeList)
                //{
                //    item.ReportingToName = (from t in employee where t.EmployeeId == item.ReportingTo select t.FirstName).FirstOrDefault();
                //}

                return departmentList;
            }

            return null;
        }

        public ResultDTO CreateDepartment(BusinessEntities.DepartmentEntity departmentEntity)
        {
            //CodeGenerator code = new CodeGenerator();

            //var res = String.Empty;
            //var result = _unitOfWork.DepartmentRepository.GetMany(x => x.DepartmentId == departmentEntity.DepartmentId);
            //if (result.Count() > 0)
            //{
            //    res = (from c in result
            //           orderby c.DepartmentId descending
            //           select c).Take(1).First().DepartmentCode;
            //}
            //var last = res != null ? code.NumberFromExcelColumn(res) : 0;
            //var DepartmentCode = code.GenerateSequence(last);

            var result = new ResultDTO { IsSuccess = false };

            var isExist = _unitOfWork.DepartmentRepository.GetManyQueryable(c => c.DepartmentName.ToLower() == departmentEntity.DepartmentName.ToLower() && c.CompanyId == departmentEntity.CompanyId).Count() > 0;
            if (!isExist)
            {

                using (var scope = new TransactionScope())
                {
                    var department = new DataModel.Department
                    {

                        DepartmentName = departmentEntity.DepartmentName,
                        DepartmentCode = departmentEntity.DepartmentCode,
                        CompanyId = departmentEntity.CompanyId,
                        ParentId = departmentEntity.ParentId,
                        IsActive = true,
                        CreatedBy = departmentEntity.CreatedBy,
                        CreatedOn = DateTime.Now,
                        ModifiedBy = departmentEntity.CreatedBy,
                        ModifiedOn = DateTime.Now,
                    };
                    _unitOfWork.DepartmentRepository.Insert(department);
                    _unitOfWork.Save();
                    scope.Complete();

                    result.IsSuccess = true;
                    result.Message = "Inserted Department Successfully";

                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "Department name already exist";
            }
            return result;
        }

        public ResultDTO UpdateDepartment(int DepartmentId, BusinessEntities.DepartmentEntity departmentEntity)
        {
            var result = new ResultDTO { IsSuccess = false };

            if (departmentEntity != null)
            {
                var isExist = _unitOfWork.DepartmentRepository.GetManyQueryable(c => c.DepartmentName.ToLower() == departmentEntity.DepartmentName.ToLower()&& c.DepartmentCode == departmentEntity.DepartmentCode && c.CompanyId == departmentEntity.CompanyId).Count() > 0;
                if (!isExist)
                {
                    using (var scope = new TransactionScope())
                    {
                        var department = _unitOfWork.DepartmentRepository.GetByID(DepartmentId);
                        if (departmentEntity != null)
                        {
                            department.DepartmentId = departmentEntity.DepartmentId;
                            department.DepartmentName = departmentEntity.DepartmentName;
                            department.DepartmentCode = departmentEntity.DepartmentCode;
                            department.CompanyId = departmentEntity.CompanyId;
                            department.ParentId = departmentEntity.ParentId;
                            department.IsActive = true;
                            department.ModifiedBy = departmentEntity.ModifiedBy;
                            department.ModifiedOn = DateTime.Now;
                            _unitOfWork.DepartmentRepository.Update(department);
                            _unitOfWork.Save();
                            scope.Complete();

                            result.IsSuccess = true;
                            result.Message = "Updated Department Successfully";
                        }
                    }
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Designation name already exist";
                }
            }
            return result;
        }

        public bool DeleteDepartment(int DepartmentId)
        {
            var isSuccess = false;
            if (DepartmentId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var deps = _unitOfWork.DepartmentRepository.GetMany(d => d.ParentId == DepartmentId);
                    if (!deps.Any())
                    {
                        var action = _unitOfWork.DepartmentRepository.GetByID(DepartmentId);
                        if (DepartmentId != 0)
                        {
                            _unitOfWork.DepartmentRepository.Delete(action);
                            _unitOfWork.Save();
                            scope.Complete();
                            isSuccess = true;
                        }
                    }
                }

            }
            return isSuccess;
        }

        public bool ToggleActiveDepartment(int DepartmentId)
        {
            var success = false;
            if (DepartmentId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var Dep = _unitOfWork.DepartmentRepository.GetByID(DepartmentId);
                    if (Dep != null)
                    {
                        Dep.IsActive = !Dep.IsActive;
                        Dep.ModifiedBy = 1;
                        Dep.ModifiedOn = DateTime.Now;
                        _unitOfWork.DepartmentRepository.Update(Dep);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public IEnumerable<DepartmentEntity> GetActiveDepartmentsById(int CompanyId)
        {
            return GetAllDepartments(CompanyId).Where(d => d.IsActive == true).ToList();
        }

        public IEnumerable<DepartmentEntity> GetDepOfParentCompanies(GetParentListDTO obj)
        {
            var allCompanies = _unitOfWork.CompanyRepository.GetAll().Where(c => c.IsActive == true);

            var resCompanies = new List<Company>();
            resCompanies.Add(allCompanies.First(c => c.CompanyId == obj.CompanyId));
            resCompanies.AddRange(CommonService.getParentsList(allCompanies, resCompanies.First().ParentCompany, obj.UserCompanyId));

            var compIdLsit = from company in resCompanies
                             select (company.CompanyId);

            var allDepartments = _unitOfWork.DepartmentRepository.GetAll();
            var departments = allDepartments.Where(d => compIdLsit.Contains(d.CompanyId)).ToList();
            if (departments.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Company, DepartmentCompanyDTO>();
                    cfg.CreateMap<Department, DepartmentEntity>();
                });
                IMapper mapper = config.CreateMapper();
                var departmentList = mapper.Map<List<Department>, List<DepartmentEntity>>(departments);
                foreach (var d in departmentList)
                {
                    if (d.ParentId > 0)
                        d.ParentName = allDepartments.Where(dep => dep.DepartmentId == d.ParentId).First().DepartmentName;
                    else
                        d.ParentName = String.Empty;
                }
                return departmentList;
            }
            return null;
        }
    }
}
