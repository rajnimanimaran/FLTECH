using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("RoleMenuMapping")]
    public class RoleMenuMapping : BaseModel
    {

        public string ActionList { get; set; }


        //Foreign Key

        [ForeignKey("Menu")]
        [Column(Order = 0), Key]
        public int MenuId { get; set; }

        [ForeignKey("Role")]
        [Column(Order = 1), Key]
        public int RoleId { get; set; }

        //Virtual Property
        public virtual Role Role { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
