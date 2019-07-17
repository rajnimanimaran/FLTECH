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
    public class UserMenuMappingServices : IUserMenuMappingServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserMenuMappingServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private UserMenuMapping mapMenuItemsEntityToMenu(long userId, MenuItemsEntity menuItem)
        {
            return new UserMenuMapping
            {
                UserId = userId,
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

        private List<UserMenuMapping> getUserMenuFromSubMenuList(long userId, List<MenuItemsEntity> menuList)
        {
            List<UserMenuMapping> roleMapping = new List<UserMenuMapping>();

            foreach (var menu in menuList)
            {
                if (menu.IsSelected && menu.SubMenuItems.Count == 0)
                {
                    roleMapping.Add(mapMenuItemsEntityToMenu(userId, menu));
                }
                else if (menu.IsSelected)
                {
                    roleMapping.Add(mapMenuItemsEntityToMenu(userId, menu));
                    roleMapping.AddRange(getUserMenuFromSubMenuList(userId, menu.SubMenuItems));
                }
            }

            return roleMapping;
        }

        public bool CreateUserMenuMapping(long userId, IEnumerable<MenuItemsEntity> menuMapDetails)
        {
            List<UserMenuMapping> userMapping = getUserMenuFromSubMenuList(userId, menuMapDetails.ToList());

            _unitOfWork.UserMenuMappingRepository.Delete(m => m.UserId == userId);
            userMapping.ForEach(m => _unitOfWork.UserMenuMappingRepository.Insert(m));
            _unitOfWork.Save();

            return true;
        }

        public IEnumerable<MenuItemsEntity> GetUserMenuMapDetails(long userId)
        {
            var userMenu = _unitOfWork.UserMenuMappingRepository.GetMany(m => m.UserId == userId).ToList();
            IEnumerable<RoleMenuMapping> roleMenu = new List<RoleMenuMapping>();
            if (userMenu.Count == 0)
            {
                int roleId = _unitOfWork.UserRepository.GetByID(userId).RoleId;

                roleMenu = _unitOfWork.RoleMenuMappingRepository.GetMany(m => m.RoleId == roleId).ToList();
            }

            List<Menu> lstMenus = _unitOfWork.MenuRepository.GetAll().ToList();

            List<MenuItemsEntity> lstresult = new List<MenuItemsEntity>();

            foreach (var menu in lstMenus)
            {
                var tempMenu = new MenuItemsEntity
                {
                    MenuId = menu.MenuId,
                    MenuName = menu.MenuName,
                    ParentId = menu.ParentMenu,
                    MenuUrl = menu.MenuUrl,
                    MenuIcon = menu.MenuIcon,
                    MenuOrder = menu.MenuOrder,
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
                if (userMenu.Count > 0)
                {
                    foreach (var map in userMenu)
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
                else
                {
                    foreach (var map in roleMenu)
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
                return list.OrderBy(m => m.MenuOrder).ToList();
            }
            else
            {
                return new List<MenuItemsEntity>();
            }
        }
    }
}
