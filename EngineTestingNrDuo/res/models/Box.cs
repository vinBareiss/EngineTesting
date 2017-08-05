using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EngineTestingNrDuo.src.util;


namespace EngineTestingNrDuo.res.models
{
    class Box : Model
    {
        #region Singelton
        static Box mInstance;
        public static Box GetInstance()
        {
            if (mInstance == null)
                return mInstance = new Box();
            else
                return mInstance;
        }
        #endregion

        private ParsedObj mData;

        public Box()
        {
            //this is the first time we want this Model, load it from its OBJ file
            ParsedObj data =ResourceLoader.LoadFile("res/models/Box.obj");
        }
    }
}
