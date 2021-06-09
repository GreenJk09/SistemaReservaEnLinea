using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaReservaEnLinea.Web.Models
{
    public class Paypal
    {
        public string ApiAppName { get; set; }
        public string Account { get; set; }
        public string ClientID { get; set; }
        public string Secret { get; set; }
    }
}
