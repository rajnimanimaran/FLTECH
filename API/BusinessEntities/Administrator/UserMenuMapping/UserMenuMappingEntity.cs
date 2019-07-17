using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class UserMenuMappingEntity
    {
        public int UserMenuMappingId { get; set; }
        public string ActionList { get; set; }
        public int MenuId { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
