using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
namespace System.Runtime.CompilerServices { internal static class IsExternalInit {} }

namespace Game339.Shared.Infastructure.DataTypes
{
    // ReSharper disable once InconsistentNaming
    public class LazyReadonlyValue<T> : IEquatable<T>
    {
        private static readonly IEqualityComparer<T> DefaultComparer = EqualityComparer<T>.Default;
        
        private T _value;
        private MethodBase _initializingMethod;

        public T Value
        {
            get => _value;
            set => TrySet(value);
        }

        private void TrySet(T value)
        {
            MethodBase caller = new StackFrame(2).GetMethod();
            
            if (_initializingMethod != null && caller != _initializingMethod) throw new InvalidOperationException($"{_ClassName()} is readonly after initialization.");

            _value = value;
            _initializingMethod = caller;
            
            string _ClassName()
            {
                const string className = nameof(LazyReadonlyValue<T>);
                string typeName = typeof(T).Name;
                return $"{className}<{typeName}>";
            }
        }

        public static implicit operator T(LazyReadonlyValue<T> value) => value.Value;
        public static implicit operator LazyReadonlyValue<T>(T value) => new(value);
        
        private LazyReadonlyValue(T value) {
            _value = value;
            _initializingMethod = MethodBase.GetCurrentMethod();
        }

        public bool Equals(T other) => DefaultComparer.Equals(_value, other);

        public override bool Equals(object obj)
        {
            return (obj is T other) ? Equals(other) : ReferenceEquals(this, obj);
        }

        // ReSharper disable once NonReadonlyMemberInGetHashCode
        public override int GetHashCode() => DefaultComparer.GetHashCode(_value);

        public override string ToString() => Value.ToString();
    }
}