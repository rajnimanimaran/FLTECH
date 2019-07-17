using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("EmployeeExperience")]
    public class EmployeeExperience : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExperienceId { get; set; }
        public string Organization { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public double YearsofExperience { get; set; }
        public string Remarks { get; set; }

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
