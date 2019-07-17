using AutoMapper;
using BusinessEntities;
using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessServices
{
    public class AreaServices : IAreaServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public AreaServices(IUnitOfWork unit)
        {
            this._unitOfWork = unit;
        }

        public BusinessEntities.AreaEntity GetAreaById(int AreaId)
        {
            var Area = _unitOfWork.AreaRepository.GetByID(AreaId);
            if (Area != null)
            {
                var config = new MapperConfiguration(cfg =>
                {

                    cfg.CreateMap<Country, CountryEntity>();
                    cfg.CreateMap<State, StateEntity>();
                    cfg.CreateMap<City, CityEntity>();
                    cfg.CreateMap<Area, AreaEntity>();
                });

                IMapper mapper = config.CreateMapper();

                var AreaModel = mapper.Map<Area, AreaEntity>(Area);
                return AreaModel;
            }
            return null;
        }

        public IEnumerable<BusinessEntities.AreaEntity> GetAllAreas()
        {
            var Area = _unitOfWork.AreaRepository.GetAll().OrderByDescending(x => x.AreaId).ToList();
            if (Area.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Country, CountryEntity>();
                    cfg.CreateMap<State, StateEntity>();
                    cfg.CreateMap<City, CityEntity>();
                    cfg.CreateMap<Area, AreaEntity>();
                });

                IMapper mapper = config.CreateMapper();
                var AreaList = mapper.Map<List<Area>, List<AreaEntity>>(Area);

                return AreaList;
            }
            return null;
        }
        public IEnumerable<BusinessEntities.AreaEntity> GetActiveAreaById()
        {
            var areaList = _unitOfWork.AreaRepository.GetMany(a => a.IsActive == true).ToList();
            if (areaList.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Country, CountryEntity>();
                    cfg.CreateMap<State, StateEntity>();
                    cfg.CreateMap<City, CityEntity>();
                    cfg.CreateMap<Area, AreaEntity>();

                });

                IMapper mapper = config.CreateMapper();
                var AreaModel = mapper.Map<List<Area>, List<AreaEntity>>(areaList);
                return AreaModel;
            }
            return new List<AreaEntity>();
        }



        public ResultDTO CreateArea(BusinessEntities.AreaEntity AreaEntity)
        {
            var result = new ResultDTO { IsSuccess = false };

            var isExist = _unitOfWork.AreaRepository.GetManyQueryable(c => c.AreaName.ToLower() == AreaEntity.AreaName.ToLower() && c.CountryId == AreaEntity.CountryId && c.StateId == AreaEntity.StateId && c.CityId == AreaEntity.CityId).Count() > 0;
            if (!isExist)
            {
                using (var scope = new TransactionScope())
                {
                    var Area = new Area
                    {

                        AreaName = AreaEntity.AreaName,
                        CityId = AreaEntity.CityId,
                        StateId = AreaEntity.StateId,
                        CountryId = AreaEntity.CountryId,
                        Pincode = AreaEntity.Pincode,
                        IsActive = true,
                        CreatedBy = AreaEntity.CreatedBy,
                        CreatedOn = DateTime.Now,
                        ModifiedBy = AreaEntity.CreatedBy,
                        ModifiedOn = DateTime.Now,
                    };
                    _unitOfWork.AreaRepository.Insert(Area);
                    _unitOfWork.Save();
                    scope.Complete();

                    result.IsSuccess = true;
                    result.Message = "Inserted Area Successfully";
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "Area name already exist";
            }
            return result;

        }

        public ResultDTO UpdateArea(int AreaId, BusinessEntities.AreaEntity AreaEntity)
        {
            var result = new ResultDTO { IsSuccess = false };

            if (AreaEntity != null)
            {
                var isExist = _unitOfWork.AreaRepository.GetManyQueryable(c => c.AreaName.ToLower() == AreaEntity.AreaName.ToLower() && c.CountryId == AreaEntity.CountryId && c.StateId == AreaEntity.StateId && c.CityId == AreaEntity.CityId).Count() > 0;
                if (!isExist)
                {
                    using (var scope = new TransactionScope())
                    {
                        var Areaent = _unitOfWork.AreaRepository.GetByID(AreaId);
                        if (Areaent != null)
                        {
                            Areaent.AreaId = AreaEntity.AreaId;
                            Areaent.AreaName = AreaEntity.AreaName;
                            Areaent.StateId = AreaEntity.StateId;
                            Areaent.Pincode = AreaEntity.Pincode;
                            Areaent.CountryId = AreaEntity.CountryId;
                            Areaent.CityId = AreaEntity.CityId;
                            Areaent.IsActive = AreaEntity.IsActive;
                            Areaent.ModifiedBy = AreaEntity.ModifiedBy;
                            Areaent.ModifiedOn = DateTime.Now;


                            _unitOfWork.AreaRepository.Update(Areaent);
                            _unitOfWork.Save();
                            scope.Complete();

                            result.IsSuccess = true;
                            result.Message = "Updated Area Successfully";
                        }
                    }
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Area name already exist";
                }
            }
            return result;
        }

        public bool DeleteArea(int AreaId)
        {
            var success = false;
            if (AreaId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var Employee = _unitOfWork.AreaRepository.GetByID(AreaId);
                    if (Employee != null)
                    {

                        _unitOfWork.AreaRepository.Delete(Employee);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool ToggleActiveAreas(int AreaId)
        {
            var success = false;
            if (AreaId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var ObjArea = _unitOfWork.AreaRepository.GetByID(AreaId);
                    if (ObjArea != null)
                    {
                        ObjArea.IsActive = !ObjArea.IsActive;
                        ObjArea.ModifiedBy = 1;
                        ObjArea.ModifiedOn = DateTime.Now;
                        _unitOfWork.AreaRepository.Update(ObjArea);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public IEnumerable<AreaEntity> GetAreaByCityId(int CityId)
        {
            var areaList = _unitOfWork.AreaRepository.GetMany(a => a.CityId == CityId && a.IsActive == true).ToList();
            if (areaList.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Country, CountryEntity>();
                    cfg.CreateMap<State, StateEntity>();
                    cfg.CreateMap<City, CityEntity>();
                    cfg.CreateMap<Area, AreaEntity>();

                });

                IMapper mapper = config.CreateMapper();
                var AreaModel = mapper.Map<List<Area>, List<AreaEntity>>(areaList);
                return AreaModel;
            }
            return new List<AreaEntity>();

        }

       
    }
}