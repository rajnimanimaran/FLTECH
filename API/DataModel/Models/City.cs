using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("City")]
    public class City : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityId { get; set; }
        public string CityName { get; set; }

        //Foreign Key

        [ForeignKey("State")]
        [Column("StateId")]
        public int StateId { get; set; }

        [ForeignKey("Country")]
        [Column("CountryId")]
        public int CountryId { get; set; }

        //Virtual properties
        public virtual ICollection<Area> Area { get; set; }
        public virtual ICollection<Company> Company { get; set; }


        public virtual State State { get; set; }
        public virtual Country Country { get; set; }

    }
}
