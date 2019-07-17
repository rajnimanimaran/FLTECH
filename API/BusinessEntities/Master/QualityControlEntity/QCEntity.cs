using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class QCCommonEntity
    {
        public int CheckListID { get; set; }
        public int prdID { get; set; }
        public string prdName { get; set; }
        public bool IsActive { get; set; }
        public int ActionBy { get; set; }
        public string QCheck_tDetails { get; set; }
    }
    public class GetQCM
    {
        public int CheckListID { get; set; }
        public int prdID { get; set; }
        public string prdName { get; set; }
        public string CheckName { get; set; }
    }

    public class QCPrdName
    {
        public int prdID { get; set; }
        public string prdName { get; set; }
    }
    public class QCDetails
    {
        public int ID { get; set; }
        public int CheckListID { get; set; }
        public string CheckName { get; set; }
    }
    public class QCMaster
    {
        public int CheckListID { get; set; }
        public int prdID { get; set; }
        public string prdName { get; set; }
    }
    public class GetQcCheckList
    {
        public QCMaster QCMaster { get; set; }
        public List<QCDetails> QCDetails { get; set; }
    }
}