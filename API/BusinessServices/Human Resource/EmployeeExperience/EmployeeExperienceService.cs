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
    public class EmployeeExperienceService : IEmployeeExperienceService
    {

        private readonly IUnitOfWork _unitOfWork;

        public EmployeeExperienceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<BusinessEntities.EmployeeExperienceEntity> GetEmpExpByEmpId(int EmployeeId)
        {
            var employeeList = _unitOfWork.EmployeeExperienceRepository.GetMany(a => a.EmployeeId == EmployeeId).ToList();
            if (employeeList.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    //cfg.CreateMap<Company, EmployeeCompanyDTO>();
                    cfg.CreateMap<Employee, EmployeeEntity>();
                    cfg.CreateMap<EmployeeExperience, EmployeeExperienceEntity>();
                });
                IMapper mapper = config.CreateMapper();
                var employeemodel = mapper.Map<List<EmployeeExperience>, List<EmployeeExperienceEntity>>(employeeList);

                return employeemodel;
            }
            return null;
        }

        public BusinessEntities.ResultDTO CreateEmployeeExperience(EmployeeExperienceEntity employeeExperience)
        {
            var result = new ResultDTO { IsSuccess = false };

            using (var scope = new TransactionScope())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<EmployeeExperienceEntity, EmployeeExperience>();
                });
                IMapper mapper = config.CreateMapper();
                var expModel = mapper.Map<EmployeeExperienceEntity, EmployeeExperience>(employeeExperience);

                expModel.CreatedBy = employeeExperience.CreatedBy;
                expModel.CreatedOn = DateTime.Now;
                expModel.ModifiedBy = employeeExperience.CreatedBy;
                expModel.ModifiedOn = DateTime.Now;
                expModel.IsActive = true;

                _unitOfWork.EmployeeExperienceRepository.Insert(expModel);
                _unitOfWork.Save();
                scope.Complete();
                result.IsSuccess = true;
                result.Message = "Inserted Employee Experience Successfully";
            }
            return result;
        }

        public BusinessEntities.ResultDTO UpdateEmployeeExperience(int ExperienceId, BusinessEntities.EmployeeExperienceEntity employeeExperience)
        {
            var result = new ResultDTO { IsSuccess = false };
            if (employeeExperience != null)
            {

                using (var scope = new TransactionScope())
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<EmployeeExperience, EmployeeExperienceEntity>();

                    });


                    var empExp = _unitOfWork.EmployeeExperienceRepository.GetByID(ExperienceId);
                    if (empExp != null)
                    {

                        empExp.IsActive = employeeExperience.IsActive;
                        empExp.ModifiedBy = employeeExperience.ModifiedBy;
                        empExp.ModifiedOn = DateTime.Now;


                        _unitOfWork.EmployeeExperienceRepository.Update(empExp);
                        _unitOfWork.Save();
                        scope.Complete();
                        result.IsSuccess = true;
                        result.Message = "Updated Employee Successfully";
                    }
                }

            }
            return result;
        }

        public bool DeleteEmployeeExperience(int ExperienceId)
        {
            var success = false;
            if (ExperienceId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var empExp = _unitOfWork.EmployeeExperienceRepository.GetByID(ExperienceId);
                    if (empExp != null)
                    {

                        _unitOfWork.EmployeeExperienceRepository.Delete(empExp);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }
    }
}
