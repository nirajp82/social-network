using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Infrastructure
{
    public class AuthOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            bool hasAllowAnonymousAttr = context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any() ||
                                    context.MethodInfo.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any();
            if (hasAllowAnonymousAttr)
                return;

            operation.Responses.TryAdd(StatusCodes.Status401Unauthorized.ToString(), 
                new OpenApiResponse { Description = "Unauthorized" });

            operation.Responses.TryAdd(StatusCodes.Status403Forbidden.ToString(), 
                new OpenApiResponse { Description = "Forbidden" });

            var jwtBearerScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = JwtBearerDefaults.AuthenticationScheme }
            };


            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                     [ jwtBearerScheme ] = new string[]{ }
                }
            };
        }
    }
}
