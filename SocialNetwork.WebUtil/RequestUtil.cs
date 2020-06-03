using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
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
                var buffer = new byte[Convert.ToInt32(request.ContentLength)];
                await request.Body.ReadAsync(buffer, 0, buffer.Length);
                string bodyAsText = Encoding.UTF8.GetString(buffer);
                request.Body.Seek(0, SeekOrigin.Begin);
                return bodyAsText;

                //        // Leave the body open so the next middleware can read it.
                //        using (var reader = new StreamReader(request.Body,encoding: Encoding.UTF8, 
                //                    detectEncodingFromByteOrderMarks: false,
                //bufferSize: bufferSize,
                //leaveOpen: true))
                //            string body = await reader.ReadToEndAsync();
                //            // Reset the request body stream position so the next middleware can read it
                //            request.Body.Seek(0, SeekOrigin.Begin);

                //            return body;
                //        }
            }
            return string.Empty;
        }
    }
}
