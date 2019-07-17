using AutoMapper;
using BusinessEntities;
using BusinessServices;
using DataModel.UnitOfWork;
using DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessServices
{
    public class OrganizationLevelServices : IOrganizationLevelServices
    {

        private readonly IUnitOfWork _unitOfWork;

        public OrganizationLevelServices(IUnitOfWork unit)
        {
            _unitOfWork = unit;
        }

        //Select the All OrganizationLevel Details
        public IEnumerable<BusinessEntities.OrganizationLevelEntity> GetAllOrganizationLevel()
        {
            var OrganizationLevel = _unitOfWork.OrganizationLevelRepository.GetAll().OrderByDescending(x => x.OrganizationLevelId).ToList();
            if (OrganizationLevel.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<OrganizationLevel, OrganizationLevelEntity>();
                });

                IMapper mapper = config.CreateMapper();
                var OrganizationLevelModel = mapper.Map<List<OrganizationLevel>, List<OrganizationLevelEntity>>(OrganizationLevel);
                foreach (var d in OrganizationLevelModel)
                {
                    if (d.Parent > 0)
                        d.ParentName = OrganizationLevelModel.Where(dep => dep.OrganizationLevelId == d.Parent).First().LevelName;
                    else
                        d.ParentName = String.Empty;
                }
                return OrganizationLevelModel;
            }
            return null;
        }

        // GetorganizationlevelById
        public OrganizationLevelEntity GetOrganizationLevelById(int OrganizationLevelId)
        {
            var OrganizationLevel = _unitOfWork.OrganizationLevelRepository.GetByID(OrganizationLevelId);
            if (OrganizationLevel != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<OrganizationLevel, OrganizationLevelEntity>();
                });

                IMapper mapper = config.CreateMapper();

                var Orglevelid = mapper.Map<OrganizationLevel, OrganizationLevelEntity>(OrganizationLevel);
                return Orglevelid;
            }
            return null;
        }

        // Create OrganizationLevel
        public ResultDTO CreateOrganizationLevel(BusinessEntities.OrganizationLevelEntity OrganizationLevelEntity)
        {
            var result = new ResultDTO { IsSuccess = false };
            var isExist = _unitOfWork.OrganizationLevelRepository.GetManyQueryable(c => c.LevelName.ToLower() == OrganizationLevelEntity.LevelName.ToLower()).Count() > 0;
            if (!isExist)
            {

                using (var scope = new TransactionScope())
                {
                    var Orglevel = new OrganizationLevel
                    {


                        LevelName = OrganizationLevelEntity.LevelName,
                        Parent = OrganizationLevelEntity.Parent,
                        CreatedBy = OrganizationLevelEntity.CreatedBy,
                        CreatedOn = DateTime.Now,
                        ModifiedBy = OrganizationLevelEntity.CreatedBy,
                        ModifiedOn = DateTime.Now,
                        IsActive = true,
                        Code=OrganizationLevelEntity.Code
                    };
                    _unitOfWork.OrganizationLevelRepository.Insert(Orglevel);
                    _unitOfWork.Save();
                    scope.Complete();

                    result.IsSuccess = true;
                    result.Message = "Insert Successfully";

                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "Level Name already exist";
            }
            return result;
        }


        public ResultDTO UpdateOrganizationLevel(int OrganizationLevelId, OrganizationLevelEntity OrganizationLevelEntity)
        {
            var result = new ResultDTO { IsSuccess = false };

            if (OrganizationLevelEntity != null)
            {
                var isExist = _unitOfWork.OrganizationLevelRepository.GetManyQueryable(c => c.LevelName.ToLower() == OrganizationLevelEntity.LevelName.ToLower() && c.Parent == OrganizationLevelEntity.Parent).Count() > 0;
                if (!isExist)
                {

                    using (var scope = new TransactionScope())
                    {
                        var Orglevel = _unitOfWork.OrganizationLevelRepository.GetByID(OrganizationLevelId);
                        if (Orglevel != null)
                        {

                            Orglevel.LevelName = OrganizationLevelEntity.LevelName;
                            Orglevel.Parent = OrganizationLevelEntity.Parent;
                            Orglevel.CreatedBy = OrganizationLevelEntity.CreatedBy;
                            Orglevel.ModifiedBy = OrganizationLevelEntity.ModifiedBy;
                            Orglevel.ModifiedOn = DateTime.Now;
                            Orglevel.IsActive = OrganizationLevelEntity.IsActive;
                            Orglevel.Code = OrganizationLevelEntity.Code;

                            _unitOfWork.OrganizationLevelRepository.Update(Orglevel);
                            _unitOfWork.Save();
                            scope.Complete();

                            result.IsSuccess = true;
                            result.Message = "Updated Successfully";
                        }
                    }
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Level Name already exist";
                }
            }
            return result;
        }

        public bool DeleteOrganizationLevel(int OrganizationLevelId)
        {
            var success = false;
            if (OrganizationLevelId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var organization = _unitOfWork.OrganizationLevelRepository.GetMany(d => d.Parent == OrganizationLevelId);
                    if (!organization.Any())
                    {


                        var orglevel = _unitOfWork.OrganizationLevelRepository.GetByID(OrganizationLevelId);
                        if (orglevel != null)
                        {

                            _unitOfWork.OrganizationLevelRepository.Delete(orglevel);
                            _unitOfWork.Save();
                            scope.Complete();
                            success = true;
                        }
                    }
                }
            }
            return success;
        }

        public bool ToggleActiveOrganizationLevel(int OrganizationLevelId)
        {
            var success = false;
            if (OrganizationLevelId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var objOrg = _unitOfWork.OrganizationLevelRepository.GetByID(OrganizationLevelId);
                    if (objOrg != null)
                    {
                        objOrg.IsActive = !objOrg.IsActive;
                        objOrg.ModifiedBy = 1;
                        objOrg.ModifiedOn = DateTime.Now;
                        _unitOfWork.OrganizationLevelRepository.Update(objOrg);
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