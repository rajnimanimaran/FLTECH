using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("EmployeeAcademy")]
    public class EmployeeAcademy : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AcademyId { get; set; }
        public string Graduation { get; set; }
        public string Degree { get; set; }
        public string Specialization { get; set; }
        public string University { get; set; }
        public double Percentage { get; set; }
        public int YearofPassing { get; set; }
        public string Remarks { get; set; }
        public string DocAttachment { get; set; }

        [ForeignKey("Company")]
        [Column("CompanyId")]
        public int CompanyId { get; set; }

        [ForeignKey("Employee")]
        [Column("EmployeeId")]
        public int EmployeeId { get; set; }

        //virtual properties
        public virtual Employee Employee { get; set; }
        public virtual Company Company { get; set; }
    }
}
