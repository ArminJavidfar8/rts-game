using Managements.AI;
using Managements.Unit;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;
using Services.Abstraction.CoroutineSystem;
using Services.Abstraction.EventSystem;
using Services.Abstraction.PoolSystem;
using Services.Core.CoroutineSystem;
using Services.Core.EventSystem;
using Services.Core.PoolSystem;
using Services.Core.ResourceSystem;
using Services.Core.Score;
using Services.Core.Unit;
using System;

namespace Managements
{
    public class ServiceHolder
    {
        private static IServiceProvider _serviceProvider;
        public static IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceProvider == null)
                {
                    ServiceCollection serviceCollection = new ServiceCollection();
                    serviceCollection.AddSingleton<IUnitService, UnitService>();
                    serviceCollection.AddSingleton<IScoreService, ScoreService>();
                    serviceCollection.AddSingleton<IResourceService, ResourceService>();
                    serviceCollection.AddSingleton<IPoolService, PoolService>();
                    serviceCollection.AddSingleton<IEventService, EventService>();
                    serviceCollection.AddSingleton<ICoroutineService, CoroutineService>();
                    serviceCollection.AddSingleton<PlayerUnitManager>();
                    serviceCollection.AddSingleton<EnemyManager>();
                    _serviceProvider = serviceCollection.BuildServiceProvider();

                    _ = _serviceProvider.GetService<UnitService>();
                    _ = _serviceProvider.GetService<ScoreService>();
                    _ = _serviceProvider.GetService<ResourceService>();
                    _ = _serviceProvider.GetService<PoolService>();
                    _ = _serviceProvider.GetService<EventService>();
                    _ = _serviceProvider.GetService<CoroutineService>();
                    _ = _serviceProvider.GetService<PlayerUnitManager>();
                    _ = _serviceProvider.GetService<EnemyManager>();
                }
                return _serviceProvider;
            }
        }
    }
}