using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReservaEnLinea.ViewModels
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
        public string Url { get; set; }
        public string Funcion { get; set; }
        public int Id { get; set; }
        public string UrlTabla { get; set; }

        public string DivTabla { get; set; }
        public string Html { get; set; }

    }
}
