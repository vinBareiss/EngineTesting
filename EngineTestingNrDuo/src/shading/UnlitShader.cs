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
        #region "Singelton"
        private static UnlitShader mInstance = null;
        public static UnlitShader GetInstance()
        {
            if (mInstance == null)
                return mInstance = new UnlitShader();
            else
                return mInstance;
        }
        #endregion

        public UnlitShader() : base()
        {
            AddVertexShader(ResourceLoader.LoadShader("res/shaders/unlit_vertex.glsl"));
            AddFragmentShader(ResourceLoader.LoadShader("res/shaders/unlit_fragment.glsl"));
            Compile();
            AddUniform("transform");
            /*AddUniform("transform.projection");
            AddUniform("transform.view");*/
        }

        public override void UpdateUniforms(GameObject gameObject)
        {
            SetUniform("transform", gameObject.Components["transform"] * Camera.GetInstance().);
            /*SetUniform("transform.view", OpenTK.Matrix4.Identity);
            SetUniform("transform.projection", OpenTK.Matrix4.Identity);*/
            //TODO: camera class, proj + view mat4
        }
    }
}
