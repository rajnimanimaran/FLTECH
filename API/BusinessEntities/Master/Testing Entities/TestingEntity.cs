using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class TestingEntity
    {
        public int TestingCheckListId { get; set; }
        public int TestId { get; set; }
        public string TypesOfTest { get; set; }
        public string CheckListName { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }

    }
}
