﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace image_storage
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
            services.AddDbContext<UserDbContext>(o => o.UseSqlite("Data Source=users.db"));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<UserDbContext>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o => o.LoginPath = Program.WebRoot + "/loginPage");
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<IdentityUser> userManager, UserDbContext userDb)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            app.UseStaticFiles(new StaticFileOptions {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Program.AppDir, "../web/js-bin")),
                RequestPath = Program.WebRoot + "/js-bin"
            });
            app.UseStaticFiles(new StaticFileOptions {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Program.AppDir, "../web/js-3rd")),
                RequestPath = Program.WebRoot + "/js-3rd"
            });
            app.UseStaticFiles(new StaticFileOptions {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Program.AppDir, "../web/src-html")),
                RequestPath = ""
            });
            app.UseAuthentication();
            app.UseMvc();
            CreateAdminUser(userManager);
        }

        private void CreateAdminUser(UserManager<IdentityUser> userManager) {
            var user = new IdentityUser();
            user.UserName = "admin";
            var result = userManager.CreateAsync(user, File.ReadAllText(Program.AppDir + "/adminPassword.txt")).Result;
            Console.WriteLine(result);
        }
    }
}
