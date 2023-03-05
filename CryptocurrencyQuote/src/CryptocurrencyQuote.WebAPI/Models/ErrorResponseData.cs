using Newtonsoft.Json;

namespace CryptocurrencyQuote.WebAPI.Models
{
    public class ErrorResponseData
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
