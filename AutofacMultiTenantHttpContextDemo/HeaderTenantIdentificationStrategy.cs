using Autofac.Multitenant;
using Microsoft.AspNetCore.Http;

namespace AutofacMultiTenantHttpContextDemo
{
    public class HeaderTenantIdentificationStrategy : ITenantIdentificationStrategy
    {
        
        public bool TryIdentifyTenant(out object tenantId)
        {
            tenantId = null;

            var httpContext = new HttpContextAccessor().HttpContext;
            if (httpContext != null)
            {
                tenantId = httpContext.Request.Headers["tenant"];
            }

            return tenantId != null;
        }
    }
}