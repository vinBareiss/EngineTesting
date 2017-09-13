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
        GameObject mParent;
        List<GameObject> mChildren;
        Dictionary<string, Component> mComponents;

        public Dictionary<string, Component> Components { get { return mComponents; } }
        public GameObject Parent
        {
            get { return mParent; }
            set { mParent = value; }
        }
        public Transform Transform
        {
            get { return (Transform)Components["transform"]; }
            set { Components["transform"] = value; }
        }
        public List<GameObject> Children { get { return mChildren; } }

        public GameObject() //Reminder: parent = null bei rootObj
        {
            mChildren = new List<GameObject>();
            mComponents = new Dictionary<string, Component>();
            AddComponent("transform", new Transform());
        }

        public void AddComponent(string name, Component component)
        {
            component.Parent = this;
            mComponents.Add(name, component);
        }
        public void AddChild(GameObject child)
        {
            mChildren.Add(child);
            child.Parent = this;
        }



        public void Update()
        {
            //check if this is the top of the tree
            if (this.Parent != null) {
                //Not the top, we need to update the local transform
                this.Transform += this.mParent.Transform;
            }

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
