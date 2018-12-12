using AutoMapper;
using Embed.Core.Abstract;
using Embed.Persistance.Repositories;
using Embed.Persistance.Repositories.Auth;
using Embed.Web.Mapping;
using Embed.Web.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(Embed.Web.Startup))]
namespace Embed.Web
{
    /// <summary>
    /// The OWIN startup class to specify componenents for the application pipeline.
    /// Ref: https://docs.microsoft.com/en-us/aspnet/aspnet/overview/owin-and-katana/owin-startup-class-detection
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The main method that hold all the configuration details upon application start up.
        /// </summary>
        /// <param name="app">The app builder instance.</param>
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };

            ConfigureOAuth(app);

            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
        }

        /// <summary>
        /// Creates the kernel.
        /// </summary>
        /// <returns>the newly created kernel.</returns>
        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            kernel.Bind<IProductRepository>().To<ProductRepository>();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IAuthRepository>().To<AuthRepository>();
            return kernel;
        }

        /// <summary>
        /// To configure all related OAuth settings.
        /// </summary>
        /// <param name="app">The app builder instance.</param>
        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}