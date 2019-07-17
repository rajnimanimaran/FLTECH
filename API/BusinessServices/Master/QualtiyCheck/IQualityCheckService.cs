using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities.Master1;

namespace BusinessServices.Master.QualtiyCheck
{
   public interface IQualityCheckService
    {

         IEnumerable<GetAllQCMDetails> GetAllQCheck();
        GetAllQCMDetails GetAllQCMDetails(int QCID);
        bool Create(InsertQCEntity obj);
        bool Update(int QCID, UpdateQCEntity obj);
        bool Delete(int QCID);

    }
}
