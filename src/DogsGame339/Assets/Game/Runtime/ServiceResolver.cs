using System;
using Game339.Shared;
using Game339.Shared.DependencyInjection;
using Game339.Shared.DependencyInjection.Implementation;
using Game339.Shared.Diagnostics;
using Game339.Shared.Models;
using Game339.Shared.Services;
using Game339.Shared.Services.Implementation;

namespace Game.Runtime
{
    public static class ServiceResolver
    {
        public static T Resolve<T>() => Container.Value.Resolve<T>();

        private static readonly Lazy<IMiniContainer> Container = new (() =>
        {
            var container = new MiniContainer();

            var logger = new UnityGameLogger();
            container.RegisterSingletonInstance<IGameLog>(logger);

            var gameState = new GameState();
            gameState.GoodGuy.Name.Value = "Good Sandy";
            gameState.BadGuy.Name.Value = "Bad Sandy";
            gameState.Player.Name.Value = "Player";
            
            container.RegisterSingletonInstance(gameState);

            var damageService = new DamageService(logger);
            container.RegisterSingletonInstance<IDamageService>(damageService);
            
            var stringService = new StringService(logger);
            container.RegisterSingletonInstance<IStringService>(stringService);
            
            return container;
        });
    }
}
