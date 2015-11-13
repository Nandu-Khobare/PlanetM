using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractLayer
{
    public class DiscrepancyReport
    {
        public int Order
        { set; get; }
        public string ReportName
        { set; get; }
        public string StoredProcedureForReport
        { set; get; }

        public DiscrepancyReport(int order,string reportName,string SPForReport)
        {
            Order = order;
            ReportName = reportName;
            StoredProcedureForReport = SPForReport;
        }
    }
}
