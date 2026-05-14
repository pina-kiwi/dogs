using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Game339.Shared.Infastructure.DataTypes;
using Game339.Shared.Infastructure.DependencyInjection;
using Game339.Shared.Infastructure.DependencyInjection.Implementation;
using Game339.Shared.Infastructure.Diagnostics;
using Game339.Shared.Models;
using Game339.Shared.Services;
using Game339.Shared.Services.Implementation;
using EnemyDogs = System.Collections.Generic.List<Game339.Shared.Models.EnemyDog>;
namespace System.Runtime.CompilerServices { internal static class IsExternalInit {} }
// ReSharper disable InconsistentNaming


namespace Game.Runtime.Infastructure
{
    public static class ServiceResolver
    {
        private static Dictionary<Type, object> _registry;

        private static bool Ratify<T>() => _registry.ContainsKey(typeof(T));
        private static T Retrieve<T>() => (T)_registry[typeof(T)];
        public static void Register<T>(T data) => _registry.Add(typeof(T), data);

        public static T Rig<T>(GridPosition position) where T : PlayerDog
        {
            (int maxBones, int initialBones) = Retrieve<(int maxBones, int initialBones)>();
            PlayerDog playerDog = new(maxBones, initialBones, position);
            Register(playerDog);
            return (T)playerDog;
        }

        public static T Resolve<T>()
        {
            if (typeof(ITuple).IsAssignableFrom(typeof(T))) throw new NullReferenceException();
            return Container.Value.Resolve<T>();
        }

        private static readonly Lazy<IMiniContainer> Container = new (() =>
        {
            MiniContainer container = new();

            UnityGameLogger logger = new();
            container.RegisterSingletonInstance<IGameLog>(logger);
            
            if (Ratify<PlayerDog>())
            {
                BoneService boneService = new(Retrieve<PlayerDog>(), logger);
                container.RegisterSingletonInstance<I_BoneService>(boneService);
                
                if (Ratify<EnemyDogs>())
                {
                    GameModel gameModel = new(Retrieve<PlayerDog>(), Retrieve<EnemyDogs>());
                    container.RegisterSingletonInstance(gameModel);
                }
            }

            if (Ratify<(int numRows, int numColumns)>())
            {
                MoveService moveService = new(Retrieve<(int numRows, int numColumns)>(), logger);
                container.RegisterSingletonInstance<I_MoveService>(moveService);
            }
            
            return container;
        });
    }
}
