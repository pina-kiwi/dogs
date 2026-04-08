using System;
using Game.Runtime;
using Game339.Shared;
using Game339.Shared.Models;
using UnityEngine;

namespace overworld
{

    public class PlayerHpView : ObserverMonoBehaviour
    {
        [SerializeField] private GameObject fullHeart, halfHeart, emptyHeart;
        
        private static GameState GameState => ServiceResolver.Resolve<GameState>();
        private static int MaxHealth => GameState.Player.MaxHealth.Value;

        [Serializable]
        public class ObservableInt : ObservableValue<int>, ISerializationCallbackReceiver
        {
            [SerializeField] public int initialValue;

            public void OnAfterDeserialize() => Value = initialValue;
            public void OnBeforeSerialize() => initialValue = Value;
        }

        protected override void Subscribe()
        {
            GameState.Player.Health.ChangeEvent += PlayerHealthChange;
        }

        protected override void Unsubscribe()
        {
            GameState.Player.Health.ChangeEvent -= PlayerHealthChange;
        }

        private void PlayerHealthChange(int health)
        {
            //print(health);
            
            foreach (Transform child in transform) Destroy(child.gameObject);

            for (int i = 0; i < MaxHealth; i+=2)
            {
                bool? hasHeart = health > i+1 ? true : null;
                      hasHeart = health < i+1 ? false : null;
                      
                GameObject heart = hasHeart == true ? fullHeart : halfHeart;
                           heart = hasHeart == false ? emptyHeart : halfHeart;
                
                Instantiate(heart, gameObject.transform);
            }
        }
    }
}