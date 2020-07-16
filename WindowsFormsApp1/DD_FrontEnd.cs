using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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




        public (string KNinfo, string Artikel, string UNnBera, string Dienstl, string LU, string LR)  fileLastmod(string pfad1, string pfad2, string pfad3, string pfad4, string pfad5, string pfad6)
        {

         string  KNinfo= System.IO.File.GetLastWriteTime(pfad1).ToString("dd/MM/yy HH:mm:ss");
         string Artikel = System.IO.File.GetLastWriteTime(pfad2).ToString("dd/MM/yy HH:mm:ss");
         string UNnBera = System.IO.File.GetLastWriteTime(pfad3).ToString("dd/MM/yy HH:mm:ss");
         string Dienstl = System.IO.File.GetLastWriteTime(pfad4).ToString("dd/MM/yy HH:mm:ss");
         string LU = System.IO.File.GetLastWriteTime(pfad5).ToString("dd/MM/yy HH:mm:ss");
         string LR = System.IO.File.GetLastWriteTime(pfad6).ToString("dd/MM/yy HH:mm:ss");

         return (KNinfo, Artikel, UNnBera,Dienstl,LU,LR);

        }

        
    }
}
