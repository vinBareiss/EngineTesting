using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EngineTesting.src.Engine.Core.Components;

namespace EngineTesting.src.Engine.Core
{
    class GameObject
    {
        GameObject mParent;
        List<GameObject> mChildren;

        Dictionary<string, Component> mComponents;


        public GameObject(GameObject parent)
        {
            mParent = parent;
            mChildren = new List<GameObject>();

            mComponents = new Dictionary<string, Component>();

            

        }

        internal void Update()
        {
            throw new NotImplementedException();
        }
    }
}
