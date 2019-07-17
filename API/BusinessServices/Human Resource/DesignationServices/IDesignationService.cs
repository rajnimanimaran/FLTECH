using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;


namespace BusinessServices
{
    public interface IDesignationService
    {
        IEnumerable<DesignationEntity> GetDesignationById(int designationid);
        // DesignationEntity GetDesignationById(int DesignationId);
        IEnumerable<DesignationEntity> GetAllDesignation(int companyId);
        IEnumerable<DesignationEntity> GetActiveDesignationById();
        IEnumerable<DesignationEntity> GetDesigByDepId(int depId);
        ResultDTO CreateDesignation(DesignationEntity DesignationEntity);
        ResultDTO UpdateDesignation(int DesignationId, DesignationEntity DesignationEntity);
        bool DeleteDesignation(int DesignationId);
        bool ToggleActiveDesignation(int DesignationId);
    }
}
