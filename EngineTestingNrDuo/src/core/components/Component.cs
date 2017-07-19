using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineTestingNrDuo.src.core.components
{
    /// <summary>
    /// Base class for all components to inherit
    /// </summary>
    abstract class Component
    {
        private GameObject mParent;
        public GameObject Parent
        {
            get { return mParent; }
            set { mParent = value; }
        }

        public Component(GameObject parent)
        {
            mParent = parent;
        }
        
        public virtual void Update() { }
    }
}
