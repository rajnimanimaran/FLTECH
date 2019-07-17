using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface ICompanyServices
    {
        CompanyEntity GetCompanyById(int CompanyId);
        IEnumerable<CompanyEntity> GetAllCompany();
        IEnumerable<CompanyEntity> GetActiveCompanyById();
        IEnumerable<UserCompanyDTO> GetSubCompByCompId(int companyId);
        IEnumerable<UserCompanyDTO> GetParentCompByCompId(GetParentListDTO obj);
        IEnumerable<BusinessEntities.CompanyEntity> GetCompaniesByUserId(long userId);
        int CreateCompany(CompanyEntity CompanyEntity);
        bool UpdateCompany(int CompanyId, CompanyEntity CompanyEntity);
        bool DeleteCompany(int CompanyId);
        bool ToggleActiveCompany(int CompanyId);
        IEnumerable<NatureOfBusinessEntities> GetAllNatureOfBusiness();
        IEnumerable<OwnerShipEntites> GetAllOwnerShip();
        bool CompanyLogoUpload(AddLogo logo);
    }
}
