﻿using Dummy.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Dummy
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Web API", Description = "Testing Web API" });

            }

            );
            var connection = @"Server=dev.retrotest.co.za;Database=stdbank;User Id=group5;Password=SVgY!y_FUU7Sk_5c";

            services.AddDbContext<DummyContext>
                 (options => options.UseSqlServer(connection));

           /* services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = AuthenticationOptions.DefaultScheme;
                options.DefaultChallengeScheme = AuthenticationOptions.DefaultScheme;
            }).AddBasicTokenAuthentication();
            */
            services.AddCors();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Shows UseCors with CorsPolicyBuilder.
            //app.UseCors(builder =>
              // builder.WithOrigins("http://localhost:8081"));
            app.UseCors(o => o.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

            app.UseHttpsRedirection();
            if (Configuration["EnableCORS"] == "True")
            {
                
            }
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(x =>

            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Testing Dummy DB");

            }


            );
        }
    }
}
