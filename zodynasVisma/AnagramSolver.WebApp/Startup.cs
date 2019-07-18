using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;
using AnagramSolver.EF.DatabaseFirst.Repositories;
using AnagramSolver.EF.DatabaseFirst.Entities;
using AnagramSolver.EF.CodeFirst;
using Microsoft.EntityFrameworkCore;
using AnagramSolver.EF.CodeFirst.Repositories;

namespace AnagramSolver.WebApp
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            
            services.AddDbContext<DictionaryContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("EfCfConnection"));
            });

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>(); // for cookies
            services.AddTransient<IAnagramSolver, BusinessLogic.AnagramSolver>();
            services.AddScoped<IWordRepository, EFCFWordRepository>();
            services.AddTransient<ICacheRepository, EFCFCacheRepository>();
            services.AddTransient<IUserLogRepository, EFCFUserLogRepository>();
            services.AddTransient<IUserInfoRepository, EFCFUserInfoRepository>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //services.AddSingleton<IWordRepository>( x= > new SQLWordRepository(Configuration["SqldatabaseString"]));
            //services.AddTransient<ICacheRepository>(x=> new AnagramCache(Configuration.GetConnectionString("SqldatabaseString")));
            //services.AddTransient<IUserLogRepository>(x=> new SQLUserLogRepository("SqldatabaseString"));
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();



            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/"); 
                    

            });
        }
    }
}
