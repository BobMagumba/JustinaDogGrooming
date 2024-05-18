using JustinaSystem.BLL;
using JustinaSystem.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JustinaSystem
{
    public static class BackEndExtensions
    {
        public static void AddBackendDependencies(this IServiceCollection services,
           Action<DbContextOptionsBuilder> options)
        {
            //we will register all the services that will
            //  be used by the system (context setup)
            //  and will be provided by the system (BLL services)

            //register the context service
            //options contents the connection string information
            services.AddDbContext<JustinaContext>(options);

            //register EACH service class (BLL classes)
            //each service class will have an AddTransient<T>() method

            //use the AddTransient<T>() method where T is your BLL class name
            //AddTrainsient is called a factory
            //I read my lamda symbol => as "do the following ...."
            services.AddTransient<JustinaServices>((serviceProvider) =>
            {
                //get the Context class that was registed above in AddDbContext
                var context = serviceProvider.GetService<JustinaContext>();

                //create an instance of the service class (BuildVersionServices)
                //      supplying the context reference to the serivce class
                return new JustinaServices(context);
            });

            services.AddTransient<DogServices>((serviceProvider) =>
            {
                var memoryCache = serviceProvider.GetService<IMemoryCache>();
                var context = serviceProvider.GetService<JustinaContext>();

                return new DogServices(memoryCache, context);
            });


            services.AddTransient<GrommerServices>((serviceProvider) =>
            {
                //get the Context class that was registed above in AddDbContext
                var context = serviceProvider.GetService<JustinaContext>();

                //create an instance of the service class (BuildVersionServices)
                //      supplying the context reference to the serivce class
                return new GrommerServices(context);
            });

        }
    }
 }
