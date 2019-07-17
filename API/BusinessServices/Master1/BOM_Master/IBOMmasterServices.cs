using BusinessEntities.BOM_Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.BOM_Master
{
    public interface IBOMmasterServices
    {
        GetBOM GetAllBOM_Master(int BOMID);
        bool CreateBOM_master(BOM_masterEntity obj);
        bool UpdateBOM_master(int BOMID, BOM_masterEntity obj);
        bool DeleteBOM_master(int BOMID);
    }
}
