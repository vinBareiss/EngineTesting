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
            //tell the shader what we want
            mVertexFormat = new VertexFormatFlag[] { VertexFormatFlag.Position };

            AddVertexShader(ResourceLoader.LoadShader("res/shaders/unlit/vertex.glsl"));
            AddFragmentShader(ResourceLoader.LoadShader("res/shaders/unlit/fragment.glsl"));
            Compile();
            AddUniform("transform.model");
            AddUniform("transform.view");
            AddUniform("transform.projection");
            AddUniform("color");
        }

        public override void UpdateUniforms(GameObject gameObject)
        {
            //get cam for rast
            Camera cam = Camera.GetInstance();

            SetUniform("transform.model", gameObject.Transform.Model);
            SetUniform("transform.view",cam.ViewMatrix);
            SetUniform("transform.projection",cam.ProjectionMatrix);

            SetUniform("color", new OpenTK.Vector3(1.0f, 0.2f, 0));
        }
    }
}