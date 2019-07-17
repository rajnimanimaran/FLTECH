using AutoMapper;
using BusinessEntities;
using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessServices
{
    public class CompanyServices : ICompanyServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public CompanyEntity GetCompanyById(int companyId)
        {
            var company = _unitOfWork.CompanyRepository.GetByID(companyId);
            if (company != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Company, CompanyEntity>();
                    cfg.CreateMap<Country, CompanyCountryDTO>();
                    cfg.CreateMap<State, CompanyStateDTO>();
                    cfg.CreateMap<City, CompanyCityDTO>();
                    cfg.CreateMap<Area, CompanyAreaDTO>();
                    cfg.CreateMap<OrganizationLevel, OrganizationLevelEntity>();
                    cfg.CreateMap<OwnershipTypes, OwnerShipEntites>();
                    cfg.CreateMap<NatureOfBusiness, NatureOfBusinessEntities>();
                });

                IMapper mapper = config.CreateMapper();

                var companyModel = mapper.Map<Company, CompanyEntity>(company);
                return companyModel;
            }
            return null;
        }

        public IEnumerable<CompanyEntity> GetAllCompany()
        {
            var company = _unitOfWork.CompanyRepository.GetAll().OrderByDescending(x => x.CompanyId).ToList();
            if (company.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Company, CompanyEntity>();
                    cfg.CreateMap<Country, CompanyCountryDTO>();
                    cfg.CreateMap<State, CompanyStateDTO>();
                    cfg.CreateMap<City, CompanyCityDTO>();
                    cfg.CreateMap<Area, CompanyAreaDTO>();
                    cfg.CreateMap<OrganizationLevel, OrganizationLevelEntity>();
                    cfg.CreateMap<OwnershipTypes, OwnerShipEntites>();
                    cfg.CreateMap<NatureOfBusiness, NatureOfBusinessEntities>();
                });
                IMapper mapper = config.CreateMapper();

                var companylist = mapper.Map<List<Company>, List<CompanyEntity>>(company);
                foreach (var d in companylist)
                {
                    if (d.ParentCompany > 0)
                        d.ParentCompanyName = companylist.Where(com => com.CompanyId == d.ParentCompany).First().CompanyName;
                    else
                        d.ParentCompanyName = String.Empty;
                }

                return companylist;
            }

            return null;
        }

        public IEnumerable<BusinessEntities.CompanyEntity> GetCompaniesByUserId(long userId)
        {
            //var comIdLst = _unitOfWork.UserRepository.GetByID(userId).Companies.Split(',');
            var comList = new List<CompanyEntity>();

            //foreach (var c in comIdLst)
            //{
            //    comList.Add(GetCompanyById(Convert.ToInt32(c)));
            //}

            return comList;
        }

        public int CreateCompany(CompanyEntity companyEntity)
        {

            using (var scope = new TransactionScope())
            {
                var company = new Company
                {
                    CompanyName = companyEntity.CompanyName,
                    CompanyCode = companyEntity.CompanyCode,
                    CountryId = companyEntity.CountryId,
                    StateId = companyEntity.StateId,
                    CityId = companyEntity.CityId,
                    AreaId = companyEntity.AreaId,
                    Email = companyEntity.Email,
                    PhoneNo = companyEntity.PhoneNo,
                    Website = companyEntity.Website,
                    Pincode = companyEntity.Pincode,
                    AddressLine1 = companyEntity.AddressLine1,
                    AddressLine2 = companyEntity.AddressLine2,
                    OrganizationLevelId = companyEntity.OrganizationLevelId,
                    BusinessId = companyEntity.BusinessId,
                    TypeId = companyEntity.TypeId,
                    CompanyLogo = companyEntity.CompanyLogo,
                    FaxNo = companyEntity.FaxNo,
                    ParentCompany = companyEntity.ParentCompany,
                    IsActive = true,
                    CreatedBy = companyEntity.CreatedBy,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = companyEntity.CreatedBy,
                    ModifiedOn = DateTime.Now,

                };




                _unitOfWork.CompanyRepository.Insert(company);
                _unitOfWork.Save();
                scope.Complete();
                return company.CompanyId;

            }

        }

        public bool UpdateCompany(int CompanyId, CompanyEntity companyEntity)
        {
            var success = false;
            if (companyEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var company = _unitOfWork.CompanyRepository.GetByID(CompanyId);
                    if (companyEntity != null)
                    {

                        company.CompanyName = companyEntity.CompanyName;
                        company.CompanyCode = companyEntity.CompanyCode;
                        company.CountryId = companyEntity.CountryId;
                        company.StateId = companyEntity.StateId;
                        company.CityId = companyEntity.CityId;
                        company.AreaId = companyEntity.AreaId;
                        company.ParentCompany = companyEntity.ParentCompany;
                        company.AddressLine1 = companyEntity.AddressLine1;
                        company.AddressLine2 = companyEntity.AddressLine2;
                        company.OrganizationLevelId = companyEntity.OrganizationLevelId;
                        company.Pincode = companyEntity.Pincode;
                        company.BusinessId = companyEntity.BusinessId;
                        company.TypeId = companyEntity.TypeId;
                        company.CompanyLogo = companyEntity.CompanyLogo;
                        company.FaxNo = companyEntity.FaxNo;
                        company.Email = companyEntity.Email;
                        company.PhoneNo = companyEntity.PhoneNo;
                        company.Website = companyEntity.Website;
                        company.IsActive = companyEntity.IsActive;
                        company.ModifiedBy = companyEntity.ModifiedBy;
                        company.ModifiedOn = DateTime.Now;

                        _unitOfWork.CompanyRepository.Update(company);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool DeleteCompany(int CompanyId)
        {
            var success = false;
            if (CompanyId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var companys = _unitOfWork.CompanyRepository.GetMany(d => d.ParentCompany == CompanyId);
                    if (!companys.Any())
                    {
                        var company = _unitOfWork.CompanyRepository.GetByID(CompanyId);
                        if (CompanyId != 0)
                        {
                            _unitOfWork.CompanyRepository.Delete(company);
                            _unitOfWork.Save();
                            scope.Complete();
                            success = true;
                        }
                    }

                }
            }
            return success;
        }

        public bool ToggleActiveCompany(int CompanyId)
        {
            var success = false;
            if (CompanyId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var Cmp = _unitOfWork.CompanyRepository.GetByID(CompanyId);
                    if (Cmp != null)
                    {
                        Cmp.IsActive = !Cmp.IsActive;
                        Cmp.ModifiedBy = 1;
                        Cmp.ModifiedOn = DateTime.Now;
                        _unitOfWork.CompanyRepository.Update(Cmp);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public IEnumerable<CompanyEntity> GetActiveCompanyById()
        {
            var ObjCmp = _unitOfWork.CompanyRepository.GetMany(a => a.IsActive == true).ToList();
            if (ObjCmp.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Company, CompanyEntity>();
                    cfg.CreateMap<Country, CompanyCountryDTO>();
                    cfg.CreateMap<State, CompanyStateDTO>();
                    cfg.CreateMap<City, CompanyCityDTO>();
                    cfg.CreateMap<Area, CompanyAreaDTO>();
                    cfg.CreateMap<OrganizationLevel, OrganizationLevelEntity>();
                    cfg.CreateMap<OwnershipTypes, OwnerShipEntites>();
                    cfg.CreateMap<NatureOfBusiness, NatureOfBusinessEntities>();
                });
                IMapper mapper = config.CreateMapper();
                var CompanyModel = mapper.Map<List<Company>, List<CompanyEntity>>(ObjCmp);
                return CompanyModel;
            }
            return new List<CompanyEntity>();
        }

        public IEnumerable<NatureOfBusinessEntities> GetAllNatureOfBusiness()
        {
            var business = _unitOfWork.NatureOfBusinessRepository.GetAll().OrderByDescending(x => x.BusinessId).ToList();
            if (business.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {

                    cfg.CreateMap<NatureOfBusiness, NatureOfBusinessEntities>();
                });
                IMapper mapper = config.CreateMapper();

                var businesslist = mapper.Map<List<NatureOfBusiness>, List<NatureOfBusinessEntities>>(business);


                return businesslist;
            }

            return null;
        }

        public IEnumerable<OwnerShipEntites> GetAllOwnerShip()
        {
            var ownership = _unitOfWork.OwnershipTypesRepository.GetAll().OrderByDescending(x => x.TypeId).ToList();
            if (ownership.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {

                    cfg.CreateMap<OwnershipTypes, OwnerShipEntites>();
                });
                IMapper mapper = config.CreateMapper();

                var ownershiplist = mapper.Map<List<OwnershipTypes>, List<OwnerShipEntites>>(ownership);


                return ownershiplist;
            }

            return null;
        }

        public IEnumerable<UserCompanyDTO> GetSubCompByCompId(int companyId)
        {
            var allCompanies = _unitOfWork.CompanyRepository.GetAll().Where(c => c.IsActive == true);

            var userCompanies = new List<Company>();
            userCompanies.Add(allCompanies.First(c => c.CompanyId == companyId));

            userCompanies.AddRange(CommonService.getSubCompanyList(allCompanies, userCompanies.First().CompanyId));

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Company, UserCompanyDTO>();
            });
            IMapper mapper = config.CreateMapper();

            var result = mapper.Map<List<Company>, List<UserCompanyDTO>>(userCompanies);

            return result;
        }

        public IEnumerable<UserCompanyDTO> GetParentCompByCompId(GetParentListDTO obj)
        {
            var allCompanies = _unitOfWork.CompanyRepository.GetAll().Where(c => c.IsActive == true);

            var resCompanies = new List<Company>();
            resCompanies.Add(allCompanies.First(c => c.CompanyId == obj.CompanyId));
            resCompanies.AddRange(CommonService.getParentsList(allCompanies, resCompanies.First().ParentCompany, obj.UserCompanyId));
            resCompanies.Reverse();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Company, UserCompanyDTO>();
            });
            IMapper mapper = config.CreateMapper();

            var result = mapper.Map<List<Company>, List<UserCompanyDTO>>(resCompanies);

            return result;
        }

        public bool CompanyLogoUpload(AddLogo logo)
        {
            var result = false;
            var CmpToUpdate = _unitOfWork.CompanyRepository.GetByID(logo.CompanyId);
            CmpToUpdate.CompanyLogo = logo.CompanyLogo;
            using (var scope = new TransactionScope())
            {
                _unitOfWork.CompanyRepository.Update(CmpToUpdate);
                _unitOfWork.Save();
                result = true;
                scope.Complete();
            }
            return result;
        }

    }
}
