using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("Company")]
    public class Company : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int ParentCompany { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Website { get; set; }
        public string CompanyLogo { get; set; }
        public string FaxNo { get; set; }
        public string Pincode { get; set; }



        //Foreign Key

        [ForeignKey("Country")]
        [Column("CountryId")]
        public int CountryId { get; set; }


        [ForeignKey("State")]
        [Column("StateId")]
        public int StateId { get; set; }

        [ForeignKey("City")]
        [Column("CityId")]
        public int CityId { get; set; }

        [ForeignKey("Area")]
        [Column("AreaId")]
        public int AreaId { get; set; }

        [ForeignKey("OrganizationLevel")]
        [Column("OrgLvlId")]
        public int? OrganizationLevelId { get; set; }

        [ForeignKey("NatureOfBusiness")]
        [Column("BusinessId")]
        public int? BusinessId { get; set; }

        [ForeignKey("OwnershipTypes")]
        [Column("TypeId")]
        public int? TypeId { get; set; }

        //Virtual properties
        public virtual Area Area { get; set; }
        public virtual City City { get; set; }
        public virtual State State { get; set; }
        public virtual Country Country { get; set; }
        public virtual OrganizationLevel OrganizationLevel { get; set; }
        public virtual OwnershipTypes OwnershipTypes { get; set; }
        public virtual NatureOfBusiness NatureOfBusiness { get; set; }

        public virtual ICollection<Designation> Designation { get; set; }
        public virtual ICollection<Department> Department { get; set; }

    }
}
