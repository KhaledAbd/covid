using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Coid.API.Data;
using Coid.API.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;

namespace Coid.API
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
            services.AddDbContext<ContextData>(option=>{
                option.UseSqlite(Configuration.GetConnectionString("Covid"));});
            services.AddTransient<IDataRepository, DataRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddAutoMapper();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                Options => {
                    Options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters{
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(Configuration.GetSection("Appsettings:Token").Value)
                        ),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                }
            );
            //services.AddTransient<seed.CoronaSeed>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {

                app.UseExceptionHandler(Builder=>{
                    Builder.Run(async context => {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if(error != null){
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }
            //seed.SetUpTimer(new TimeSpan(1,00,00));
            //seed.ReedFromServer();
            app.UseCors(Options=> Options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
