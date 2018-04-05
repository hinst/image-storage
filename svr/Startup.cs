using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using NLog;

namespace image_storage
{
    public class Startup
    {
        Logger Log = LogManager.GetCurrentClassLogger();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UserDbContext>(o => o.UseSqlite("Data Source=users.db"));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<UserDbContext>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o => o.LoginPath = Program.WebRoot + "/loginPage");
            services.AddMvc();
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            app.UseResponseCompression();
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            void useStatic(string dir)
            {
                app.UseStaticFiles(new StaticFileOptions {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(Program.AppDir, "../web/" + dir)),
                    RequestPath = Program.WebRoot + "/" + dir
                });
            }
            useStatic("js-bin");
            useStatic("js-3rd");
            useStatic("css-3rd");
            useStatic("css");
            useStatic("src-html");
            app.UseAuthentication();
            app.UseMvc();
            PrepareUsers(userManager, roleManager);
            Log.Info("Configured");
        }

        private void PrepareUsers(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) {
            CreateAdminRole(roleManager);
            CreateAdminUser(userManager);
        }

        private void CreateAdminRole(RoleManager<IdentityRole> roleManager) {
            var adminRole = new IdentityRole(Role.Admin.ToString());
            var result = roleManager.CreateAsync(adminRole).Result;
            Log.Info("Admin role created: " + result.Succeeded);
        }

        private void CreateAdminUser(UserManager<IdentityUser> userManager) {
            var admin = new IdentityUser();
            admin.UserName = "admin";
            var result = userManager.CreateAsync(admin, File.ReadAllText(Program.AppDir + "/adminPassword.txt")).Result;
            Log.Info(result.ToString());
            var roleResult = userManager.AddToRoleAsync(admin, Role.Admin.ToString()).Result;
            Log.Info("Admin user created successfully: " + result.Succeeded + ", " + roleResult.Succeeded);
        }
    }
}
