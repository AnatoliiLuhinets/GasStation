using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Environment
{
    public class BaseComponentsKeeper : MonoBehaviour
    {
        [SerializeField] private BaseComponentsKeeper ParentKeeper;
        [SerializeField] private List<BaseComponentsKeeper> ChildKeepers = new ();

        public event Action<BaseComponentsKeeper> OnDestroying;
        
        private readonly List<BaseComponent> _componentLinks = new();

        public bool IsRootKeeper => !ParentKeeper;

        private void Awake()
        {
            foreach (var childKeeper in ChildKeepers)
            {
                childKeeper.OnDestroying += TryToRemoveChildKeeper;
            }
        }

        private void OnDestroy()
        {
            foreach (var childKeeper in ChildKeepers)
            {
                childKeeper.OnDestroying -= TryToRemoveChildKeeper;
            }

            OnDestroying?.Invoke(this);
        }

        public void TryToAddComponent(BaseComponent component)
        {
            if(_componentLinks.Contains(component))
                return;
            
            _componentLinks.Add(component);
        }

        public void TryToDeleteComponent(BaseComponent component)
        {
            if(!_componentLinks.Remove(component))
                return;
        }
        
        public T TryToGetFirstComponentOfType<T>(bool includeChildrenKeepers)
        {
            foreach (var component in _componentLinks)
            {
                if (component is T typedComponent)
                    return typedComponent;
            }
            
            if(!includeChildrenKeepers)
                return default;

            foreach (var childKeeper in ChildKeepers)
            {
                if(!childKeeper)
                    continue;

                var component = childKeeper.TryToGetFirstComponentOfType<T>(true);

                if (component != null)
                    return component;
            }
            
            return default;
        }
        
        public BaseComponentsKeeper GetRoot()
        {
            if (IsRootKeeper)
                return this;

            return ParentKeeper.GetRoot();
        }
        
        public T TryToGetFirstComponentOfTypeInRoot<T>(bool includeChildrenKeepers)
        {
            if (IsRootKeeper)
                return TryToGetFirstComponentOfType<T>(includeChildrenKeepers);

            return ParentKeeper.TryToGetFirstComponentOfTypeInRoot<T>(includeChildrenKeepers);
        }

        public IEnumerable<T> GetAllComponentsOfType<T>(bool includeChildrenKeepers)
        {
            foreach (var component in _componentLinks)
            {
                if (component is T typedComponent)
                     yield return typedComponent;
            }

            if (!includeChildrenKeepers)
                yield break;

            foreach (var childKeeper in ChildKeepers)
            {
                if(!childKeeper)
                    continue;

                foreach (var item in childKeeper.GetAllComponentsOfType<T>(true))
                {
                    yield return item;
                }
            }
        }
        
        public IEnumerable<T> GetAllComponentsOfTypeInRoot<T>(bool includeChildrenKeepers)
        {
            if (IsRootKeeper)
                return GetAllComponentsOfType<T>(includeChildrenKeepers);

            return ParentKeeper.GetAllComponentsOfTypeInRoot<T>(includeChildrenKeepers);
        }

        public void TryToSetParentKeeper(BaseComponentsKeeper parentKeeper)
        {
            if(!transform.IsChildOf(parentKeeper.transform))
                return;

            ParentKeeper = parentKeeper;
        }

        private void TryToAddChildKeeper(BaseComponentsKeeper childKeeper)
        {
            if(!childKeeper.transform.IsChildOf(transform))
                return;

            if(ChildKeepers.Contains(childKeeper))
                return;
            
            ChildKeepers.Add(childKeeper);
            childKeeper.OnDestroying += TryToRemoveChildKeeper;
            InvokeChildKeepersChangedRecursively();
        }

        private void TryToRemoveChildKeeper(BaseComponentsKeeper childKeeper)
        {
            if(!ChildKeepers.Contains(childKeeper))
                return;

            ChildKeepers.Remove(childKeeper);
            childKeeper.OnDestroying -= TryToRemoveChildKeeper;
            InvokeChildKeepersChangedRecursively();
        }

        private void InvokeChildKeepersChangedRecursively()
        {
            if(ParentKeeper)
                ParentKeeper.InvokeChildKeepersChangedRecursively();
        }

        private void OnTransformParentChanged()
        {
            var prevParentKeeper = ParentKeeper;
            
            var parent = transform.parent;
            ParentKeeper = parent ? parent.GetComponentInParent<BaseComponentsKeeper>(true) : null;

            if (prevParentKeeper != ParentKeeper)
            {
                if(prevParentKeeper)
                    prevParentKeeper.TryToRemoveChildKeeper(this);
                
                if(ParentKeeper)
                    ParentKeeper.TryToAddChildKeeper(this);
            }
        }

        private void OnValidate()
        {
            var parent = transform.parent;
            ParentKeeper = parent ? parent.GetComponentInParent<BaseComponentsKeeper>(true) : null;
            SetChildKeepers();
        }

        protected void SetChildKeepers()
        {
            var possibleChildKeepers = GetComponentsInChildren<BaseComponentsKeeper>(true)
                .Where(k => k != this)
                .ToList();

            foreach (var childKeeper in possibleChildKeepers)
                childKeeper.SetChildKeepers();

            ChildKeepers.Clear();
            foreach (var possibleChild in possibleChildKeepers)
            {
                var anyChildContainsThis =
                    possibleChildKeepers.Any(k => k != possibleChild && possibleChild.transform.IsChildOf(k.transform));
                if(anyChildContainsThis)
                    continue;
                ChildKeepers.Add(possibleChild);
            }
        }
    }
}
