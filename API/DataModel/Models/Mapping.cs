using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("Mapping")]

    public class Mapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MRPId { get; set; }

        // Foriegn Key
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        // Virtual Properties      
        public virtual Product Product { get; set; }

        [ForeignKey("RawMaterial")]
        public int RawMaterialId { get; set; }

        // Virtual Properties      
        public virtual RawMaterial RawMaterial { get; set; }

    }
}
