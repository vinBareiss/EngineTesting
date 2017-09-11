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
using EngineTestingNrDuo.src.core;


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
        protected ParsedObj mData;


        public GameObject GetGameObject(ShaderProgram shader)
        {
            VertexArray vao = new VertexArray();
            vao.Bind();

            VertexBuffer<float> vbo = new VertexBuffer<float>();
            vbo.Bind();

            //for every flag in VertexFormatFlat
            for (int i = 0; i < 4; i++) {
                if ((shader.VertexFormat & (1 << i)) != 0) {
                    //flag is set in shader
                    if ((mData.GetVertexFormat() & (1 << i)) != 0) {
                        //this data exists, create vertexattribpointer for it
                        -
                    } else {
                        throw new ApplicationException("Shader requested data that the model did not deliver");
                    }
                }
            }

        }

        protected static void SetVertexAttribPointer(ShaderProgram shader, ParsedObj modelData)
        {


        }

        public static bool CheckVertexFormat(int shaderFormat, int vertexFormat)
        {
            //dont need to check for pos, every vertex has that
            //check for UV
            if ((shaderFormat & (int)VertexFormatFlag.UvCoord) != 0) {
                //shader wants this
                if ((vertexFormat & (int)VertexFormatFlag.UvCoord) == 0)
                    throw new ApplicationException("Shader requested UVCoords that were not supplied by the model");
            }

            //check for Normal
            if ((shaderFormat & (int)VertexFormatFlag.Normal) != 0) {
                //shader wants this
                if ((vertexFormat & (int)VertexFormatFlag.Normal) == 0)
                    //vertex dont have this
                    throw new ApplicationException("Shader requested UVCoords that were not supplied by the model");
            }

            //if we got to this without an exception, we are aOk
            return true;
        }
    }
}
