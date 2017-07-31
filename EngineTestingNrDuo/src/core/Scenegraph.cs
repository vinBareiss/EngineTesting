using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineTestingNrDuo.src.util;

namespace EngineTestingNrDuo.src.core
{
    sealed class Scenegraph
    {
        #region "Singelton"
        private static Scenegraph mInstance = null;
        public static Scenegraph GetInstance()
        {
            if (mInstance == null)
                return mInstance = new Scenegraph();
            else
                return mInstance;
        }
        #endregion
        
        GameObject mRootObject = null;
        public GameObject Root { get { return mRootObject; } }

        public Scenegraph()
        {
            mRootObject = new GameObject();
            mRootObject.Parent = null;
        }
        public void Update()
        {
            mRootObject.Update();
        }
    }
}
