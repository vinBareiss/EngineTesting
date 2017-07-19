using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EngineTestingNrDuo.src.shading;
using EngineTestingNrDuo.src.core.buffer;


namespace EngineTestingNrDuo.src.core.components
{
    /// <summary>
    /// Class that contains information on which VAO and Shader the Rendering engine has to use
    /// </summary>
    class RenderInfo : Component
    {
        public ShaderProgram Shader { get; private set; }
        public VertexArray VAO { get; private set; }
        public int Length { get; private set; }

        public RenderInfo(GameObject gameObject, ShaderProgram shader, VertexArray vao, int length)
            : base(gameObject)
        {
            this.Shader = shader;
            this.VAO = vao;
            this.Length = length;

            //Register this RenderInfo with the RenderingEngine so it gets drawn next call
            RenderingEngine.GetInstance().AddRenderInfo(this);
        }
    }
}
