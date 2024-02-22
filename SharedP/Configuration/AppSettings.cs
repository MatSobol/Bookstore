using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration
{
    public class AppSettings
    {
        public string DefaultLanguage { get; set; }
        public string BaseAPIUrl { get; set; }
        public BaseBookEndpoint BaseBookEndpoint { get; set; }
    }
}
