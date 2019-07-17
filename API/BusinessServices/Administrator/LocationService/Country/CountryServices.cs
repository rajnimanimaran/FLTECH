using AutoMapper;
using BusinessEntities;
using BusinessServices;
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
    public class CountryServices : ICountryService
    {

        private readonly IUnitOfWork _unitOfWork;

        public CountryServices(IUnitOfWork unit)
        {
            _unitOfWork = unit;
        }

        //Select the All Company Details
        public IEnumerable<BusinessEntities.CountryEntity> GetAllCountry()
        {
            var Country = _unitOfWork.CountryRepository.GetAll().OrderByDescending(x => x.CountryId).ToList();
            if (Country.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Country, CountryEntity>();
                });

                IMapper mapper = config.CreateMapper();
                var CountryModel = mapper.Map<List<Country>, List<CountryEntity>>(Country);

                return CountryModel;
            }
            return null;
        }

        public IEnumerable<BusinessEntities.CountryEntity> GetActiveCountryById()
        {
            var countryList = _unitOfWork.CountryRepository.GetMany(a => a.IsActive == true).ToList();
            if (countryList.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {

                    cfg.CreateMap<Country, CountryEntity>();

                });

                IMapper mapper = config.CreateMapper();
                var CountryModel = mapper.Map<List<Country>, List<CountryEntity>>(countryList);
                return CountryModel;
            }
            return new List<CountryEntity>();
        }
        public CountryEntity GetCountryById(int countryId)
        {

            int countryid = countryId;
            var country = _unitOfWork.CountryRepository.GetByID(countryid);
            if (country != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Country, CountryEntity>();
                });

                IMapper mapper = config.CreateMapper();

                var Countryid = mapper.Map<Country, CountryEntity>(country);
                return Countryid;
            }
            return null;
        }

        public ResultDTO CreateCountry(BusinessEntities.CountryEntity Country)
        {
            var result = new ResultDTO { IsSuccess = false };

            var isExist = _unitOfWork.CountryRepository.GetManyQueryable(c => c.CountryName.ToLower() == Country.CountryName.ToLower() && c.CountryCode == Country.CountryCode).Count() > 0;
            if (!isExist)
            {
                using (var scope = new TransactionScope())
                {

                    var country = new DataModel.Country
                    {
                        CountryName = Country.CountryName,
                        CountryCode = Country.CountryCode,
                        CreatedBy = Country.CreatedBy,
                        CreatedOn = DateTime.Now,
                        ModifiedBy = Country.CreatedBy,
                        ModifiedOn = DateTime.Now,
                        IsActive = true,


                    };
                    _unitOfWork.CountryRepository.Insert(country);
                    _unitOfWork.Save();
                    scope.Complete();
                    result.IsSuccess = true;
                    result.Message = "Inserted Country Successfully";

                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "Country name already exist";
            }

            return result;
        }

        public ResultDTO UpdateCountry(int CountryId, BusinessEntities.CountryEntity CountryEntity)
        {
            var result = new ResultDTO { IsSuccess = false };
            if (CountryEntity != null)
            {
                var isExist = _unitOfWork.CountryRepository.GetManyQueryable(c => c.CountryName.ToLower() == CountryEntity.CountryName.ToLower() && c.CountryCode == CountryEntity.CountryCode).Count() > 0;
                if (!isExist)
                {
                    using (var scope = new TransactionScope())
                    {
                        var country = _unitOfWork.CountryRepository.GetByID(CountryId);
                        if (country != null)
                        {
                            country.CountryId = CountryEntity.CountryId;
                            country.CountryName = CountryEntity.CountryName;
                            country.CountryCode = CountryEntity.CountryCode;
                            country.IsActive = CountryEntity.IsActive;
                            country.ModifiedBy = CountryEntity.ModifiedBy;
                            country.ModifiedOn = DateTime.Now;


                            _unitOfWork.CountryRepository.Update(country);
                            _unitOfWork.Save();
                            scope.Complete();
                            result.IsSuccess = true;
                            result.Message = "Updated Country Successfully";
                        }
                    }
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Country name already exist";
                }
            }
            return result;
        }


        public bool DeleteCountry(int CountryId)
        {
            var success = false;
            if (CountryId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var country = _unitOfWork.CountryRepository.GetByID(CountryId);
                    if (country != null)
                    {

                        _unitOfWork.CountryRepository.Delete(country);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool ToggleActiveCountry(int CountryId)
        {
            var success = false;
            if (CountryId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var country = _unitOfWork.CountryRepository.GetByID(CountryId);
                    if (country != null)
                    {
                        country.IsActive = !country.IsActive;
                        country.ModifiedBy = 1;
                        country.ModifiedOn = DateTime.Now;

                        _unitOfWork.CountryRepository.Update(country);
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