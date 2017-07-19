using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using OpenTK.Graphics.OpenGL4;
using EngineTestingNrDuo.src.util;
using EngineTestingNrDuo.src.core;


namespace EngineTestingNrDuo.src.shading
{
    /// <summary>
    /// Very simple shader, no lighting calculations at all. For testing puropses mostly.
    /// </summary>
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

        public override void UpdateUniforms(GameObject gameObject)
        {
            SetUniform("transform.model", gameObject.Transform.Model);
            //TODO: camera class, proj + view mat4
        }
    }
}
