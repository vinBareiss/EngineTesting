using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineTestingNrDuo.src.util;

namespace EngineTestingNrDuo.src.core
{
    sealed class Scenegraph : Singelton<Scenegraph>
    {
        GameObject mRootObject = null;


        public Scenegraph()
        {
            mRootObject = new GameObject();
        }
        public void Update()
        {
            mRootObject.Update();
        }
    }
}
