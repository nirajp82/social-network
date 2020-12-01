using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.WebUtil
{
    public class WebHelperFunc
    {
        public static async Task<string> GetBodyAsync(HttpRequest request)
        {
            if (request.ContentLength > 0)
            {
                request.EnableBuffering();
                var buffer = new byte[Convert.ToInt32(request.ContentLength)];
                await request.Body.ReadAsync(buffer, 0, buffer.Length);
                string bodyAsText = Encoding.UTF8.GetString(buffer);
                request.Body.Seek(0, SeekOrigin.Begin);
                return bodyAsText;
            }
            return default;
        }
        
        public static async Task<IEnumerable<string>> ReadFormFileAsArray(IFormFile file)
            {
                if (file == null || file.Length == 0)
                    return null;

                ICollection<string> result = new List<string>();
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        result.Add(await reader.ReadLineAsync());
                }
                return result;
            }
    }
}
