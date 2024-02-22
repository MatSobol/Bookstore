using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedP
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

        private int _codeError = 200;
    
        public int CodeError 
        {
            get
            {
                return _codeError;
            }
            set
            {
                _codeError = value;
            }
        }
    }
}
