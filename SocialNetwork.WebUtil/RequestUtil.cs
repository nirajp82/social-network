using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace SocialNetwork.WebUtil
{
    public class RequestUtil
    {
        public static async Task<string> GetBodyAsync(HttpRequest request)
        {
            if (request.ContentLength > 0)
            {
                request.EnableBuffering();
                // Leave the body open so the next middleware can read it.
                using (var reader = new StreamReader(request.Body))
                {
                    string body = await reader.ReadToEndAsync();
                    // Reset the request body stream position so the next middleware can read it
                    request.Body.Seek(0, SeekOrigin.Begin);

                    return body;
                }
            }
            return string.Empty;
        }
    }
}
