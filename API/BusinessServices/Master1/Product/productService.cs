using BusinessEntities.Master1.product;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Master1.Product
{
    public class productService : IproductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public productService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<productEntity> GetAllproduct()
        {
            SqlCommand cmd = new SqlCommand("PRD_spGetProduct");
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<productEntity>(cmd);
            return locMas;
        }

        public IEnumerable<ProductMasterEntity> GetProductMaster(int prdID)
        {
            SqlCommand cmd = new SqlCommand("spGetProduct_Master");
            cmd.Parameters.AddWithValue("@p_prdID", prdID);
            cmd.CommandType = CommandType.StoredProcedure;
            var product = _unitOfWork.DbLayer.GetEntityList<ProductMasterEntity>(cmd);

            return product;

        }
        public IEnumerable<UOMMasterEntity> GetUOMDetails()
        {
            SqlCommand cmd = new SqlCommand("UOM_spgetUOM");
            cmd.CommandType = CommandType.StoredProcedure;
            var uom = _unitOfWork.DbLayer.GetEntityList<UOMMasterEntity>(cmd);

            return uom;
        }
        public bool Createproduct(productEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("PRD_spSaveProduct");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_prdCode", obj.prdCode);
            cmd.Parameters.AddWithValue("@p_prdName", obj.prdName);
            cmd.Parameters.AddWithValue("@p_UOMID", obj.UOMID);

            cmd.Parameters.AddWithValue("@p_splitable", obj.splitable);
            cmd.Parameters.AddWithValue("@p_stock", obj.stock);
            cmd.Parameters.AddWithValue("@p_re_Orderlevel", obj.re_Orderlevel);
            cmd.Parameters.AddWithValue("@p_HSNCode", obj.HSNCode);
            cmd.Parameters.AddWithValue("@p_sell_price", obj.sell_price);
            cmd.Parameters.AddWithValue("@p_cost_price", obj.cost_price);
            cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
            //  cmd.Parameters.AddWithValue("@createdOn", obj.createdOn);
            // cmd.Parameters.AddWithValue("@modifiedOn", obj.modifiedOn);
            // cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }
        public bool Updateproduct(int prdID, productEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("PRD_spSaveProduct");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_prdID", obj.prdID);
            cmd.Parameters.AddWithValue("@p_prdCode", obj.prdCode);
            cmd.Parameters.AddWithValue("@p_prdName", obj.prdName);
            cmd.Parameters.AddWithValue("@p_UOMID", obj.UOMID);
            cmd.Parameters.AddWithValue("@p_splitable", obj.splitable);
            cmd.Parameters.AddWithValue("@p_stock", obj.stock);
            cmd.Parameters.AddWithValue("@p_re_Orderlevel", obj.re_Orderlevel);
            cmd.Parameters.AddWithValue("@p_HSNCode", obj.HSNCode);
            cmd.Parameters.AddWithValue("@p_sell_price", obj.sell_price);
            cmd.Parameters.AddWithValue("@p_cost_price", obj.cost_price);
            cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
            cmd.Parameters.AddWithValue("@p_IsActive", obj.IsActive);
            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;

        }
        public bool Deleteproduct(int prdID)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("PRD_spRemoveProduct");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_prdID", prdID);
            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;

        }

    }
}

