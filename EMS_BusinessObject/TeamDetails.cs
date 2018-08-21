using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_BusinessObject
{
    public class TeamDetails
    {
        public int Team_ID { get; set; }
        public string Team_Name { get; set; }
        public string Team_Lead { get; set; }
        public List<string> MemberList { get; set; }
    }
}
