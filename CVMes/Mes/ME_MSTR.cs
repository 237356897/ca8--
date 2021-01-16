using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CVMes
{
    public class ME_MSTR
    {
        public string Step_Order = "1";
        public string Step_Source = "ME";
        public string ProductionTime = "";//实时，检测的开始时间
        public string ErrorCode = "0";
        public string ErrorMessage = "";
        public string TotalTime = "";      //实时，检测的总时间
        public string StepName = "";
        public string Step_Status = "";   // 实时
        public string StepType = "";
        public string Step_PassFail = "";
        public string Units = "";
        public string Comp = "";
        public string LimitStep_Data = "0";
        public string limitLow = "0";
        public string limitHigh = "0";
        public string Result_String = "";
        public string limits_String = "";
    }
}
