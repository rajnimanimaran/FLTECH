using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("UserMenuMapping")]
    public class UserMenuMapping : BaseModel
    {

        public string ActionList { get; set; }

        //Foreign Key

        [ForeignKey("Menu")]
        [Column(Order = 0), Key]
        public int MenuId { get; set; }

        [ForeignKey("User")]
        [Column(Order = 1), Key]
        public long UserId { get; set; }

        //Virtual Property
        public virtual User User { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
