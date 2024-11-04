using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TrainerJournal.API.Swagger;

public class NullDefaultSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema.Type == "object" && schema.Properties != null)
        {
            foreach (var property in schema.Properties)
            {
                if (property.Value.Type != "array" && !property.Value.Required.Any() && property.Value.Nullable)
                {
                    property.Value.Default = new OpenApiNull();
                }
            }
        }
    }
}