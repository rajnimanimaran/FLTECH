
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("Department")]
    public class Department : BaseModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int ParentId { get; set; }
        public string DepartmentCode { get; set; }


        //Foreign Key

        [ForeignKey("Company")]
        [Column("CompanyId")]
        public int CompanyId { get; set; }

        //Virtual properties
        public virtual Company Company { get; set; }
        public virtual ICollection<Designation> Designation { get; set; }
    }
}
