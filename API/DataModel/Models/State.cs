using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("StateMaster")]
    public class State : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StateId { get; set; }
        public string StateName { get; set; }

        //Foreign Key

        [ForeignKey("Country")]
        [Column("CountryId")]
        public int CountryId { get; set; }

        // Virtual Properties
        public virtual Country Country { get; set; }
        public virtual ICollection<City> City { get; set; }
        public virtual ICollection<Area> Area { get; set; }
        public virtual ICollection<Company> Company { get; set; }


    }
}
