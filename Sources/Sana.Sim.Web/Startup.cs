using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sana.Sim.Business;
using Sana.Sim.Mvc.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Sana.Sim.Mvc.Authentication;
using Sana.Sim.Mvc.Helpers;
using Sana.Sim.Mvc.ServiceFilters;
using Sana.Sim.Mvc;
using AutoMapper;
using Sana.Sim.Business.Components.Automapper;
using Sana.Sim.Mvc.Extensions;
using Sana.Sim.Business.Calculation;

namespace Sana.Sim.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSession();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<ExecutionContext>();
            services.AddScoped<ExecutionContextKeyHelper>();
            services.AddScoped<ExecutionContextRequiredFilter>();

            services.AddSingleton<AutomapperInitializersContainer>();

            services.AddEFRepositories(Configuration.GetConnectionString("DefaultConnection"));

            services.AddSingleton<ProgressCalculator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseSession();

            app.UseIoC();
                        
            app.UseMiddleware<ExecutionContextInitializerMiddleware>();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = AdminAuthenticationManager.AdminAuthenticationScheme,
                LoginPath = new PathString("/Admin/Login/"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });
            
            app.UseEFRepositories();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
            IoC.Resolve<AutomapperInitializersContainer>().Register(AutoMapperInitializer.Initialize);

            Mapper.Initialize(IoC.Resolve<AutomapperInitializersContainer>().Build());
        }
    }
}
