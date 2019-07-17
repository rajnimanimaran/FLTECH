using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class RFQEntity
    {
        public int RFQId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int Quantity { get; set; }
        public string MeasurementDetails { get; set; }
        public int SupplierId { get; set; }
        public decimal RatePerQuantity { get; set; }
        public int Indent { get; set; }
        public decimal Mobilization { get; set; }
        public int TermsAndConditionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Notes { get; set; }
        public int RfqStatus { get; set; }
        public string SupplierCompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Fax { get; set; }
        public string MobileNumber { get; set; }
        public string WebSite { get; set; }
        public string MailId { get; set; }
        public string TypeOfEmployeeManufacture { get; set; }
        public string Documents { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }

    }
}
