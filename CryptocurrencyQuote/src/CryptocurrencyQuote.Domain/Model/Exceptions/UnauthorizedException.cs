﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyQuote.Domain.Model.Exceptions
{
    public class UnauthorizedException : Exception
    {
        private System.Net.HttpStatusCode _statusCode;
        public UnauthorizedException(string message) : base(message) {
            _statusCode = System.Net.HttpStatusCode.Unauthorized;
        }
        
    }
}
