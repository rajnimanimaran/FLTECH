//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DataModel
//{
//    [Table("EmployeeDocuments")]
//    public class EmployeeDocuments : BaseModel
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int DocumentId { get; set; }
//        public string DocumentName { get; set; }
//        [Column("DocAttachment")]
//        public string Attachment { get; set; }
//        public string Remarks { get; set; }

//        [ForeignKey("Company")]
//        [Column("CompanyId")]
//        public int CompanyId { get; set; }

//        [ForeignKey("Employee")]
//        [Column("EmployeeId")]
//        public int EmployeeId { get; set; }

//        //virtual properties
//        public virtual Employee Employee { get; set; }
//        public virtual Company Company { get; set; }
//    }
//}
