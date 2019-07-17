using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class AreaEntity
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public string Pincode { get; set; }
        public int CountryId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        public CountryEntity Country { get; set; }
        public StateEntity State { get; set; }
        public CityEntity City { get; set; }
    }
}
