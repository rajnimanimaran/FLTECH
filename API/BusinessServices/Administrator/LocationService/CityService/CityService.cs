using BusinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BusinessEntities;

using DataModel;
using AutoMapper;
using DataModel.UnitOfWork;

namespace BusinessServices
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CityService(IUnitOfWork unit)
        {
            _unitOfWork = unit;
        }
        public BusinessEntities.CityEntity GetCityById(int CityId)
        {
            var city = _unitOfWork.CityRepository.GetByID(CityId);
            if (city != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Country, CountryEntity>();
                    cfg.CreateMap<State, StateEntity>();
                    cfg.CreateMap<City, CityEntity>();
                });

                IMapper mapper = config.CreateMapper();

                var cityModel = mapper.Map<City, CityEntity>(city);
                return cityModel;
            }
            return null;
        }
        //Get Active City by stateId
        public IEnumerable<BusinessEntities.CityEntity> GetCityByStateId(int StateId)
        {
            var cityList = _unitOfWork.CityRepository.GetMany(a => a.StateId == StateId && a.IsActive == true).ToList();

            if (cityList.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Country, CountryEntity>();
                    cfg.CreateMap<State, StateEntity>();
                    cfg.CreateMap<City, CityEntity>();
                });

                IMapper mapper = config.CreateMapper();
                var cityModel = mapper.Map<List<City>, List<CityEntity>>(cityList);
                return cityModel;
            }
            return new List<CityEntity>();
        }

        public IEnumerable<CityEntity> GetAllCity()
        {

            var CityList = _unitOfWork.CityRepository.GetAll().OrderByDescending(x => x.CityId).ToList();
            if (CityList.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Country, CountryEntity>();
                    cfg.CreateMap<State, StateEntity>();
                    cfg.CreateMap<City, CityEntity>();
                });

                IMapper mapper = config.CreateMapper();
                var CityModel = mapper.Map<List<City>, List<CityEntity>>(CityList);
                return CityModel;
            }
            return null;
        }

        public IEnumerable<BusinessEntities.CityEntity> GetActiveCityById()
        {
            var ObjCity = _unitOfWork.CityRepository.GetMany(a => a.IsActive == true).ToList();
            if (ObjCity.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Country, CountryEntity>();
                    cfg.CreateMap<State, StateEntity>();
                    cfg.CreateMap<City, CityEntity>();
                });
                IMapper mapper = config.CreateMapper();
                var CityModel = mapper.Map<List<City>, List<CityEntity>>(ObjCity);
                return CityModel;
            }
            return new List<CityEntity>();
        }

        public ResultDTO CreateCity(CityEntity cityEntity)
        {
            var result = new ResultDTO { IsSuccess = false };

            var isExist = _unitOfWork.CityRepository.GetManyQueryable(c => c.CityName.ToLower() == cityEntity.CityName.ToLower() && c.CountryId == cityEntity.CountryId && c.StateId == cityEntity.StateId).Count() > 0;
            if (!isExist)
            {

                using (var scope = new TransactionScope())
                {
                    var city = new City
                    {
                        CityName = cityEntity.CityName,
                        StateId = cityEntity.StateId,
                        CountryId = cityEntity.CountryId,
                        IsActive = true,
                        CreatedBy = cityEntity.CreatedBy,
                        CreatedOn = DateTime.Now,
                        ModifiedBy = cityEntity.CreatedBy,
                        ModifiedOn = DateTime.Now,


                    };
                    _unitOfWork.CityRepository.Insert(city);
                    _unitOfWork.Save();
                    scope.Complete();

                    result.IsSuccess = true;
                    result.Message = "Inserted City Successfully";

                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "City name already exist";
            }
            return result;
        }


        public ResultDTO UpdateCity(int CityId, BusinessEntities.CityEntity CityEntity)
        {
            var result = new ResultDTO { IsSuccess = false };

            if (CityEntity != null)
            {
                var isExist = _unitOfWork.CityRepository.GetManyQueryable(c => c.CityName.ToLower() == CityEntity.CityName.ToLower() && c.CountryId == CityEntity.CountryId && c.StateId == CityEntity.StateId).Count() > 0;
                if (!isExist)
                {
                    using (var scope = new TransactionScope())
                    {
                        var City = _unitOfWork.CityRepository.GetByID(CityId);
                        if (City != null)
                        {
                            City.CityId = CityEntity.CityId;
                            City.CityName = CityEntity.CityName;
                            City.StateId = CityEntity.StateId;
                            City.CountryId = CityEntity.CountryId;
                            City.IsActive = CityEntity.IsActive;
                            City.ModifiedBy = CityEntity.ModifiedBy;
                            City.ModifiedOn = DateTime.Now;


                            _unitOfWork.CityRepository.Update(City);
                            _unitOfWork.Save();
                            scope.Complete();

                            result.IsSuccess = true;
                            result.Message = "Updated City Successfully";
                        }
                    }
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "City name already exist";
                }
            }
            return result;
        }

        public bool DeleteCity(int CityId)
        {
            var success = false;
            if (CityId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var city = _unitOfWork.CityRepository.GetByID(CityId);
                    if (city != null)
                    {

                        _unitOfWork.CityRepository.Delete(city);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool ToggleActiveCity(int CityId)
        {
            var success = false;
            if (CityId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var ObjCity = _unitOfWork.CityRepository.GetByID(CityId);
                    if (ObjCity != null)
                    {
                        ObjCity.IsActive = !ObjCity.IsActive;
                        ObjCity.ModifiedBy = 1;
                        ObjCity.ModifiedOn = DateTime.Now;
                        _unitOfWork.CityRepository.Update(ObjCity);
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