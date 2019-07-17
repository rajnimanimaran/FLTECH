
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("Designation")]
    public class Designation : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public int Superior { get; set; }
        public string DesignationCode { get; set; }


        //Foreign Key

        [ForeignKey("Company")]
        [Column("CompanyId")]
        public int CompanyId { get; set; }

        [ForeignKey("Department")]
        [Column("DepartmentId")]
        public int DepartmentId { get; set; }

        // Virtual Properties
        public virtual Company Company { get; set; }
        public virtual Department Department { get; set; }


    }
}
