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
using SBA.Business.Concrete;
using SBA.Business.ExternalServices;
using SBA.Business.ExternalServices.Abstract;
using SBA.Business.FunctionalServices.Abstract;
using SBA.Business.FunctionalServices.Concrete;
using SBA.DataAccess.Abstract;
using SBA.DataAccess.Concrete.EntityFramework;
using SBA.DataAccess.Concrete.MongoDB;
using SBA.ExternalDataAccess.Abstract;
using SBA.ExternalDataAccess.Concrete;

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

            builder.RegisterType<ComparisonStatisticsHolderManager>().As<IComparisonStatisticsHolderService>();
            builder.RegisterType<EfComparisonStatisticsHolderDal>().As<IComparisonStatisticsHolderDal>();

            builder.RegisterType<AverageStatisticsHolderManager>().As<IAverageStatisticsHolderService>();
            builder.RegisterType<EfAverageStatisticsHolderDal>().As<IAverageStatisticsHolderDal>();

            builder.RegisterType<TeamPerformanceStatisticsHolderManager>().As<ITeamPerformanceStatisticsHolderService>();
            builder.RegisterType<EfTeamPerformanceStatisticsHolderDal>().As<ITeamPerformanceStatisticsHolderDal>();

            builder.RegisterType<MatchIdentifierManager>().As<IMatchIdentifierService>();
            builder.RegisterType<EfMatchIdentifierDal>().As<IMatchIdentifierDal>();

            builder.RegisterType<StatisticInfoHolderManager>().As<IStatisticInfoHolderService>();
            builder.RegisterType<EfStatisticInfoHolderDal>().As<IStatisticInfoHolderDal>();

            builder.RegisterType<AiDataHolderManager>().As<IAiDataHolderService>();
            builder.RegisterType<EfAiDataHolderDal>().As<IAiDataHolderDal>();

            builder.RegisterType<ForecastManager>().As<IForecastService>();
            builder.RegisterType<EfForecastDal>().As<IForecastDal>();

            builder.RegisterType<LeagueStatisticsHolderManager>().As<ILeagueStatisticsHolderService>();
            builder.RegisterType<EfLeagueStatisticsHolderDal>().As<ILeagueStatisticsHolderDal>();

            builder.RegisterType<DataMaintenanceManager>().As<IDataMaintenanceService>();

            builder.RegisterType<ConfigHelper>().As<IConfigHelper>();

            builder.RegisterType<TelegramMessagingManager>().As<ISocialBotMessagingService>();
            builder.RegisterType<GoogleTranslationService>().As<ITranslationService>();

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
