namespace Hypomos.Api
{
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using Microsoft.OpenApi.Any;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public sealed class EnumSchemaFilter : ISchemaFilter
    {
        /// <inheritdoc />
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Enum.Clear();
                var enumMembers = context.Type.GetMembers(BindingFlags.Public | BindingFlags.Static);
                foreach (var enumMember in enumMembers.Where(m => m.MemberType == MemberTypes.Field))
                {
                    schema.Enum.Add(new OpenApiString(enumMember.GetCustomAttribute<EnumMemberAttribute>()?.Value));
                }
            }
        }
    }
}