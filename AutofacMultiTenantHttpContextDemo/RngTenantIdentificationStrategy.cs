using System;
using Autofac.Multitenant;

namespace AutofacMultiTenantHttpContextDemo
{
    public class RngTenantIdentificationStrategy : ITenantIdentificationStrategy
    {
        
        public bool TryIdentifyTenant(out object tenantId)
        {
            var rng = new Random();
            var num = rng.Next(1, 3);
            tenantId = num == 2 ? "two" : "one";
            return true;
        }
    }
}