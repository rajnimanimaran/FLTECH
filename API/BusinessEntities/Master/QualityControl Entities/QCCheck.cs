using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Master.QualityControl_Entities
{
        public class QualityCheckEntity
        {
            public int QCID { get; set; }
            public string QCCode { get; set; }
            public string QCName { get; set; }
            public int PrdID { get; set; }
            public string prdName { get; set; }
            public bool IsActive { get; set; }
            public int ActionBy { get; set; }
        }
        public class InsertQCEntity
        {
            public string QCCode { get; set; }
            public string QCName { get; set; }
            public int PrdID { get; set; }
            public bool IsActive { get; set; }
            public int ActionBy { get; set; }
            public string QCDetails { get; set; }
        }
        public class UpdateQCEntity
        {
            public int QCID { get; set; }
            public string QCCode { get; set; }
            public string QCName { get; set; }
            public int PrdID { get; set; }
            public bool IsActive { get; set; }
            public int ActionBy { get; set; }
            public string QCDetails { get; set; }
        }
        public class GetAllQCMDetails
        {
            public QCCheckMaster QCCheckMaster { get; set; }
            public List<QCCheckDetails> QCCheckDetails { get; set; }
        }
        public class QCCheckMaster
        {
            public int QCID { get; set; }
            public string QCCode { get; set; }
            public string QCName { get; set; }
            public int PrdID { get; set; }
        }
        public class QCCheckDetails
        {
            public int QCID { get; set; }
            public int ChkListID { get; set; }
            public string ChkName { get; set; }
            
        }

        public class GetPrdName
        {
            public int prdID { get; set; }
            public string prdName { get; set; }
        }

   
}
