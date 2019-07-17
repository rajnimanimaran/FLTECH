using BusinessEntities;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public class RawMaterialServices : IRawMaterialServices
    {
        private readonly UnitOfWork _unitOfWork;

        public RawMaterialServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public List<RawMaterialEntity> GetRawMaterialById(int RawMaterialId)
        //{
        //    SqlCommand cmd = new SqlCommand("selectdata");

        //    cmd.Parameters.AddWithValue("@RawMaterialId", RawMaterialId);


        //    cmd.CommandType = CommandType.StoredProcedure;

        //    var locMas = _unitOfWork.DbLayer.GetEntityList<ProductEntity>(cmd);

        //    return locMas;
        //}


        public IEnumerable<RawMaterialEntity> GetAllRawMaterials()
        {
            SqlCommand cmd = new SqlCommand("sp_Select_rawMaterialMaste");
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<RawMaterialEntity>(cmd);

            return locMas;

        }

        public bool CreateRawMaterial(RawMaterialEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("sp_Insert_rawMaterialMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RawMaterialName", obj.RawMaterialName);
            cmd.Parameters.AddWithValue("@CreatedBy", obj.CreatedBy);
            var locMas = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMas != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }

        public bool UpdateRawMaterial(int RawMaterialId, RawMaterialEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("sp_Update_rawMaterialMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RawMaterialId", obj.RawMaterialId);
            cmd.Parameters.AddWithValue("@RawMaterialName", obj.RawMaterialName);
            cmd.Parameters.AddWithValue("@ModifiedBy", obj.ModifiedBy);
            cmd.Parameters.AddWithValue("@ModifiedOn", DateTime.Now);
            var locMas = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMas != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }

        public bool DeleteRawMaterial(int RawMaterialId)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("sp_Delete_rawMaterialMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RawMaterialId",RawMaterialId);
            var locMas = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMas != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }





    }
}
