using AutoMapper;
using BusinessEntities.sodm;
using DataModel.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BusinessServices.sodm
{
    public class sodmService : IsodmService
    {
        private readonly IUnitOfWork _unitOfWork;

        public sodmService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

       

        public IEnumerable<sodmgEntity> GetAllsodm()
        {
            SqlCommand cmd = new SqlCommand("spGetSalesOrderDetails");
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<sodmgEntity>(cmd);
            return locMas;
        }

        //public IEnumerable<sodmsodEntity> GetAllsodmupdate(int OrderID)
        //{
        //    SqlCommand cmd = new SqlCommand("getSalesDetails");
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    var locMas = _unitOfWork.DbLayer.GetEntityList<sodmsodEntity>(cmd);
        //    return locMas;
        //}

        //public IEnumerable<BusinessEntities.sodmsodEntity> GetStateByCountryId(int CountryId)
        //{
        //    var stateList = _unitOfWork.StateRepository.GetMany(a => a.CountryId == CountryId && a.IsActive == true).ToList();

        //    if (stateList.Any())
        //    {
        //        var config = new MapperConfiguration(cfg =>
        //        {
        //            cfg.CreateMap<Country, CountryEntity>();
        //            cfg.CreateMap<State, StateEntity>();
        //        });

        //        IMapper mapper = config.CreateMapper();
        //        var StateModel = mapper.Map<List<State>, List<sodmsodEntity>>(stateList);
        //        return StateModel;
        //    }
        //    return new List<sodmsodEntity>();
        //}

        public bool Create(sodmsomdEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("SP_SODInsert");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_OrderNumber", obj.OrderNumber);
            cmd.Parameters.AddWithValue("@p_CODE", obj.CODE);
            cmd.Parameters.AddWithValue("@p_CustomerID", obj.CustomerID);
            cmd.Parameters.AddWithValue("@p_OrderDate", obj.OrderDate);
            cmd.Parameters.AddWithValue("@p_ACKDate", obj.ACKDate);
            cmd.Parameters.AddWithValue("@p_OrderTakenBy", obj.OrderTakenBy);
            cmd.Parameters.AddWithValue("@p_AssignedTo", obj.AssignedTo);
            cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
            cmd.Parameters.AddWithValue("@p_IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@p_OrderDetails",obj.OrderDetails);
            //cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }



    }
}
