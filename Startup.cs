using AspNetCoreRequesLifeCycle.Contracts;
using AspNetCoreRequesLifeCycle.Middlewares;
using AspNetCoreRequesLifeCycle.ModelBinder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace AspNetCoreRequesLifeCycle
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
            services.AddControllers(op=>
            {
               op.OutputFormatters.Clear();
                op.ModelBinderProviders.Insert(0,new CSVModelBinderProvider());
            })
                .AddNewtonsoftJson((options) =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            
            


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<UserFrendlyResultMiddleware>();

            app.UseRouting();

            app.UseMiddleware<ModuleStateControlMiddleware>();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
