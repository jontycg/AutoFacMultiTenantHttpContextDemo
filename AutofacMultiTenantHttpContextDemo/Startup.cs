using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Multitenant;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AutofacMultiTenantHttpContextDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddAutofacMultitenantRequestServices();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<DefaultGreeter>().As<IGreeter>();
        }
        
        public static MultitenantContainer ConfigureMultitenantContainer(IContainer container)
        {
            // If the strategy is changed to RngTenantIdentificationStrategy it will work (but tenants will be assigned
            // randomly)
            var strategy = new HeaderTenantIdentificationStrategy();
            var mtc = new MultitenantContainer(strategy, container);
            mtc.ConfigureTenant("one", cb => cb.RegisterType<NumberOneGreeter>().As<IGreeter>());
            mtc.ConfigureTenant("two", cb => cb.RegisterType<NumberTwoGreeter>().As<IGreeter>());
            return mtc;
        }
    }
}