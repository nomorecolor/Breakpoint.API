using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Breakpoint.API.Filters
{
	public class SwaggerAuthFilter : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			if (context.MethodInfo.CustomAttributes.Any(x => x.AttributeType == typeof(AuthorizeAttribute))
				|| (context.MethodInfo.DeclaringType?.CustomAttributes.Any(x => x.AttributeType == typeof(AuthorizeAttribute)) ?? false))
			{
#if DEBUG
				if (operation.Parameters == null)
				{
					operation.Parameters = new List<OpenApiParameter>();
				}

				operation.Parameters.Add(new OpenApiParameter
				{
					Name = "X-Swagger",
					In = ParameterLocation.Header,
					Description = "This header is for bypassing the authorization",
					Required = true,
					Schema = new OpenApiSchema
					{
						Type = "string",
						Default = new OpenApiString("Bypass Authorization")
					}
				});
#endif

#if RELEASE
				if (operation.Security == null)
				{
					operation.Security = new List<OpenApiSecurityRequirement>();
				}

				operation.Security.Add(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme {
							Reference = new OpenApiReference {
								Type = ReferenceType.SecurityScheme,
								Id = "bearer"
							}
						}, new string[] { }
					}
				});
#endif
			}
		}
	}
}
