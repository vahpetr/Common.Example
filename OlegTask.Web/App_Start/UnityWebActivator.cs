using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity.Mvc;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using OlegTask.Web;
using WebActivatorEx;


[assembly: PreApplicationStartMethod(typeof(UnityWebActivator), "Start")]

namespace OlegTask.Web
{
    public static class UnityWebActivator
    {
        public static void Start()
        {
            var container = UnityConfig.container;

            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(container));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        }
    }
}