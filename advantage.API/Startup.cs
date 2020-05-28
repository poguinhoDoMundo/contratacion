using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using advantage.API.Models;

namespace advantage.API
{
    public class Startup
    {
        string _connectionString=null;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
             _connectionString = Configuration["secretConnectionString"];    

            services.AddCors( opt =>{ opt.AddPolicy("CorsPolicy",
                                        c=> c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod() );                           
                                    });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2); 
            /*
            services.AddEntityFrameworkNpgsql().
                    AddDbContext<providersBankContext>( opt=>opt.UseNpgsql(_connectionString) );
            */    
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("CorsPolicy");
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseMvc(  routes => routes.MapRoute("default","api/{controller}/{action}/{id?}") );
            
        }
    }
}