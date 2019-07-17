using BusinessServices;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BusinessEntities;
using DataModel;
using AutoMapper;

namespace BusinessServices
{
    public class DesignationService : IDesignationService
    {
        private readonly UnitOfWork _unitOfWork;

        public DesignationService(UnitOfWork unit)
        {
            _unitOfWork = unit;
        }
        public IEnumerable<DesignationEntity> GetDesignationById(int departmentid)
        {
            var designation = _unitOfWork.DesignationRepository.GetMany(x => x.DepartmentId == departmentid).ToList();

            if (designation != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Company, DesignationCompanyDTO>();
                    cfg.CreateMap<Department, DesignationDepartmentDTO>();
                    cfg.CreateMap<Designation, DesignationEntity>();
                });

                IMapper mapper = config.CreateMapper();


                var designationModel = mapper.Map<List<Designation>, List<DesignationEntity>>(designation);

                return designationModel;
            }
            return new List<DesignationEntity>();
        }

        public IEnumerable<DesignationEntity> GetAllDesignation(int companyId)
        {
            var companyList = _unitOfWork.CompanyRepository.GetAll();

            var userCompanies = new List<Company>();
            userCompanies.Add(companyList.First(c => c.CompanyId == companyId));
            userCompanies.AddRange(CommonService.getSubCompanyList(companyList, companyId));

            var compIdLsit = from company in userCompanies
                             select (company.CompanyId);

            var allDesignation = _unitOfWork.DesignationRepository.GetAll().OrderByDescending(x => x.DesignationId).ToList();

            var designations = allDesignation.Where(d => compIdLsit.Contains(d.CompanyId)).ToList();

            if (designations.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {

                    cfg.CreateMap<Company, DesignationCompanyDTO>();
                    cfg.CreateMap<Department, DesignationDepartmentDTO>();
                    cfg.CreateMap<Designation, DesignationEntity>();
                });

                IMapper mapper = config.CreateMapper();
                var designationList = mapper.Map<List<Designation>, List<DesignationEntity>>(designations);
                foreach (var d in designationList)
                {
                    if (d.Superior > 0)
                        d.SuperiorName = allDesignation.Where(des => des.DesignationId == d.Superior).First().DesignationName;
                    else
                        d.SuperiorName = String.Empty;
                }
                return designationList;
            }
            return null;
        }

        public ResultDTO CreateDesignation(DesignationEntity DesignationEntity)
        {
            var result = new ResultDTO { IsSuccess = false };

            var isExist = _unitOfWork.DesignationRepository.GetManyQueryable(c => c.DesignationName.ToLower() == DesignationEntity.DesignationName.ToLower() && c.CompanyId == DesignationEntity.CompanyId && c.DepartmentId == DesignationEntity.DepartmentId).Count() > 0;
            if (!isExist)
            {

                using (var scope = new TransactionScope())
                {
                    var designation = new Designation
                    {
                        DesignationName = DesignationEntity.DesignationName,
                        CompanyId = DesignationEntity.CompanyId,
                        DepartmentId = DesignationEntity.DepartmentId,
                        IsActive = true,
                        Superior = DesignationEntity.Superior,
                        DesignationCode = DesignationEntity.DesignationCode,
                        CreatedBy = DesignationEntity.CreatedBy,
                        CreatedOn = DateTime.Now,
                        ModifiedBy = DesignationEntity.CreatedBy,
                        ModifiedOn = DateTime.Now,

                    };
                    _unitOfWork.DesignationRepository.Insert(designation);
                    _unitOfWork.Save();
                    scope.Complete();

                    result.IsSuccess = true;
                    result.Message = "Inserted Designation Successfully";

                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "Designation name already exist";
            }
            return result;
        }


        public ResultDTO UpdateDesignation(int DesignationId, BusinessEntities.DesignationEntity DesignationEntity)
        {
            var result = new ResultDTO { IsSuccess = false };

            if (DesignationEntity != null)
            {
                var isExist = _unitOfWork.DesignationRepository.GetManyQueryable(c => c.DesignationName.ToLower() == DesignationEntity.DesignationName.ToLower()&& c.DesignationCode == DesignationEntity.DesignationCode && c.CompanyId == DesignationEntity.CompanyId && c.DepartmentId == DesignationEntity.DepartmentId).Count() > 0;
                if (!isExist)
                {
                    using (var scope = new TransactionScope())
                    {
                        var Designation = _unitOfWork.DesignationRepository.GetByID(DesignationId);
                        if (Designation != null)
                        {
                            Designation.DesignationId = DesignationEntity.DesignationId;
                            Designation.DesignationName = DesignationEntity.DesignationName;
                            Designation.Superior = DesignationEntity.Superior;
                            Designation.CompanyId = DesignationEntity.CompanyId;
                            Designation.DepartmentId = DesignationEntity.DepartmentId;
                            Designation.IsActive = DesignationEntity.IsActive;
                            Designation.ModifiedBy = DesignationEntity.ModifiedBy;
                            Designation.ModifiedOn = DateTime.Now;



                            _unitOfWork.DesignationRepository.Update(Designation);
                            _unitOfWork.Save();
                            scope.Complete();

                            result.IsSuccess = true;
                            result.Message = "Updated Designation Successfully";
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

        public bool DeleteDesignation(int DesignationId)
        {
            var success = false;
            if (DesignationId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var des = _unitOfWork.DesignationRepository.GetMany(d => d.Superior == DesignationId);
                    if (!des.Any())
                    {
                        var designation = _unitOfWork.DesignationRepository.GetByID(DesignationId);
                        if (designation != null)
                        {

                            _unitOfWork.DesignationRepository.Delete(designation);
                            _unitOfWork.Save();
                            scope.Complete();
                            success = true;
                        }
                    }
                }
            }
            return success;
        }

        public bool ToggleActiveDesignation(int DesignationId)
        {
            var success = false;
            if (DesignationId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var Design = _unitOfWork.DesignationRepository.GetByID(DesignationId);
                    if (Design != null)
                    {
                        Design.IsActive = !Design.IsActive;
                        Design.ModifiedBy = 1;
                        Design.ModifiedOn = DateTime.Now;
                        _unitOfWork.DesignationRepository.Update(Design);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public IEnumerable<DesignationEntity> GetActiveDesignationById()
        {
            var DesignationList = _unitOfWork.DesignationRepository.GetMany(a => a.IsActive == true).ToList();
            if (DesignationList.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Company, DesignationCompanyDTO>();
                    cfg.CreateMap<Department, DesignationDepartmentDTO>();
                    cfg.CreateMap<Designation, DesignationEntity>();

                });

                IMapper mapper = config.CreateMapper();
                var DesignationModel = mapper.Map<List<Designation>, List<DesignationEntity>>(DesignationList);
                return DesignationModel;
            }
            return new List<DesignationEntity>();

            //return GetActiveDesignationById().Where(d => d.IsActive == true).ToList();
        }

        public IEnumerable<DesignationEntity> GetDesigByDepId(int depId)
        {
            var DesignationList = _unitOfWork.DesignationRepository.GetMany(d => d.IsActive == true && d.DepartmentId == depId).ToList();
            if (DesignationList.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Company, DesignationCompanyDTO>();
                    cfg.CreateMap<Department, DesignationDepartmentDTO>();
                    cfg.CreateMap<Designation, DesignationEntity>();

                });

                IMapper mapper = config.CreateMapper();
                var DesignationModel = mapper.Map<List<Designation>, List<DesignationEntity>>(DesignationList);
                return DesignationModel;
            }
            return new List<DesignationEntity>();
        }
    }
}