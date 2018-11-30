using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace SuitSupply.Infrastructure.Bootstrapper.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSuitRouting(this IApplicationBuilder applicationBuilder, string routing = "", Action<IRouteBuilder> routeBuilderAction = null)
        {
            if (routeBuilderAction == null)
            {
                routeBuilderAction = builder =>
                {
                    if (string.IsNullOrEmpty(routing))
                        routing = "api/{controller}/{action}/{id?}";
                    builder.MapRoute("default", routing);
                };
            }

            return UseSuitRouting(applicationBuilder, routeBuilderAction);
        }
        public static IApplicationBuilder UseSuitRouting(this IApplicationBuilder applicationBuilder, Action<IRouteBuilder> routeBuilderAction) => applicationBuilder.UseMvc(routeBuilderAction);
        public static IApplicationBuilder UseCorsToAllowAll(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            return applicationBuilder;
        }
    }
}
