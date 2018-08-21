using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_DataAccessLayer
{
    public class DBConnection
    {


        public static string connection()
        {
            return "Data Source=.;Initial Catalog=EMS;Integrated Security=True;MultipleActiveResultSets=True";
        }
      
    }
}
