using CryptocurrencyQuote.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyQuote.Domain
{
    public interface IConfigurationAPI
    {
        CryptocurrencyAPIConfig Get();
    }
}
