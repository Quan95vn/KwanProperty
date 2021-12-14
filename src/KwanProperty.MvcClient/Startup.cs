using IdentityModel;
using KwanProperty.MvcClient.HttpHandlers;
using KwanProperty.MvcClient.PostConfigurationOptions;
using KwanProperty.MvcClient.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace KwanProperty.MvcClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "PaidUserCanCallGetUserApi",
                    policyBuilder =>
                    {
                        policyBuilder.RequireAuthenticatedUser();
                        policyBuilder.RequireClaim("subscription_level", "PaidUser");
                        policyBuilder.RequireClaim("country", "vn");
                    });
            });

            services.AddHttpContextAccessor();

            services.AddTransient<BearerTokenHandler>();

            #region Init HttpClient 
            // create an HttpClient used for accessing the User Api
            services.AddHttpClient("KwanUserApiClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:44373/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            }).AddHttpMessageHandler<BearerTokenHandler>();

            // create an HttpClient without token used for accessing the User Api
            services.AddHttpClient("BasicUserApiClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:44373/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            });

            // create an HttpClient used for accessing the IdentityServer
            services.AddHttpClient("IdentityServerClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:44397/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            });

            // create an HttpClient used for EventCatalog service
            services.AddHttpClient<IEventCatalogService, EventCatalogService>(c =>
              c.BaseAddress = new Uri(Configuration["ApiConfigs:EventCatalog:Uri"]));

            #endregion

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.AccessDeniedPath = "/Authorization/AccessDenied";
            })
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Authority = "https://localhost:44397/";
                options.ClientId = "Mvc_Client";
                options.ResponseType = "code";

                // cần add các scope vào để truy cập tới claim, scope này lấy từ AllowedScope của client
                // đây là scope thuộc IdentityResource
                options.Scope.Add("address");
                options.Scope.Add("roles");
                options.Scope.Add("KwanPropertyUserApi");
                options.Scope.Add("country");
                options.Scope.Add("IdentityNumber");

                // đây là scope của ApiScope -> để xác định Audience của api service
                options.Scope.Add("KwanPropertyEventCatalog.FullAccess");
                options.Scope.Add("KwanPropertyGateway.FullAccess");

                // sample scope load động từ phía client -> OpenIdConnectOptionsPostConfigureOptions
                options.Scope.Add("subscription_level"); 
                // support refresh_token
                options.Scope.Add("offline_access"); 

                options.ClaimActions.DeleteClaim("sid");
                options.ClaimActions.DeleteClaim("idp");
                options.ClaimActions.DeleteClaim("s_hash");
                options.ClaimActions.DeleteClaim("auth_time");

                // access các claim của scope, cần phải map tay 
                options.ClaimActions.MapUniqueJsonKey("address", "address");

                options.ClaimActions.MapUniqueJsonKey("country", "country");
                options.ClaimActions.MapUniqueJsonKey("country1", "country1");
                options.ClaimActions.MapUniqueJsonKey("country3", "country3");

                options.ClaimActions.MapUniqueJsonKey("subscription_level", "subscription_level");
                options.ClaimActions.MapUniqueJsonKey("subscription_level1", "subscription_level1");

                options.ClaimActions.MapUniqueJsonKey("IdentityNumber_Old", "IdentityNumber_Old");
                options.ClaimActions.MapUniqueJsonKey("IdentityNumber_New", "IdentityNumber_New");

                options.ClaimActions.MapUniqueJsonKey("admin", "admin");
                options.ClaimActions.MapUniqueJsonKey("super_user", "super_user");
                options.ClaimActions.MapUniqueJsonKey("moderator", "moderator");
                options.ClaimActions.MapUniqueJsonKey("user", "user");



                options.SaveTokens = true;
                options.ClientSecret = "secret";
                options.GetClaimsFromUserInfoEndpoint = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = JwtClaimTypes.GivenName,
                    RoleClaimType = JwtClaimTypes.Role
                };
            });

            //services.AddSingleton<IPostConfigureOptions<OpenIdConnectOptions>,
            //  OpenIdConnectOptionsPostConfigureOptions>();
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
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
