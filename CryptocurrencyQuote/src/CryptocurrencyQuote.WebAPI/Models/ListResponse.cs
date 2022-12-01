using System.Collections.ObjectModel;

namespace CryptocurrencyQuote.WebAPI.Models
{
    public class ListResponse<T>
    {
        public List<T> Data { get; set; }
        public bool Success {get;set;}
    }
}
