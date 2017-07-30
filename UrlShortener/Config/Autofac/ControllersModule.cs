using System.Diagnostics;
using Autofac;
using UrlShortener.Controllers;

namespace UrlShortener.Config.Autofac
{
    public class ControllersModule : global::Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UrlsController>().PropertiesAutowired()
                   .OnActivated(e =>
                   {
                       Debug.WriteLine("Activated");
                   });
        }
    }
}