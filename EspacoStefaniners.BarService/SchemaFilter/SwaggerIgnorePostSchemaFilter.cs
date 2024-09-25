using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;

public class SwaggerIgnorePostSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema?.Properties == null)
        {
            return;
        }

        var ignorePostProperties = context.Type.GetProperties()
            .Where(prop => prop.GetCustomAttribute<SwaggerIgnorePostAttribute>() != null);

        foreach (var prop in ignorePostProperties)
        {
            var propertyName = schema.Properties.Keys
                .SingleOrDefault(key => key.ToLower() == prop.Name.ToLower());

            if (propertyName != null)
            {
                schema.Properties.Remove(propertyName);
            }
        }
    }
}