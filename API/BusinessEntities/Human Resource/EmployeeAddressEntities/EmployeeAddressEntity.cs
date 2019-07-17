using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class EmployeeAddressEntity
    {
        public int EmployeeId { get; set; }
        public string PerAddress1 { get; set; }
        public string PerAddress2 { get; set; }
        public string PerArea { get; set; }
        public string PerCity { get; set; }
        public string PerState { get; set; }
        public string PerCountry { get; set; }
        public string PerPincode { get; set; }
        public string PerEmailId { get; set; }
        public string PerMobile { get; set; }
        public string PerLandline { get; set; }
        public bool IsSameAddress { get; set; }
        public string CommAddress1 { get; set; }
        public string CommAddress2 { get; set; }
        public string CommArea { get; set; }
        public string CommCity { get; set; }
        public string CommState { get; set; }
        public string CommCountry { get; set; }
        public string CommPincode { get; set; }
        public string CommEmailId { get; set; }
        public string CommMobile { get; set; }
        public string CommLandline { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int CompanyId { get; set; }
    }

    public class EmployeeAddressCompanyDTO
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }

    public class EmployeeAddressEmployeeDTO
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
    }
}
