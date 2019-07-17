using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("Country")]
    public class Country : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }

        // Virtual Properties
        public virtual ICollection<State> States { get; set; }
        public virtual ICollection<Company> Company { get; set; }

    }
}
