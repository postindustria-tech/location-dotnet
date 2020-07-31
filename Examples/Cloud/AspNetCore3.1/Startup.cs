using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FiftyOne.GeoLocation.Cloud.FlowElements;
using FiftyOne.Pipeline.CloudRequestEngine.FlowElements;
using FiftyOne.Pipeline.Core.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

/// @example AspNetCore3.1/Startup.cs
/// @include{doc} example-web-integration-location.txt
/// 
/// The source code for this example is available in full on [GitHub](https://github.com/51Degrees/location-dotnet/blob/master/Examples/Cloud/AspNetCore3.1). 
/// 
/// @include{doc} example-require-resourcekey.txt
/// 
/// Required NuGet Dependencies:
/// - [Microsoft.AspNetCore.App](https://www.nuget.org/packages/Microsoft.AspNetCore.App/)
/// - [FiftyOne.GeoLocation](https://www.nuget.org/packages/FiftyOne.GeoLocation/)
/// - [FiftyOne.Pipeline.Web](https://www.nuget.org/packages/FiftyOne.Pipeline.Web/)
/// 
/// ## Configuration
/// @include appsettings.json
/// 
/// ## Controller
/// @include Controllers/HomeController.cs
/// 
/// ## View
/// @include Views/Home/Index.cshtml
/// 
/// ## Startup

namespace AspNetCore31
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
            // This section is not really necessary. We're just checking
            // if the resource key has been set to a new value.
            var pipelineConfig = new PipelineOptions();
            Configuration.Bind("PipelineOptions", pipelineConfig);
            var cloudConfig = pipelineConfig.Elements.Where(e =>
                e.BuilderName.Contains(nameof(CloudRequestEngine),
                    StringComparison.OrdinalIgnoreCase));
            if (cloudConfig.Count() > 0)
            {
                if (cloudConfig.Any(c => c.BuildParameters
                         .TryGetValue("ResourceKey", out var resourceKey) == true &&
                     resourceKey.ToString().StartsWith("!!")))
                {
                    throw new Exception("You need to create a resource key at " +
                        "https://configure.51degrees.com and paste it into the " +
                        "appsettings.json file in this example.");
                }
            }
            services.AddControllersWithViews();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc();

            services.AddSingleton<GeoLocationCloudEngineBuilder>();
            services.AddSingleton<CloudRequestEngineBuilder>();
            services.AddSingleton<HttpClient>();
            services.AddFiftyOne(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseFiftyOne();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
