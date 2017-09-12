using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EngineTestingNrDuo.src.util;
using EngineTestingNrDuo.src.shading;
using EngineTestingNrDuo.src.core;

using EngineTestingNrDuo.src.util.buffer;

namespace EngineTestingNrDuo.res.models
{
    class Box : Model
    {
        #region "Singelton"
        private static Box mInstance = null;
        public static Box GetInstance()
        {
            if (mInstance == null)
                return mInstance = new Box();
            else
                return mInstance;
        }
        #endregion

        public Box()
        {
            this.mData = ResourceLoader.LoadFile("res/models/box.obj");
        }
    }
}
