using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("Store")]
    public class Store : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StoredId { get; set; }
        public string Quantity { get; set; }


        // Foriegn Key
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        // Virtual Properties      
        public virtual Product Product { get; set; }



    }
}
