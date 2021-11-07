using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;

namespace TechnicalityTestWebApp.Utility
{
    public static class HttpContentExtensions
    {

        public static async Task<T> ReadAsAsync<T>(this HttpContent content)
        {
           return await JsonSerializer.DeserializeAsync<T>(await content.ReadAsStreamAsync());
        }
    }
}
