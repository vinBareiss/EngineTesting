using EngineTestingNrDuo.src.util;
using EngineTestingNrDuo.src.core;

namespace EngineTestingNrDuo.src.shading
{

    /// <summary>
    /// A Testing-Shader that uses the NormalCoordinates of a model as its color.
    /// </summary>
    class NormalViewShader : ShaderProgram
    {
        #region "Singelton"
        private static NormalViewShader mInstance = null;
        public static NormalViewShader GetInstance()
        {
            if (mInstance == null)
                return mInstance = new NormalViewShader();
            else
                return mInstance;
        }
        #endregion

        public NormalViewShader()
        {
            mVertexFormat = new VertexFormatFlag[] { VertexFormatFlag.Position, VertexFormatFlag.Normal };

            AddVertexShader(ResourceLoader.LoadShader("res/shaders/normalView_vertex.glsl"));
            AddFragmentShader(ResourceLoader.LoadShader("res/shaders/normalView_fragment.glsl"));
            Compile();

            //AddUniform("transform");
        }

        public override void UpdateUniforms(GameObject gameObject)
        {
            Camera cam = Camera.GetInstance();
            //SetUniform("transform", gameObject.Transform.Model * cam.ViewMatrix * cam.ProjectionMatrix, false);
        }
    }
}
