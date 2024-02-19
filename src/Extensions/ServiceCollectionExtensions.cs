using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SmartBreadcrumbs.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static void AddBreadcrumbs(this IServiceCollection services, Assembly[] assemblies)
        {
            AddBreadcrumbs(services, assemblies, new BreadcrumbOptions());
        }

        public static void AddBreadcrumbs(this IServiceCollection services, Assembly[] assemblies, Action<BreadcrumbOptions> optionsSetter)
        {
            var options = new BreadcrumbOptions();
            optionsSetter.Invoke(options);
            AddBreadcrumbs(services, assemblies, options);
        }

        private static void AddBreadcrumbs(IServiceCollection services, Assembly[] assemblies, BreadcrumbOptions options)
        {
            var bm = new BreadcrumbManager(options);
            bm.Initialize(assemblies);
            services.AddSingleton(bm);

            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }

    }
}
