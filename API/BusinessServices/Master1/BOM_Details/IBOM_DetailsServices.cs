using BusinessEntities.BOM_Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.BOM_Details
{
  public  interface IBOM_DetailsServices
    {
        IEnumerable<BOM_DetailsEntity> GetAllBOM_Details();
        bool CreateBOM_Details(BOM_DetailsEntity obj);
        bool UpdateBOM_Details(int BOMD_ID, BOM_DetailsEntity obj);
        bool DeleteBOM_Details(int BOMD_ID);
    }
}
