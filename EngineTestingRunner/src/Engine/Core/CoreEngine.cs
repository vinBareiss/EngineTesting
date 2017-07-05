using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EngineTesting.src.Engine.Rendering;

namespace EngineTesting.src.Engine.Core
{
    class CoreEngine
    {
        Scenegraph scenegraph;
        RenderingEngine renderingEngine;


        public CoreEngine() {
            scenegraph = new Scenegraph();
            renderingEngine = new RenderingEngine();
        }

               
    }
}
