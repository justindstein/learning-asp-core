using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace learning_asp_core.Utils.Extensions
{
    public static class HttpClientExtension
    {
        public static void ApplyBasicCredentials(this HttpClient client, String username, String password)
        {
            string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);

        }
    }
}
