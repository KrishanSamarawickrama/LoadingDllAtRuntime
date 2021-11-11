using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module1
{
    public class ModuleStartup
    {
        public void AddModule(WebApplication app)
        {
            app.MapWeatherForecastEndpoints();
        }
    }
}
