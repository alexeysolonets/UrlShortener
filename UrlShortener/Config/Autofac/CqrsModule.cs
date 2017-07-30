using Autofac;
using System.Linq;
using UrlShortener.Bll.Infrastructure.Cqrs;

namespace UrlShortener.Config.Autofac
{
    public class CqrsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = System.Reflection.Assembly.GetEntryAssembly();

            builder.RegisterAssemblyTypes(assembly)
                .Where(type => type.IsClosedTypeOf(typeof(IQueryHandler<,>)))
                .AsClosedTypesOf(typeof(IQueryHandler<,>))
                .PropertiesAutowired();

            builder.RegisterAssemblyTypes(assembly)
                .Where(type => type.IsClosedTypeOf(typeof(ICommandHandler<>)))
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .PropertiesAutowired();

            builder.RegisterAssemblyTypes(assembly)
                .Where(type => type.IsClosedTypeOf(typeof(ICommandHandler<,>)))
                .AsClosedTypesOf(typeof(ICommandHandler<,>))
                .PropertiesAutowired();

            // Активатор (для процессоров запросов/команд; чтобы у них не было зависимости от Autofac)
            builder.Register<GetInstance>(c => c.Resolve<IComponentContext>().Resolve);

            // Процессор запросов
            builder.RegisterType<QueryProcessor>()
                .As<IQueryProcessor>();

            // Процессор команд
            builder.RegisterType<CommandProcessor>()
                .As<ICommandProcessor>();
        }
    }
}