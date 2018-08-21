using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_BusinessObject
{
    public class TaskDetails
    {
        public int Task_ID { get; set; }
        public string Task_Name { get; set; }
        public string Task_Duration { get; set; }
        public string Team_Name { get; set; }
        public List<string> MemberList { get; set; }
        public Nullable<System.DateTime> Started_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public string Status { get; set; }

    }
}
