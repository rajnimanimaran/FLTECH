using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("OrganizationLevel")]
    public class OrganizationLevel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("OrgLvlId")]
        public int OrganizationLevelId { get; set; }
        public string LevelName { get; set; }
        public int Parent { get; set; }
        public string Code { get; set; } 


        // Virtual Properties
        public virtual ICollection<Role> Roles { get; set; }
    }
}
