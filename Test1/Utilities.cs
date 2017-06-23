using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    class Utilities
    {
        public string GetConnectionString()
        {
            string current_dir = System.Reflection.Assembly.GetExecutingAssembly().Location;//because the database is currenly
            for (int i = 0; i < 3; i++)
            {
                current_dir = current_dir.Substring(0, current_dir.LastIndexOf('\\'));
            }

            return  (@" Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + current_dir + @"\DB1TEST.mdf ; Integrated Security = True; Connect Timeout = 30 ");
        }
    }
}
