using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using System.Data.SqlClient;
using System.Data;
using DataModel.UnitOfWork;

namespace BusinessServices
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unit)
        {
            _unitOfWork = unit;
        }
        public List <StudentDetails> GetStudentById(int StudentId)
        {
            SqlCommand cmd = new SqlCommand("selectdata");

            cmd.Parameters.AddWithValue("@StudentId",StudentId);
       

            cmd.CommandType = CommandType.StoredProcedure;

            var locMas = _unitOfWork.DbLayer.GetEntityList<StudentDetails>(cmd);

            return locMas;
        }
    }
}
