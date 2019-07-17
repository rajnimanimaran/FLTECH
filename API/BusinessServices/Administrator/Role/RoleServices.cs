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
    public class RoleServices : IRoleServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public BusinessEntities.RoleEntity GetRoleById(int roleId)
        {
            var role = _unitOfWork.RoleRepository.GetByID(roleId);
            if (role != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<OrganizationLevel, OrganizationLevelEntity>();
                    cfg.CreateMap<Role, RoleEntity>();
                });

                IMapper mapper = config.CreateMapper();

                var roleModel = mapper.Map<Role, RoleEntity>(role);
                return roleModel;
            }
            return null;
        }

        public IEnumerable<BusinessEntities.RoleEntity> GetAllRoles()
        {
            var roles = _unitOfWork.RoleRepository.GetAll().OrderByDescending(x => x.RoleId).ToList();
            if (roles.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<OrganizationLevel, OrganizationLevelEntity>();
                    cfg.CreateMap<Role, RoleEntity>();
                });
                IMapper mapper = config.CreateMapper();

                var roleModel = mapper.Map<List<Role>, List<RoleEntity>>(roles);
                return roleModel;
            }

            return null;
        }

        public ResultDTO CreateRole(BusinessEntities.RoleEntity roleEntity)
        {
            var result = new ResultDTO { IsSuccess = false };

            var isExist = _unitOfWork.RoleRepository.GetManyQueryable(c => c.RoleName.ToLower() == roleEntity.RoleName.ToLower() && c.OrganizationLevelId == roleEntity.OrganizationLevelId).Count() > 0;
            if (!isExist)
            {
                using (var scope = new TransactionScope())
                {
                    var role = new Role
                    {
                        RoleName = roleEntity.RoleName,
                        OrganizationLevelId = roleEntity.OrganizationLevelId,
                        IsActive = true,
                        CreatedBy = roleEntity.CreatedBy,
                        CreatedOn = DateTime.Now,
                        ModifiedBy = roleEntity.CreatedBy,
                        ModifiedOn = DateTime.Now,

                    };
                    _unitOfWork.RoleRepository.Insert(role);
                    _unitOfWork.Save();
                    scope.Complete();

                    result.IsSuccess = true;
                    result.Message = "Added Successfully";
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "Role already exist";
            }
            return result;
        }

        public ResultDTO UpdateRole(int RoleId, BusinessEntities.RoleEntity roleEntity)
        {
            var result = new ResultDTO { IsSuccess = false };

            if (roleEntity != null)
            {
                var isExist = _unitOfWork.RoleRepository.GetManyQueryable(c => c.RoleName.ToLower() == roleEntity.RoleName.ToLower() && c.OrganizationLevelId == roleEntity.OrganizationLevelId).Count() > 0;
                if (!isExist)
                {
                    using (var scope = new TransactionScope())
                    {
                        var role = _unitOfWork.RoleRepository.GetByID(RoleId);
                        if (roleEntity != null)
                        {
                            role.RoleId = roleEntity.RoleId;
                            role.RoleName = roleEntity.RoleName;
                            role.OrganizationLevelId = roleEntity.OrganizationLevelId;
                            role.IsActive = roleEntity.IsActive;
                            role.ModifiedBy = roleEntity.ModifiedBy;
                            role.ModifiedOn = DateTime.Now;

                            _unitOfWork.RoleRepository.Update(role);
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
                    result.Message = "Role already exist";
                }
            }
            return result;
        }

        public bool DeleteRole(int RoleId)
        {
            var success = false;
            if (RoleId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var role = _unitOfWork.RoleRepository.GetByID(RoleId);
                    if (RoleId != 0)
                    {
                        _unitOfWork.RoleRepository.Delete(role);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }

            }
            return success;
        }

        public bool ToggleActiveRole(int RoleId)
        {
            var success = false;
            if (RoleId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var ObjRole = _unitOfWork.RoleRepository.GetByID(RoleId);
                    if (ObjRole != null)
                    {
                        ObjRole.IsActive = !ObjRole.IsActive;
                        ObjRole.ModifiedBy = 1;
                        ObjRole.ModifiedOn = DateTime.Now;
                        _unitOfWork.RoleRepository.Update(ObjRole);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public IEnumerable<RoleEntity> GetActiveRolesById()
        {
            var ObjRole = _unitOfWork.RoleRepository.GetMany(a => a.IsActive == true).ToList();
            if (ObjRole.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<OrganizationLevel, OrganizationLevelEntity>();
                    cfg.CreateMap<Role, RoleEntity>();
                });
                IMapper mapper = config.CreateMapper();
                var RoleModel = mapper.Map<List<Role>, List<RoleEntity>>(ObjRole);
                return RoleModel;
            }
            return new List<RoleEntity>();
        }
    }
}
