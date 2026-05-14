using System;
using Game339.Shared.Infastructure.DataTypes;
using Game339.Shared.Models;
using UnityEngine;

namespace Game.Runtime.World
{
    public abstract class WorldObject<T_GridObject> : MonoBehaviour where T_GridObject : GridObject
    {
        protected LazyReadonlyValue<T_GridObject> Model;
        protected virtual T_GridObject InstantiateModel() => (T_GridObject)Activator.CreateInstance(typeof(T_GridObject), World2GridPos);

        protected virtual void Awake()
        {
            transform.position = new(transform.position.x, transform.position.y, RenderPriority);
            Model.Value = InstantiateModel();
        }

        protected virtual int RenderPriority => 1;
        protected GridPosition World2GridPos => (Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.x));
        protected Vector3 Grid2WorldPos => new Vector3(Model.Value.Position.col, Model.Value.Position.row, RenderPriority);
    }
    
    public abstract class WorldEntity<T_GridEntity> : WorldObject<T_GridEntity> where T_GridEntity : GridEntity
    {
        protected override void Awake()
        {
            base.Awake();
            Model.Value.StepEvent += OnStepEvent;
        }
        
        protected override int RenderPriority => 3;

        private void OnStepEvent(GridPosition position)
        {
            gameObject.transform.position = Grid2WorldPos;
        }
    }
    
    public abstract class WorldProp<T_GridEntity> : WorldObject<T_GridEntity> where T_GridEntity : GridEntity
    {
        protected override int RenderPriority => 2;
    }
}
