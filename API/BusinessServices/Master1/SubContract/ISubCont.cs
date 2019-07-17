using BusinessEntities.Master1.SubContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Master1.SubContract
{
  public  interface ISubCont
    {

        IEnumerable<SubContEntity> GetAllsub();
        bool Createsub(SubContEntity obj);
        bool Updatesub(int subContID, SubContEntity obj);
        bool Deletesub(int subContID);

    }
}
