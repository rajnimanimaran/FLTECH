using AutoMapper;
using BusinessEntities;
using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessServices
{
    public class StateServices : IStateServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public StateServices(IUnitOfWork unit)
        {
            this._unitOfWork = unit;
        }

        public BusinessEntities.StateEntity GetStateById(int StateId)
        {
            var state = _unitOfWork.StateRepository.GetByID(StateId);
            if (state != null)
            {
                var config = new MapperConfiguration(cfg =>
                {

                    cfg.CreateMap<Country, CountryEntity>();
                    cfg.CreateMap<State, StateEntity>();
                });

                IMapper mapper = config.CreateMapper();

                var stateModel = mapper.Map<State, StateEntity>(state);
                return stateModel;
            }
            return null;
        }
        // Get Active State by country Id 
        public IEnumerable<BusinessEntities.StateEntity> GetStateByCountryId(int CountryId)
        {
            var stateList = _unitOfWork.StateRepository.GetMany(a => a.CountryId == CountryId && a.IsActive == true).ToList();

            if (stateList.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Country, CountryEntity>();
                    cfg.CreateMap<State, StateEntity>();
                });

                IMapper mapper = config.CreateMapper();
                var StateModel = mapper.Map<List<State>, List<StateEntity>>(stateList);
                return StateModel;
            }
            return new List<StateEntity>();
        }

        public IEnumerable<BusinessEntities.StateEntity> GetAllStates()
        {
            var stateList = _unitOfWork.StateRepository.GetAll().OrderByDescending(x => x.StateId).ToList();
            if (stateList.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Country, CountryEntity>();
                    cfg.CreateMap<State, StateEntity>();
                });

                IMapper mapper = config.CreateMapper();
                var StateModel = mapper.Map<List<State>, List<StateEntity>>(stateList);
                return StateModel;
            }
            return null;
        }
        public IEnumerable<BusinessEntities.StateEntity> GetActiveStateById()
        {
            var stateList = _unitOfWork.StateRepository.GetMany(a => a.IsActive == true).ToList();
            if (stateList.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Country, CountryEntity>();
                    cfg.CreateMap<State, StateEntity>();

                });

                IMapper mapper = config.CreateMapper();
                var StateModel = mapper.Map<List<State>, List<StateEntity>>(stateList);
                return StateModel;
            }
            return new List<StateEntity>();
        }

        public ResultDTO CreateState(BusinessEntities.StateEntity StateEntity)
        {
            var result = new ResultDTO { IsSuccess = false };

            var isExist = _unitOfWork.StateRepository.GetManyQueryable(c => c.StateName.ToLower() == StateEntity.StateName.ToLower() && c.CountryId == StateEntity.CountryId).Count() > 0;
            if (!isExist)
            {

                using (var scope = new TransactionScope())
                {
                    var State = new State
                    {
                        StateName = StateEntity.StateName,
                        CountryId = StateEntity.CountryId,
                        IsActive = true,
                        CreatedBy = StateEntity.CreatedBy,
                        CreatedOn = DateTime.Now,
                        ModifiedBy = StateEntity.CreatedBy,
                        ModifiedOn = DateTime.Now,


                    };
                    _unitOfWork.StateRepository.Insert(State);
                    _unitOfWork.Save();
                    scope.Complete();

                    result.IsSuccess = true;
                    result.Message = "Inserted State Successfully";

                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "State name already exist";
            }

            return result;
        }

        public ResultDTO UpdateState(int StateId, BusinessEntities.StateEntity StateEntity)
        {
            var result = new ResultDTO { IsSuccess = false };

            if (StateEntity != null)
            {


                using (var scope = new TransactionScope())
                {
                    var stateent = _unitOfWork.StateRepository.GetByID(StateId);
                    if (stateent != null)
                    {
                        stateent.StateId = StateEntity.StateId;
                        stateent.StateName = StateEntity.StateName;
                        stateent.CountryId = StateEntity.CountryId;
                        stateent.IsActive = StateEntity.IsActive;
                        stateent.ModifiedBy = StateEntity.ModifiedBy;
                        stateent.ModifiedOn = DateTime.Now;



                        _unitOfWork.StateRepository.Update(stateent);
                        _unitOfWork.Save();
                        scope.Complete();

                        result.IsSuccess = true;
                        result.Message = "Updated State Successfully";
                    }
                }


            }
            return result;

        }

        public bool DeleteState(int StateId)
        {
            var success = false;
            if (StateId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var Employee = _unitOfWork.StateRepository.GetByID(StateId);
                    if (Employee != null)
                    {

                        _unitOfWork.StateRepository.Delete(Employee);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool ToggleActiveState(int StateId)
        {
            var success = false;
            if (StateId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var state = _unitOfWork.StateRepository.GetByID(StateId);
                    if (state != null)
                    {
                        state.IsActive = !state.IsActive;
                        state.ModifiedBy = 1;
                        state.ModifiedOn = DateTime.Now;

                        _unitOfWork.StateRepository.Update(state);
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