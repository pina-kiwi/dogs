using System;
using Game.Runtime;
using Game339.Shared;
using Game339.Shared.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Runtime
{
    [Serializable]
    public class ObservableBool : ObservableValue<bool>, ISerializationCallbackReceiver
    {
        [SerializeField] private bool initialValue;

        public void OnAfterDeserialize() => Value = initialValue;
        public void OnBeforeSerialize() => initialValue = Value;
    }
}

public class SceneView : ObserverMonoBehaviour
{
    private static GameState GameState => ServiceResolver.Resolve<GameState>();
    private static bool _sceneJustChanged = true;

    protected override void Subscribe()
    {
        GameState.IsInCombat.ChangeEvent += CombatStateChange;
    }

    protected override void Unsubscribe()
    {
        GameState.IsInCombat.ChangeEvent -= CombatStateChange;
    }
    
    private static void CombatStateChange(bool enteredCombat)
    {
        if (_sceneJustChanged) { _sceneJustChanged = false; return; }
        
        string sceneToLoad = enteredCombat ? "combatScene" : "walkingScene";
        _sceneJustChanged = true;
        SceneManager.LoadScene(sceneToLoad);
    }
}