﻿using CRUDWebApiWithSwagger.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace WebApiWithSwagger
{
    public class Startup
    {
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddMvc().SetCompatibilityVersion( CompatibilityVersion.Version_2_1 );
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ProductContext>(options => options.UseSqlServer(connection));

            // Register Swagger
            services.AddSwaggerGen( c =>
            {
                c.SwaggerDoc( "v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Sample API", Version = "version 1" } );
            } );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IHostingEnvironment env )
        {
            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();

                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI( c =>
                {
                    c.SwaggerEndpoint( "/swagger/v1/swagger.json", "My API V1" );
                } );
            }

            app.UseMvc();
        }
    }
}