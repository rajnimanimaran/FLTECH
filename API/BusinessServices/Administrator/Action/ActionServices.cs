using AutoMapper;
using BusinessEntities;
using BusinessServices;
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
    public class ActionServices : IActionServices
    {
        private readonly UnitOfWork _unitOfWork;

        public ActionServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionEntity GetActionById(int ActionId)
        {
            var action = _unitOfWork.ActionRepository.GetByID(ActionId);
            if (action != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DataModel.Menu, BusinessEntities.MenuEntity>();
                    cfg.CreateMap<DataModel.Action, ActionEntity>();
                });

                IMapper mapper = config.CreateMapper();

                var actionModel = mapper.Map<DataModel.Action, ActionEntity>(action);

                //actionModel.Menu = new MenuEntity
                //{
                //    MenuId = action.Menu.MenuId,
                //    MenuName = action.Menu.MenuName
                //};

                return actionModel;
            }
            return null;
        }


        public IEnumerable<ActionEntity> GetAllActions()
        {
            var actions = _unitOfWork.ActionRepository.GetAll().ToList();
            if (actions.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DataModel.Menu, BusinessEntities.MenuEntity>();
                    cfg.CreateMap<DataModel.Action, ActionEntity>();
                });
                IMapper mapper = config.CreateMapper();

                var actionModel = mapper.Map<List<DataModel.Action>, List<ActionEntity>>(actions);
                return actionModel;
            }

            return null;
        }

        public int CreateAction(ActionEntity actionEntity)
        {
            using (var scope = new TransactionScope())
            {
                var action = new DataModel.Action
                {
                    MenuId = actionEntity.MenuId,
                    Name = actionEntity.Name,
                    Remarks = actionEntity.Remarks,
                    IsActive = actionEntity.IsActive,
                    CreatedBy = actionEntity.CreatedBy,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = actionEntity.CreatedBy,
                    ModifiedOn = DateTime.Now,
                };
                _unitOfWork.ActionRepository.Insert(action);
                _unitOfWork.Save();
                scope.Complete();
                return action.ActionId;

            }
        }

        public bool UpdateAction(int ActionId,ActionEntity actionEntity)
        {
            var success = false;
            if (actionEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var action = _unitOfWork.ActionRepository.GetByID(ActionId);
                    if (actionEntity != null)
                    {
                        
                        action.MenuId = actionEntity.MenuId;
                        action.Name = actionEntity.Name;
                        action.Remarks = actionEntity.Remarks;
                        action.IsActive = actionEntity.IsActive;
                        action.ModifiedBy = actionEntity.ModifiedBy;
                        action.ModifiedOn = DateTime.Now;
                        _unitOfWork.ActionRepository.Update(action);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool DeleteAction(int ActionId)
        {
            var success = false;
            if (ActionId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var action = _unitOfWork.ActionRepository.GetByID(ActionId);
                    if (ActionId != 0)
                    {
                        _unitOfWork.ActionRepository.Delete(action);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }

            }
            return success;
        }




        
    }
}
