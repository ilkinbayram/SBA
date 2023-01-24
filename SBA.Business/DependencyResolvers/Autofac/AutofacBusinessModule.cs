using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.Helpers;
using Core.Utilities.Helpers.Abstracts;
using Core.Utilities.Interceptors;
using Microsoft.AspNetCore.Http;
using SBA.Business.Abstract;
using SBA.Business.ExternalServices;
using SBA.Business.ExternalServices.Abstract;
using SBA.Business.FunctionalServices.Abstract;
using SBA.Business.FunctionalServices.Concrete;
using SBA.DataAccess.Abstract;
using SBA.DataAccess.Concrete.EntityFramework;
using SBA.DataAccess.Concrete.MongoDB;

namespace SBA.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FilterResultManager>().As<IFilterResultService>();
            builder.RegisterType<EfFilterResultDal>().As<IFilterResultDal>();

            builder.RegisterType<MatchBetManager>().As<IMatchBetService>();
            builder.RegisterType<EfMatchBetDal>().As<IMatchBetDal>();

            builder.RegisterType<DataMaintenanceManager>().As<IDataMaintenanceService>();

            builder.RegisterType<ConfigHelper>().As<IConfigHelper>();

            builder.RegisterType<TelegramMessagingManager>().As<ISocialBotMessagingService>();

            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>();
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();
            builder.RegisterType<ClientSideStorageHelper>().As<ISessionStorageHelper>();


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }
    }
}
