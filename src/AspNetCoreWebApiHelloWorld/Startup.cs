using System.IO;
using AspNetCoreWebApiHelloWorld.Repositiories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace AspNetCoreWebApiHelloWorld
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


        private string GetSwaggerXMLPath()
        {
            var app = PlatformServices.Default.Application;
            return Path.Combine(app.ApplicationBasePath, "AspNetCoreWebApiHelloWorld.xml");
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {



            // Swagger services.
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My First Aspnet Core WebAPI - Swagger Documentation",
                    Version = "v1",
                    Description = "Alper`s Todo API",
                    Contact = new Contact { Name = "Alper Ebicoglu", Email = "alperonline@hotmail.com", Url = "https://github.com/alperonline" } });

                c.IncludeXmlComments(GetSwaggerXMLPath());
                c.DescribeAllEnumsAsStrings();
            });




            //services.ConfigureSwaggerGen(options =>
            //{
            //    options.SingleApiVersion(new Info
            //    {
            //        Version = "v1",
            //        Title = "Test API",
            //        Description = "Jesse's Test API",
            //        TermsOfService = "None",
            //        Contact = new Contact { Name = "JESSE.NET", Email = "info@test.com", Url = "www.JesseDotNet.com" }
            //    });

            //    options.IncludeXmlComments(GetSwaggerXMLPath());
            //    options.DescribeAllEnumsAsStrings();

            //});





            // Add framework services.
            services.AddMvc();

            services.AddSingleton<TodoContext, TodoContext>();
            services.AddSingleton<ITodoRepository, TodoRepository>();

            services.AddCors();





        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseMvc();




            app.UseSwagger();
            app.UseSwaggerUi(c =>
                       {
                           c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                       });



        }
    }
}
