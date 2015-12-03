using OlegTask.Web;
using WebActivatorEx;

[assembly: ApplicationShutdownMethod(typeof(UnityDeactivator), "Shutdown")]

namespace OlegTask.Web
{
    public static class UnityDeactivator
    {
        public static void Shutdown()
        {
            var container = UnityConfig.container;
            container.Dispose();
        }
    }
}