using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module3
{
    public class ModuleExtensions
    {
        public IServiceCollection AddModule(IServiceCollection services)
        {
            services.AddControllers().ConfigureApplicationPartManager(manager =>
                manager.ApplicationParts.Add(new AssemblyPart(typeof(ModuleExtensions).Assembly)));

            return services;
        }
    }
}
