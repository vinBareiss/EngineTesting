using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL4;

using EngineTestingNrDuo.src.shading;
using EngineTestingNrDuo.src.util.buffer;


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
        public BeginMode Mode { get; private set; }

        public RenderInfo(ShaderProgram shader, VertexArray vao, int length, BeginMode mode = BeginMode.Triangles)
        {
            this.Shader = shader;
            this.VAO = vao;
            this.Length = length;
            this.Mode = mode;
            
            //Register this RenderInfo with the RenderingEngine so it gets drawn next call
            RenderingEngine.GetInstance().AddRenderInfo(this);
        }
    }
}
