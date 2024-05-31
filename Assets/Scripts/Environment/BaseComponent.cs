using UnityEngine;
using UnityEngine.Serialization;

namespace Environment
{
    public class BaseComponent : MonoBehaviour
    {
        [FormerlySerializedAs("_baseComponentsKeeper")] [SerializeField] private ComponentsKeeper componentsKeeper;
        
        public ComponentsKeeper ComponentsKeeper
        {
            get
            {
                ToRegisterInKeeper();
                return componentsKeeper;
            }
            protected set
            {
                componentsKeeper = value;
                ToRegisterInKeeper();
            }
        }

        private bool _registeredInKeeper = false;
        
        protected virtual void OnValidate()
        {
            componentsKeeper = GetComponentInParent<ComponentsKeeper>(true);
        }

        protected virtual void Awake()
        {
            if(componentsKeeper == null)
                componentsKeeper = GetComponentInParent<ComponentsKeeper>(true);
            
            ToRegisterInKeeper();
        }

        private void ToRegisterInKeeper()
        {
            if(_registeredInKeeper)
                return;
            
            if (!componentsKeeper)
                return;
            
            componentsKeeper.TryToAddComponent(this);
            
            _registeredInKeeper = true;
        }

        protected virtual void OnDestroy()
        {
            if(!componentsKeeper)
                return;
            
            componentsKeeper.TryToDeleteComponent(this);
        }
    }
}
