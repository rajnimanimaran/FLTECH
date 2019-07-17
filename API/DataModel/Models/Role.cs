using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("RoleMaster")]
    public class Role : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }
        public string RoleName { get; set; }


        // Foriegn Key
        [ForeignKey("OrganizationLevel")]
        [Column("OrgLvlId")]
        public int OrganizationLevelId { get; set; }

        // Virtual Properties
        public virtual OrganizationLevel OrganizationLevel { get; set; }
    }
}
