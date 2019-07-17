using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("NatureOfBusiness")]
    public class NatureOfBusiness : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BusinessId { get; set; }
        public string BusinessName { get; set; }
        //public bool IsActive { get; set; }
        //public int CreatedBy { get; set; }
        //public int ModifiedBy { get; set; }

    }
}
