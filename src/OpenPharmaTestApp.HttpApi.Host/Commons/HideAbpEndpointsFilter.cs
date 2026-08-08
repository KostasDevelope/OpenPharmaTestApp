using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace OpenPharmaTestApp.Commons
{
    public class HideAbpEndpointsFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // Укажите префиксы путей, которые нужно скрыть
            var prefixesToHide = new[]
            {
            "/api/abp/",
            "/api/account/",
            "/api/feature-management/",
            "/api/permission-management/",
            "/api/multi-tenancy/",
            "/api/identity/",
            "/api/setting-management/"
        };

            var pathsToRemove = swaggerDoc.Paths
                .Where(path => prefixesToHide.Any(prefix => path.Key.StartsWith(prefix, System.StringComparison.OrdinalIgnoreCase)))
                .ToList();

            foreach (var item in pathsToRemove)
            {
                swaggerDoc.Paths.Remove(item.Key);
            }
        }
    }
}
