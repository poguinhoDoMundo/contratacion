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
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
        public void ConfigureServices( IServiceCollection services)
        {
             _connectionString = Configuration["secretConnectionString"];    

            services.AddCors( opt =>{ opt.AddPolicy("CorsPolicy",
                                        c=> c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod() );                           
                                    });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2); 

            services.Configure<FormOptions>(o => {
                                                        o.ValueLengthLimit = int.MaxValue;
                                                        o.MultipartBodyLengthLimit = int.MaxValue;
                                                        o.MemoryBufferThreshold = int.MaxValue;
                                                  });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                options =>
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = "yourdomain.com",
                     ValidAudience = "yourdomain.com",
                     IssuerSigningKey = new SymmetricSecurityKey(
                     Encoding.UTF8.GetBytes( Environment.GetEnvironmentVariable("secretKey") ) ) ,
                     ClockSkew = TimeSpan.Zero
                 }  );                                      
            
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

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc(  routes => routes.MapRoute("default","api/{controller}/{action}/{id?}") );
            
                app.UseStaticFiles();
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                    RequestPath = new PathString("/Resources")
                });
        }
    }
}