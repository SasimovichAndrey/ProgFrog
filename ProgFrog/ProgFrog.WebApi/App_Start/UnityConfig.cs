using System.Web.Http;

namespace ProgFrog.WebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            ProgFrog.IoC.DependencyReolver.Configure();
            GlobalConfiguration.Configuration.DependencyResolver = ProgFrog.IoC.DependencyReolver.GetWebDependencyResolver();
        }
    }
}