using Autofac.Multitenant;
using Microsoft.AspNetCore.Http;

namespace AutofacMultiTenantHttpContextDemo
{
    public class HeaderTenantIdentificationStrategy : ITenantIdentificationStrategy
    {

        public HeaderTenantIdentificationStrategy(IHttpContextAccessor accessor)
        {
            Accessor = accessor;
        }
        
        public IHttpContextAccessor Accessor { get; private set; }
        public bool TryIdentifyTenant(out object tenantId)
        {
            tenantId = null;

            var httpContext = Accessor.HttpContext;
            if (httpContext != null)
            {
                tenantId = httpContext.Request.Headers["tenant"];
            }

            return tenantId != null;
        }
    }
}