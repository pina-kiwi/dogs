using System;
using Game339.Shared.Models;
using UnityEngine;

namespace Game.Runtime.World
{
    public class Enemy<T_EnemyDog> : WorldEntity<T_EnemyDog> where T_EnemyDog : EnemyDog
    {
        enum EnemyType
        {
            Small,
            Medium,
            Large
        }

        [SerializeField] private EnemyType enemyType;

        protected override T_EnemyDog InstantiateModel()
        {
            Type dogType = typeof(SmallDog);
            
            switch (enemyType)
            {
                case EnemyType.Small:
                    dogType = typeof(SmallDog); break;
                case EnemyType.Medium:
                    dogType = typeof(MediumDog); break;
                case EnemyType.Large:
                    dogType = typeof(LargeDog); break;
            }
            return (T_EnemyDog)Activator.CreateInstance(dogType, World2GridPos);
        }
        protected override int RenderPriority => 4;
    }
}