
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("Area")]
    public class Area : BaseModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public string Pincode { get; set; }

        //Foreign Key
        [ForeignKey("City")]
        [Column("CityId")]
        public int CityId { get; set; }

        [ForeignKey("State")]
        [Column("StateId")]
        public int StateId { get; set; }

        [ForeignKey("Country")]
        [Column("CountryId")]
        public int CountryId { get; set; }

        //virtual properties       
        public virtual City City { get; set; }
        public virtual State State { get; set; }
        public virtual Country Country { get; set; }

        public virtual ICollection<Company> Company { get; set; }

    }


}

