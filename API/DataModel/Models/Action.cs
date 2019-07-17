using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("Action")]
    public class Action : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActionId { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }


        // Foriegn Key
        [ForeignKey("Menu")]
        public int MenuId { get; set; }

        // Virtual Properties      
        public virtual Menu Menu { get; set; }



    }
}