using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class ActionEntity
    {
        public int ActionId { get; set; }
        public int MenuId { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public MenuEntity Menu { get; set; }
    }
}