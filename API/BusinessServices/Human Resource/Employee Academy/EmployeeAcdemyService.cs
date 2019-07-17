using AutoMapper;
using BusinessEntities;
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
    public class EmployeeAcademyService : IEmployeeAcademyService
    {

        private readonly IUnitOfWork _unitOfWork;

        public EmployeeAcademyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<BusinessEntities.EmployeeAcademyEntity> GetEmpAcademyByEmpId(int EmployeeId)
        {            
            var employeeList = _unitOfWork.EmployeeAcademyRepository.GetMany(a => a.EmployeeId == EmployeeId).ToList();
            if (employeeList.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    //cfg.CreateMap<Company, EmployeeCompanyDTO>();
                    cfg.CreateMap<Employee, EmployeeEntity>();
                    cfg.CreateMap<EmployeeAcademy, EmployeeAcademyEntity>();
                });
                IMapper mapper = config.CreateMapper();

                var employeemodel = mapper.Map<List<EmployeeAcademy>, List<EmployeeAcademyEntity>>(employeeList);

                return employeemodel;
            }
            return null;
        }


        public ResultDTO CreateEmployeeAcademy(EmployeeAcademyEntity employeeAcademyEntity)
        {
            var result = new ResultDTO { IsSuccess = false };


            using (var scope = new TransactionScope())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<EmployeeAcademyEntity, EmployeeAcademy>();
                });

                IMapper mapper = config.CreateMapper();

                var employeemodel = mapper.Map<EmployeeAcademyEntity, EmployeeAcademy>(employeeAcademyEntity);


                employeemodel.CreatedBy = employeeAcademyEntity.CreatedBy;
                employeemodel.CreatedOn = DateTime.Now;
                employeemodel.ModifiedBy = employeeAcademyEntity.CreatedBy;
                employeemodel.ModifiedOn = DateTime.Now;
                employeemodel.IsActive = true;

                _unitOfWork.EmployeeAcademyRepository.Insert(employeemodel);
                _unitOfWork.Save();
                scope.Complete();
                result.IsSuccess = true;
                result.Message = "Inserted Employee Academy Successfully";
            }

            return result;
        }

        public ResultDTO UpdateEmployeeAcademy(int AcademyId, BusinessEntities.EmployeeAcademyEntity employeeAcademyEntity)
        {
            var result = new ResultDTO { IsSuccess = false };
            if (employeeAcademyEntity != null)
            {

                using (var scope = new TransactionScope())
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<EmployeeAcademy, EmployeeAcademyEntity>();

                    });


                    var empAcademy = _unitOfWork.EmployeeAcademyRepository.GetByID(AcademyId);
                    if (empAcademy != null)
                    {

                        empAcademy.IsActive = employeeAcademyEntity.IsActive;
                        empAcademy.ModifiedBy = employeeAcademyEntity.ModifiedBy;
                        empAcademy.ModifiedOn = DateTime.Now;


                        _unitOfWork.EmployeeAcademyRepository.Update(empAcademy);
                        _unitOfWork.Save();
                        scope.Complete();
                        result.IsSuccess = true;
                        result.Message = "Updated Employee Successfully";
                    }
                }

            }
            return result;
        }

        public bool DeleteEmployeeAcademy(int AcademyId)
        {
            var success = false;
            if (AcademyId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var empAcademy = _unitOfWork.EmployeeAcademyRepository.GetByID(AcademyId);
                    if (empAcademy != null)
                    {

                        _unitOfWork.EmployeeAcademyRepository.Delete(empAcademy);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public EmployeeAcademyEntity GetEmpAcademyInfoById(int id)
        {
            var academyDetail = _unitOfWork.EmployeeAcademyRepository.GetMany(a => a.AcademyId == id).FirstOrDefault();
            if (academyDetail != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    //cfg.CreateMap<Company, EmployeeCompanyDTO>();
                    cfg.CreateMap<Employee, EmployeeEntity>();
                    cfg.CreateMap<EmployeeAcademy, EmployeeAcademyEntity>();
                });
                IMapper mapper = config.CreateMapper();

                var employeeModel = mapper.Map<EmployeeAcademy, EmployeeAcademyEntity>(academyDetail);

                return employeeModel;
            }
            return null;
        }
    }
}
