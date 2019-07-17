using DataModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("RFQ")]
    public class RFQ : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RFQId { get; set; }
        //public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string MeasurementDetails { get; set; }
        //public int SupplierId { get; set; }
        public decimal RatePerQuantity { get; set; }
        public int Indent { get; set; }
        public decimal Mobilization { get; set; }
        //public int TermsAndConditionId { get; set; }
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
        public string TypeOfProductManufacture { get; set; }
        public string Documents { get; set; }

        // Foriegn Key
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }

        // Virtual Properties      
        public virtual Supplier Supplier { get; set; }

        // Foriegn Key
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        // Virtual Properties      
        public virtual Product Product { get; set; }

        // Foriegn Key
        [ForeignKey("Terms")]
        public int TermsAndConditionId { get; set; }

        // Virtual Properties      
        public virtual Terms Terms { get; set; }

    }
}
