using System;
using Game.Runtime;
using Game339.Shared;
using Game339.Shared.Models;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpView : ObserverMonoBehaviour
{
    private static GameState GameState => ServiceResolver.Resolve<GameState>();

    public Image Heart1;
    public Image Heart2;
    public Image Heart3;
    
    public Sprite FullHeartSprite;
    public Sprite EmptyHeartSprite;

    [Serializable]
    public class ObservableInt : ObservableValue<int>, ISerializationCallbackReceiver
    {
        [SerializeField] public int initialValue;

        public void OnAfterDeserialize()  => Value = initialValue;
        public void OnBeforeSerialize()   => initialValue = Value;
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
        print(health);

        Heart1.sprite = (health >= 1) ? FullHeartSprite : EmptyHeartSprite;
        Heart2.sprite = (health >= 2) ? FullHeartSprite : EmptyHeartSprite;
        Heart3.sprite = (health >= 3) ? FullHeartSprite : EmptyHeartSprite;
    }
}
