using UnityEngine;

namespace Environment
{
    public class BaseComponent : MonoBehaviour
    {
        [SerializeField] private BaseComponentsKeeper _baseComponentsKeeper;
        
        public BaseComponentsKeeper BaseComponentsKeeper
        {
            get
            {
                TryToRegisterObjectInKeeper();
                return _baseComponentsKeeper;
            }
            protected set
            {
                _baseComponentsKeeper = value;
                TryToRegisterObjectInKeeper();
            }
        }

        private bool _registeredInKeeper = false;

        public void SetKeeperRuntime(BaseComponentsKeeper baseComponentsKeeper)
        {
            BaseComponentsKeeper = baseComponentsKeeper;
        }
        
        protected virtual void OnValidate()
        {
            _baseComponentsKeeper = GetComponentInParent<BaseComponentsKeeper>(true);
        }

        protected virtual void Awake()
        {
            if(_baseComponentsKeeper == null)
                _baseComponentsKeeper = GetComponentInParent<BaseComponentsKeeper>(true);
            
            TryToRegisterObjectInKeeper();
        }

        protected void TryToRegisterObjectInKeeper()
        {
            if(_registeredInKeeper)
                return;
            
            if (!_baseComponentsKeeper)
                return;
            
            _baseComponentsKeeper.TryToAddComponent(this);
            
            _registeredInKeeper = true;
        }

        protected virtual void OnDestroy()
        {
            if(!_baseComponentsKeeper)
                return;
            
            _baseComponentsKeeper.TryToDeleteComponent(this);
        }
    }
}
