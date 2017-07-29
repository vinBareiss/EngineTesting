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
        public Transform Transform { get { return (Transform)Components["transform"]; } }
        public List<GameObject> Children { get { return mChildren; } }

        public GameObject() //Reminder: parent = null bei rootObj
        {
            mChildren = new List<GameObject>();
            mComponents = new Dictionary<string, Component>();
            Transform transform = new Transform() {
                Parent = this
            };
            mComponents.Add("transform", transform);
        
        }

        public void AddComponent(string name, Component component)
        {
            component.Parent = this;
            mComponents.Add(name, component);
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
