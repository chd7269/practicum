using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO
{
    public class HistoryDTO
    {
        //אותיות גדולות בתחילת השם
        public string Month { get; set; }
        public string Year { get; set; }
        public double? Expenses { get; set; }//הוצאות
        public double? Revenues { get; set; }//הכנסות


    }
}
