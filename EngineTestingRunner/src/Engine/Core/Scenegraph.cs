using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineTesting.src.Engine.Core
{
    class Scenegraph : Singelton<Scenegraph>
    {
        GameObject rootObject = null;

        /// <summary>
        /// Initialize Scenegraph
        /// </summary>
        public Scenegraph()
        {
            //make root object, all other objects will be a child of this object
            rootObject = new GameObject(null);
        }


        public void Update()
        {

                   }
    }
}
