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

            AddVertexShader(ResourceLoader.LoadShader("res/shaders/normalView/vertex.glsl"));
            AddFragmentShader(ResourceLoader.LoadShader("res/shaders/normalView/fragment.glsl"));
            Compile();

            AddUniform("transform.model");
            AddUniform("transform.view");
            AddUniform("transform.projection");
        }

        public override void UpdateUniforms(GameObject gameObject)
        {
            Camera cam = Camera.GetInstance();

            SetUniform("transform.model", gameObject.Transform.Model);
            SetUniform("transform.view", cam.ViewMatrix);
            SetUniform("transform.projection", cam.ProjectionMatrix);
        }
    }
}
