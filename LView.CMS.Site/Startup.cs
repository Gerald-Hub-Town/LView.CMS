using Alexinea.Autofac.Extensions.DependencyInjection;
using Autofac;
using Autofac.Core;
using AutoMapper;
using FluentValidation.AspNetCore;
using LView.CMS.Core.Options;
using LView.CMS.IRepository;
using LView.CMS.IServices;
using LView.CMS.Repository;
using LView.CMS.Services;
using LView.CMS.Site.Filter;
using LView.CMS.Site.Validation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using System;
using System.Linq;
using System.Reflection;

namespace LView.CMS.Site
{
    public class Startup
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            env.ConfigureNLog("Nlog.config");
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<DbOption>("LViewCMS", Configuration.GetSection("DbOpion"));
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Index";
                    options.LogoutPath = "/Account/Logout";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
                });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
            });
            services.AddAntiforgery(options =>
            {
                options.FormFieldName = "AntiforgeryKey_Gerald";
                options.HeaderName = "X-CSRF-TOKEN-Gerald";
                options.SuppressXFrameOptionsHeader = false;
            });
            services.AddMvc(option =>
            {
                option.Filters.Add(new GlobalExceptionFilter());
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddControllersAsServices()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<ManagerRoleValidation>();
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });
            services.AddAutoMapper();
            //services.AddSingleton<IManagerRoleRepository, ManagerRoleRepository>();
            //services.AddSingleton<IManagerRepository, ManagerRepository>();
            //services.AddSingleton<IManagerLogRepository, ManagerLogRepository>();
            //services.AddSingleton<IManagerRoleService, ManagerRoleService>();
            //services.AddSingleton<IManagerService, ManagerService>();
            var builder = new ContainerBuilder();
            builder.Populate(services);
            //Assembly RepositoryAss = Assembly.Load("ManagerRoleRepository");
            //Assembly managerRoleAss = Assembly.Load("ManagerRoleRepository");
            //builder.RegisterAssemblyTypes(RepositoryAss).Where(x => typeof(ManagerRoleRepository).IsAssignableFrom(x) && x.Name.EndsWith("dll"));
            //builder.RegisterAssemblyTypes(managerRoleAss).Where(x => typeof(ManagerRoleRepository).IsAssignableFrom(x) && x.Name.EndsWith("dll"));

            builder.RegisterAssemblyTypes(typeof(ManagerRoleRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(ManagerRoleService).Assembly)
                .Where(t => t.Name.Contains("Service"))
                .AsImplementedInterfaces();

            return new AutofacServiceProvider(builder.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                //app.UseHsts();
            }

            //try
            //{
            //    var jobInfoAppService = app.ApplicationServices.GetRequiredService<ITaskInfoService>();
            //    var scheduleCenter = app.ApplicationServices.GetRequiredService<>();
            //}
            //catch (Exception ex) { logger.Error(ex, nameof(Startup)); }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();
            loggerFactory.AddNLog();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
