using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class DD_FrontEnd
    {

        public string SerienNummer(string EB, int lfdn)
        {
            string serie;
            serie = EB + lfdn.ToString("000");
            return serie;
        }
    }
}
