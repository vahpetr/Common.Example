using System.Web.Http;
using OlegTask.Web;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(UnityWebApiActivator), "Start")]

namespace OlegTask.Web
{
    public static class UnityWebApiActivator
    {
        public static void Start()
        {
            var container = UnityConfig.container;
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}