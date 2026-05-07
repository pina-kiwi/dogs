using System;
using Game.Runtime;
using Game339.Shared;
using Game339.Shared.Models;
using TMPro;
using UnityEngine;

namespace battle
{
    public class PlayerStatsView : ObserverMonoBehaviour
    {
        [SerializeField] private TMP_Text hpText;
        [SerializeField] private TMP_Text atkText;

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
            GameState.Player.AttackPower.ChangeEvent += PlayerDamageChange;
        }

        protected override void Unsubscribe()
        {
            GameState.Player.Health.ChangeEvent -= PlayerHealthChange;
            GameState.Player.AttackPower.ChangeEvent -= PlayerDamageChange;
        }

        private void PlayerHealthChange(int changedHealth)
        {
            //print(health);
            
            if (changedHealth <= 0) BattleManager.EndBattle();
            
            hpText.text = $"{changedHealth}";
            
            if (MaxHealth != int.MaxValue) hpText.text += $" / {MaxHealth}";
        }
        
        private void PlayerDamageChange(int changedDamage)
        {
            atkText.text = $"{changedDamage}";
        }
    }
}