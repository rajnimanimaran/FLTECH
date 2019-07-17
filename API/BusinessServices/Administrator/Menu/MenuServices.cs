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
    public class MenuServices : IMenuServices
    {
        private readonly UnitOfWork _unitOfWork;

        public MenuServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public BusinessEntities.MenuEntity GetMenuById(int MenuId)
        {
            var menu = _unitOfWork.MenuRepository.GetByID(MenuId);
            if (menu != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DataModel.Menu, MenuEntity>();
                });

                IMapper mapper = config.CreateMapper();

                var menuModel = mapper.Map<DataModel.Menu, MenuEntity>(menu);
                return menuModel;
            }
            return null;
        }


        public IEnumerable<BusinessEntities.MenuEntity> GetAllMenus()
        {
            var menus = _unitOfWork.MenuRepository.GetAll().ToList();
            if (menus.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DataModel.Menu, MenuEntity>();
                });
                IMapper mapper = config.CreateMapper();

                var menuModel = mapper.Map<List<DataModel.Menu>, List<MenuEntity>>(menus);
                return menuModel;
            }

            return null;
        }

       




    }
}
