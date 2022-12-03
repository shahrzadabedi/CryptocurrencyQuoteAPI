using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyQuote.Domain.Model.Exceptions
{
    public class BadRequestException : Exception
    {
        private string _code;
        private System.Net.HttpStatusCode _statusCode;
        public BadRequestException(string code, string message) : base(message) {
            this._code = code;
            _statusCode = System.Net.HttpStatusCode.BadRequest;
        }
    }

   
}
