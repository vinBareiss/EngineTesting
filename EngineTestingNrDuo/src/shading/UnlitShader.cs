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

            AddVertexShader(ResourceLoader.LoadShader("res/shaders/unlit_vertex.glsl"));
            AddFragmentShader(ResourceLoader.LoadShader("res/shaders/unlit_fragment.glsl"));
            Compile();
            AddUniform("transform");
            /*AddUniform("transform.projection");
            AddUniform("transform.view");*/
        }

        public override void UpdateUniforms(GameObject gameObject)
        {
            //get cam for rast
            Camera cam = Camera.GetInstance();

            SetUniform("transform", gameObject.Transform.Model * cam.ViewMatrix * cam.ProjectionMatrix, false);
            /*SetUniform("transform.view", OpenTK.Matrix4.Identity);
            SetUniform("transform.projection", OpenTK.Matrix4.Identity);*/
            //TODO: camera class, proj + view mat4
        }
    }
}