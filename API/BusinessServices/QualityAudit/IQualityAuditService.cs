﻿using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.QualityAudit
{
   public  interface IQualityAuditService
    {
        bool CreateQCAudit(QualityAuditEntity obj);
        IEnumerable<QualityAuditEntity> GetAllQCAuditDetails();
        bool UpdateQCAuditDetails(QualityAuditEntity obj);
        //bool Create(QualityAuditEntity obj);

    }

}
