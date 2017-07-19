using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using OpenTK.Graphics.OpenGL4;
using EngineTestingNrDuo.src.util;

namespace EngineTestingNrDuo.src.shading
{
    class UnlitShader : ShaderProgram
    {
        public UnlitShader() : base()
        {
            AddVertexShader(ResourceLoader.LoadShader("res/shaders/unlit_vertext.glsl"));
            AddFragmentShader(ResourceLoader.LoadShader("res/shaders/unlit_fragment.glsl"));

            AddUniform("transform.model");
            AddUniform("transform.projection");
            AddUniform("transform.view");
        }



    }
}
