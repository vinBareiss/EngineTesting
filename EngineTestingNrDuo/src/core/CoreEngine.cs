using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineTestingNrDuo.src.core
{
    class CoreEngine
    {
        #region "Singelton"
        private static CoreEngine mInstance = null;
        public static CoreEngine GetInstance()
        {
            if (mInstance == null)
                return mInstance = new CoreEngine();
            else
                return mInstance;
        }
        #endregion

        RenderingEngine renderEngine;
        Scenegraph sceneGraph;
        Camera camera;

        public CoreEngine()
        {
            renderEngine = RenderingEngine.GetInstance();
            sceneGraph = Scenegraph.GetInstance();
            camera = Camera.GetInstance();
        }

        public void Start(GameWindow w)
        {
            camera.Start(w);
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

    }
}
