using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("UOM")]
    public class UOM : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UOMId { get; set; }
        public string UOMName { get; set; }
        public string UOMCode { get; set; }
        public string BaseUnits { get; set; }

    }
}
