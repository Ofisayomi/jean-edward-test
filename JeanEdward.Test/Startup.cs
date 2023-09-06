using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jean_edwards.Database;
using jean_edwards.Interfaces;
using jean_edwards.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JeanEdward.Test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var config = BuildConfiguration();
            services.AddHttpClient<IMovieServices, MovieServices>(x=>{
                x.BaseAddress = new Uri(config["BaseAddress"]);
            });
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("DBInMemoryTest");
                // options.UseInternalServiceProvider(serviceProvider);
            });

            
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false);

            return builder.Build();
        }
    }
}