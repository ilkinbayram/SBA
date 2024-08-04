using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.Helpers;
using Core.Utilities.Helpers.Abstracts;
using Core.Utilities.Interceptors;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SBA.Business.Abstract;
using SBA.Business.Concrete;
using SBA.Business.ExternalServices;
using SBA.Business.ExternalServices.Abstract;
using SBA.Business.FunctionalServices.Abstract;
using SBA.Business.FunctionalServices.Concrete;
using SBA.DataAccess.Abstract;
using SBA.DataAccess.Concrete.EntityFramework;
using SBA.ExternalDataAccess.Abstract;
using SBA.ExternalDataAccess.Concrete;
using SBA.ExternalDataAccess.Concrete.EntityFramework.Contexts;

namespace SBA.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FilterResultManager>().As<IFilterResultService>();
            builder.RegisterType<EfFilterResultDal>().As<IFilterResultDal>().InstancePerLifetimeScope();

            builder.RegisterType<MatchBetManager>().As<IMatchBetService>();
            builder.RegisterType<EfMatchBetDal>().As<IMatchBetDal>().InstancePerLifetimeScope();

            builder.RegisterType<ComparisonStatisticsHolderManager>().As<IComparisonStatisticsHolderService>();
            builder.RegisterType<EfComparisonStatisticsHolderDal>().As<IComparisonStatisticsHolderDal>().InstancePerLifetimeScope();

            builder.RegisterType<AverageStatisticsHolderManager>().As<IAverageStatisticsHolderService>();
            builder.RegisterType<EfAverageStatisticsHolderDal>().As<IAverageStatisticsHolderDal>().InstancePerLifetimeScope();

            builder.RegisterType<TeamPerformanceStatisticsHolderManager>().As<ITeamPerformanceStatisticsHolderService>();

            builder.RegisterType<EfPerformanceOverallDal>().As<IPerformanceOverallDal>().InstancePerLifetimeScope();

            builder.RegisterType<EfTeamPerformanceStatisticsHolderDal>().As<ITeamPerformanceStatisticsHolderDal>().InstancePerLifetimeScope();

            builder.RegisterType<MatchIdentifierManager>().As<IMatchIdentifierService>();
            builder.RegisterType<EfMatchIdentifierDal>().As<IMatchIdentifierDal>().InstancePerLifetimeScope();

            builder.RegisterType<StatisticInfoHolderManager>().As<IStatisticInfoHolderService>();
            builder.RegisterType<EfStatisticInfoHolderDal>().As<IStatisticInfoHolderDal>().InstancePerLifetimeScope();

            builder.RegisterType<AiDataHolderManager>().As<IAiDataHolderService>();
            builder.RegisterType<EfAiDataHolderDal>().As<IAiDataHolderDal>().InstancePerLifetimeScope();

            builder.RegisterType<ForecastManager>().As<IForecastService>();
            builder.RegisterType<EfForecastDal>().As<IForecastDal>().InstancePerLifetimeScope();

            builder.RegisterType<LogManager>().As<ILogService>();
            builder.RegisterType<EfLogDal>().As<ILogDal>().InstancePerLifetimeScope();

            builder.RegisterType<ExtLogManager>().As<IExtLogService>();
            builder.RegisterType<EfExtLogDal>().As<IExtLogDal>().InstancePerLifetimeScope();

            builder.RegisterType<LeagueStatisticsHolderManager>().As<ILeagueStatisticsHolderService>();
            builder.RegisterType<EfLeagueStatisticsHolderDal>().As<ILeagueStatisticsHolderDal>().InstancePerLifetimeScope();

            builder.RegisterType<BetSystemManager>().As<IBetSystemService>();
            builder.RegisterType<EfBetSystemDal>().As<IBetSystemDal>().InstancePerLifetimeScope();

            builder.RegisterType<StepManager>().As<IStepService>();
            builder.RegisterType<EfStepDal>().As<IStepDal>().InstancePerLifetimeScope();

            builder.RegisterType<SavedStepManager>().As<ISavedStepService>();
            builder.RegisterType<EfSavedStepDal>().As<ISavedStepDal>().InstancePerLifetimeScope();

            builder.RegisterType<BundleManager>().As<IBundleService>();
            builder.RegisterType<EfBundleDal>().As<IBundleDal>().InstancePerLifetimeScope();

            builder.RegisterType<ComboBetManager>().As<IComboBetService>();
            builder.RegisterType<EfComboBetDal>().As<IComboBetDal>().InstancePerLifetimeScope();

            builder.RegisterType<PredictionManager>().As<IPredictionService>();
            builder.RegisterType<EfPredictionDal>().As<IPredictionDal>().InstancePerLifetimeScope();

            builder.RegisterType<DataMaintenanceManager>().As<IDataMaintenanceService>();

            builder.RegisterType<ConfigHelper>().As<IConfigHelper>();

            builder.RegisterType<TelegramMessagingManager>().As<ISocialBotMessagingService>();
            builder.RegisterType<GoogleTranslationService>().As<ITranslationService>();

            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>();
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();
            builder.RegisterType<ClientSideStorageHelper>().As<ISessionStorageHelper>();

            builder.Register(context => {
                var configuration = context.Resolve<IConfiguration>();
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("LocalDB"));
                return new ApplicationDbContext(optionsBuilder.Options);
            }).InstancePerLifetimeScope();

            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var optionsBuilder = new DbContextOptionsBuilder<ExternalAppDbContext>();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("SbaPromoDB"));
                return new ExternalAppDbContext(optionsBuilder.Options);
            }).InstancePerLifetimeScope();
            
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
