using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class CompanyEntity
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int ParentCompany { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public int AreaId { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Website { get; set; }
        public string CompanyLogo { get; set; }
        public string FaxNo { get; set; }
        public string Pincode { get; set; }
        public int OrganizationLevelId { get; set; }
        public int BusinessId { get; set; }
        public int TypeId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public string ParentCompanyName { get; set; }


        public CompanyCountryDTO Country { get; set; }
        public CompanyStateDTO State { get; set; }
        public CompanyCityDTO City { get; set; }
        public CompanyAreaDTO Area { get; set; }
        public OrganizationLevelEntity OrganizationLevel { get; set; }

    }

    public class CompanyCountryDTO
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }
    public class CompanyStateDTO
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
    }

    public class CompanyCityDTO
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
    }
    public class CompanyAreaDTO
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; }
    }

    public class UpdateSelectCompanyDTO
    {
        public long UserId { get; set; }
        public int CompanyId { get; set; }
    }

    public class GetParentListDTO
    {
        public int CompanyId { get; set; }
        public int UserCompanyId { get; set; }
    }

    public class AddLogo
    {
        public string CompanyLogo { get; set; }
        public int CompanyId { get; set; }
    }
}
