using AutoMapper;
using BusinessEntities;
using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessServices
{
    public class RoleMenuMappingServices : IRoleMenuMappingServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleMenuMappingServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private RoleMenuMapping mapMenuItemsEntityToMenu(int roleId, MenuItemsEntity menuItem)
        {
            return new RoleMenuMapping
            {
                RoleId = roleId,
                MenuId = menuItem.MenuId,
                ActionList = String.Join(",",
                                menuItem.MenuAction
                                    .Where(a => a.IsSelected == true)
                                    .Select(a => a.ActionId).ToList()),
                IsActive = true,
                CreatedBy = 1,
                ModifiedBy = 1,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            };
        }

        private List<RoleMenuMapping> getMenuFromSubMenuList(int roleId, List<MenuItemsEntity> menuList)
        {
            List<RoleMenuMapping> roleMapping = new List<RoleMenuMapping>();

            foreach (var menu in menuList)
            {
                if (menu.IsSelected && menu.SubMenuItems.Count == 0)
                {
                    roleMapping.Add(mapMenuItemsEntityToMenu(roleId, menu));
                }
                else if (menu.IsSelected)
                {
                    roleMapping.Add(mapMenuItemsEntityToMenu(roleId, menu));
                    roleMapping.AddRange(getMenuFromSubMenuList(roleId, menu.SubMenuItems));
                }
            }

            return roleMapping;
        }

        public bool CreateRoleMenuMapping(int roleId, IEnumerable<MenuItemsEntity> menuMapDetails)
        {
            List<RoleMenuMapping> roleMapping =  getMenuFromSubMenuList(roleId, menuMapDetails.ToList());

            _unitOfWork.RoleMenuMappingRepository.Delete(m => m.RoleId == roleId);
            roleMapping.ForEach(m => _unitOfWork.RoleMenuMappingRepository.Insert(m));
            _unitOfWork.Save();

            return true;
        }

        public IEnumerable<MenuItemsEntity> GetRoleMenuMapDetails(int roleId)
        {
            List<RoleMenuMapping> usermenu = _unitOfWork.RoleMenuMappingRepository.GetMany(m => m.RoleId == roleId).ToList();

            List<Menu> lstMenus = _unitOfWork.MenuRepository.GetAll().ToList();

            List<MenuItemsEntity> lstresult = new List<MenuItemsEntity>();


            foreach (var menu in lstMenus)
            {
                var tempMenu = new MenuItemsEntity
                {
                    MenuId = menu.MenuId,
                    MenuName = menu.MenuName,
                    ParentId = menu.ParentMenu,                    
                    MenuAction = new List<Actions>(),
                };
                foreach (var action in menu.Actions)
                {
                    var tempAction = new Actions()
                    {
                        ActionId = action.ActionId,
                        ActionName = action.Name,
                    };

                    tempMenu.MenuAction.Add(tempAction);
                }
                lstresult.Add(tempMenu);
            }

            foreach (var menu in lstresult)
            {
                foreach (var map in usermenu)
                {
                    if (menu.MenuId == map.MenuId)
                    {
                        menu.IsSelected = true;
                        var actionList = map.ActionList.Split(',');

                        foreach (var action in menu.MenuAction)
                        {
                            if (actionList.Contains(action.ActionId.ToString()))
                            {
                                action.IsSelected = true;
                            }
                        }
                    }
                }
            }

            var tempMenuList = new List<MenuItemsEntity>();

            foreach (var menu in lstresult.Where(m => m.ParentId == 0))
            {
                menu.SubMenuItems = getSubMenuList(menu, lstresult);
                tempMenuList.Add(menu);
            }

            return tempMenuList.OrderBy(m => m.MenuOrder).ToList();
        }

        private List<MenuItemsEntity> getSubMenuList(MenuItemsEntity menu, List<MenuItemsEntity> menuList)
        {
            if (menuList.Where(m => m.ParentId == menu.MenuId).ToList().Count > 0)
            {
                var list = (from m in menuList
                            where menu.MenuId == m.ParentId
                            select m).ToList();
                foreach (var m in list)
                {
                    m.SubMenuItems = getSubMenuList(m, menuList);
                }
                return list;
            }
            else
            {
                return new List<MenuItemsEntity>();
            }
        }
    }
}
