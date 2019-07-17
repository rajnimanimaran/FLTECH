using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("StoreRaw")]
    public class StoreRaw : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StoreId { get; set; }
        public string RawMaterialName { get; set; }
        public string UOMName { get; set; }
        public string UOMCode { get; set; }
        public string BaseUnits { get; set; }
        public int Quantity { get; set; }

        // Foriegn Key
        [ForeignKey("RawMaterial")]
        public int RawMaterialId { get; set; }

        // Virtual Properties      
        public virtual RawMaterial RawMaterial { get; set; }

        // Foriegn Key
        [ForeignKey("UOM")]
        public int UOMId { get; set; }

        // Virtual Properties      
        public virtual UOM UOM { get; set; }


    }
}
