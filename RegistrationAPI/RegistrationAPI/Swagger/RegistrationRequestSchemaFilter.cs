using Microsoft.OpenApi.Any;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using RegistrationAPI.Models;

namespace RegistrationAPI.Swagger
{
    public class RegistrationRequestSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(RegistrationRequest) && schema?.Properties != null)
            {
                if (schema.Properties.ContainsKey("password"))
                {
                    schema.Properties["password"].Example = new OpenApiString("string");
                }

                if (schema.Properties.ContainsKey("confirmPassword"))
                {
                    schema.Properties["confirmPassword"].Example = new OpenApiString("string");
                }

                if (schema.Properties.ContainsKey("age"))
                {
                    schema.Properties["age"].Example = new OpenApiInteger(30);
                }
            }
        }
    }
}
