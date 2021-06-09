using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReservaEnLinea.Tools
{
   public  class Generics
    {
        public static string NameFile()
        {
            return "FileUpload_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond;
        }
    }
}
