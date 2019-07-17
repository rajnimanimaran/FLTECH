using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("Employee")]
    public class Employee : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOJ { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string EmployeeCode { get; set; }
        public string FatherName { get; set; }
        public string BloodGroup { get; set; }
        public int? ReportingTo { get; set; }
        public string SpouseName { get; set; }
        public int? Children { get; set; }
        public string ProfilePhoto { get; set; }

        [ForeignKey("Company")]
        [Column("CompanyId")]
        public int CompanyId { get; set; }


        [ForeignKey("Department")]
        [Column("DepartmentId")]
        public int DepartmentId { get; set; }

        [ForeignKey("Designation")]
        [Column("DesignationId")]
        public int DesignationId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Department Department { get; set; }
        public virtual Designation Designation { get; set; }

        public virtual EmployeeAddress EmpAddress { get; set; }
        public virtual List<EmployeeAcademy> EmpAcademy { get; set; }
        // public List<EmployeeDocuments> EmpDocuments { get; set; }
        public virtual List<EmployeeExperience> EmpExperience { get; set; }

    }
}
