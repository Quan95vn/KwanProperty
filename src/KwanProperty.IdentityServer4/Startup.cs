// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using System.Linq;
using KwanProperty.IdentityServer4.DbContexts;
using KwanProperty.IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using KwanProperty.IdentityServer4.Entities;
using IdentityServer4;

namespace KwanProperty.IdentityServer4
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // uncomment, if you want to add an MVC-based UI
            services.AddControllersWithViews();

            services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IUserService, UserService>();

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiResources(Config.ApiResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddTestUsers(TestUsers.Users);

            //builder.AddProfileService<UserProfileService>(); // lấy claim từ bảng UserClaims

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();

            var migrationsAssembly = typeof(Startup)
                .GetTypeInfo().Assembly.GetName().Name;

            //builder.AddConfigurationStore(options =>
            //{
            //    options.ConfigureDbContext = builder =>
            //        builder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
            //        options => options.MigrationsAssembly(migrationsAssembly));
            //});

            //builder.AddOperationalStore(options =>
            //{
            //    options.ConfigureDbContext = builder =>
            //        builder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
            //        options => options.MigrationsAssembly(migrationsAssembly));
            //});

            services.AddAuthentication().AddFacebook("Facebook", options =>
            {
                options.AppId = Configuration["Facebook:AppId"];
                options.AppSecret = Configuration["Facebook:AppSecret"]; ;
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
            });

        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //InitializeDatabase(app);

            // uncomment if you want to add MVC
            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();

            // uncomment, if you want to add MVC
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider
                    .GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var configurationDbContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                configurationDbContext.Database.Migrate();
                if (!configurationDbContext.Clients.Any())
                {
                    foreach (var client in Config.Clients)
                    {
                        configurationDbContext.Clients.Add(client.ToEntity());
                    }
                    configurationDbContext.SaveChanges();
                }

                if (!configurationDbContext.IdentityResources.Any())
                {
                    foreach (var resource in Config.IdentityResources)
                    {
                        configurationDbContext.IdentityResources.Add(resource.ToEntity());
                    }
                    configurationDbContext.SaveChanges();
                }

                if (!configurationDbContext.ApiResources.Any())
                {
                    foreach (var resource in Config.ApiResources)
                    {
                        configurationDbContext.ApiResources.Add(resource.ToEntity());
                    }
                    configurationDbContext.SaveChanges();
                }
                if (!configurationDbContext.ApiScopes.Any())
                {
                    foreach (var resource in Config.ApiScopes)
                    {
                        configurationDbContext.ApiScopes.Add(resource.ToEntity());
                    }
                    configurationDbContext.SaveChanges();
                }

                var identityDbContext = serviceScope.ServiceProvider.GetRequiredService<IdentityDbContext>();
                identityDbContext.Database.Migrate();

            }
        }
    }
}
