﻿using System;
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
        /// <summary>
        /// Gameobject this component is assigned to. Field and Public Property
        /// </summary>
        private GameObject mParent;
        public GameObject Parent
        {
            get { return mParent; }
            set { mParent = value; }
        }

        public Component() { }

        public virtual void Update() { }
    }
}
