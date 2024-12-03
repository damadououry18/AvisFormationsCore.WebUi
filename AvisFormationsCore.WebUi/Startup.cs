using AvisFormationsCore.WebUi.Data;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvisFormationsCore.WebUi
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddDbContext<MonDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddTransient<IFormationRepository, FormationRepository>();
            services.AddTransient<IAvisRepository, AvisRepository>();
            services.AddTransient<IContactRepository, ContactRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting(); //Important !
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>//UseMvc pour 2.2
            {
                endpoints.MapControllerRoute(
                    name: "LaisserUnAvis",
                    pattern: "laissez-un-avis/{nomSeo}",
                    defaults: new { controller = "Avis", action = "LaisserUnAvis" }
                    ); ;
                endpoints.MapControllerRoute(//MapRoute pour 2.2
                    name: "DetailFormation",
                    pattern: "formations/{nomSeo}",//template pour 2.2
                    defaults: new { controller = "Formation", action = "DetailsFormation" }
                    );
                endpoints.MapControllerRoute(
                    name: "Contact",
                    pattern: "contactez-nous",
                    defaults: new { controller = "Contact", action = "Index" }
                    );
                endpoints.MapControllerRoute(
                    name: "DetailFormation",
                    pattern: "formations/{idFormation}",
                    defaults: new { controller = "Formation", action = "DetailsFormation" }
                    );

                endpoints.MapControllerRoute(
                    name: "ToutesLesFormations",
                    pattern: "toutes-les-formations",
                    defaults: new {controller="Formation" ,action="ToutesLesFormations" }
                    );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
