using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessEntities
{

  public class QualityAuditEntity
    {
        public int QCAuditID { get; set; }
        public int QCID { get; set; }
        public int WOPRDSerialID { get; set; }
        public int StatusID { get; set; }
        public int ActionBy { get; set; }
        public bool IsActive { get; set; }
        public int WODetlID { get; set; }
        public int ApprovedQuantity { get; set; }
        public int RejectedQuantity { get; set; }
        public int ReworkQuantity { get; set; }
        public int prdID { get; set; }
        public string QCAuditDetails { get; set; }
        //public int QCID { get; set; }
        //public int WOPRDSerialID { get; set; }
        //public int StatusID { get; set; }
        //public int IsActive { get; set; }
        //public string QCAuditDetails { get; set; }
        //public int ActionBy { get; set; }

    }
}
