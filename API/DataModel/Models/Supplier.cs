using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Models
{
    [Table("Supplier")]
    public class Supplier : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierId { get; set; }
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



    }
}
