using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("Testing")]
    public class Testing : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestingCheckListId { get; set; }
        public string CheckListName { get; set; }

        // Foriegn Key
        [ForeignKey("Test")]
        public int TestId { get; set; }

        // Virtual Properties      
        public virtual Test Test { get; set; }



    }
}
