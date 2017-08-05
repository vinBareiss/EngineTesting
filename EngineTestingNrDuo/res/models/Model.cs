using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EngineTestingNrDuo.src.util;
using EngineTestingNrDuo.src.util.buffer;
using OpenTK;
using EngineTestingNrDuo.src.core.components;
using EngineTestingNrDuo.src.shading;

namespace EngineTestingNrDuo.res.models
{
    /// <summary>
    /// Class for all Models to inherit, contains fields for VAO, IBO
    /// Acutall data filling / retriving happens in derived classes
    /// All derived classes implement singelton pattern
    /// </summary>
    /// TODO: Implement singelton here?
    abstract class Model
    {
        /// <summary>
        /// This Function takes the Parsed data from the OBJ file and buffers it into the right VBO´s IBO´s and
        /// produces a RenderInfo object that can be used to draw this Model
        /// </summary>
        /// <param name="prog"></param>
        /// <returns></returns>
        public RenderInfo GetRenderInfo(ShaderProgram prog)
        {
            //first we check what data the Shader does take...
            
            //we then check if the obj file supplied that data

            //if yes, we set the VertexAttrib arrays

            //and build the renderInfo object

            return null;
        }
    }
}
