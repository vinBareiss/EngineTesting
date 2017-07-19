using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EngineTestingNrDuo.src.core.components;

namespace EngineTestingNrDuo.src.core
{
    class GameObject
    {
        Dictionary<string, Component> mComponents;
        List<GameObject> mChildren;
        GameObject mParent;
        public GameObject Parent
        {
            get { return mParent; }
            set { mParent = value; }
        }


        public GameObject()
        {
            mChildren = new List<GameObject>();
            mComponents = new Dictionary<string, Component> {
                { "transform", new Transform(this) }
            };
        }

        public void Update()
        {
            //TODO: Update local transform
            
            //update this
            foreach (Component component in mComponents.Values) {
                component.Update();
            }

            //update all childen
            foreach (GameObject child in mChildren) {
                child.Update();
            }
        }
    }
}
