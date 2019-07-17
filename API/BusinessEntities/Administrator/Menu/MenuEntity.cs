using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessEntities
{
    public class MenuEntity
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }
        public string MenuIcon { get; set; }
        public int MenuOrder { get; set; }
        public int ParentMenu { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class MenuItemsEntity
    {
        public bool IsSelected { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuIcon { get; set; }
        public int MenuOrder { get; set; }
        public string MenuUrl { get; set; }
        public List<MenuItemsEntity> SubMenuItems { get; set; }
        public int ParentId { get; set; }
        public List<Actions> MenuAction { get; set; }
    }

    public class Actions
    {
        public int ActionId { get; set; }
        public string ActionName { get; set; }
        public bool IsSelected { get; set; }
    }
}
