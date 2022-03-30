using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
// using  WebApi.StudentsContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Model;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Api
{
    public class Startup
    {

        public Startup(IHostEnvironment env)
        {
             var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("app.settings.json")
                .AddJsonFile("app.settings.Development.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            // Configuration = configuration;
        }
        public IConfiguration Configuration { get; set;}
        // This method gets called by the runtime. Use this method to add services to the container.
        private readonly string _policyName = "CorsPolicy";
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<DatabaseContext>(options =>
          options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
// options.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=Synapse2022;Database=student_form;"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddCors(opt =>
           {
               opt.AddPolicy(name: _policyName, builder =>
               {
                   System.Diagnostics.Debug.WriteLine(builder);
                   builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
               });
           });
    

            services.AddControllers();

        }
// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // app.UseRouting();
            // app.UseEndpoints(x => x.MapControllers());

            app.UseHttpsRedirection();
            app.UseRouting();
            // app.UseMvc();
            app.UseCors(_policyName);
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllers();
            });

        }

    }
}

