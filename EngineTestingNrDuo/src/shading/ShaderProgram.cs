using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using OpenTK;
using OpenTK.Graphics.OpenGL4;
using EngineTestingNrDuo.src.util;
using EngineTestingNrDuo.src.core;

namespace EngineTestingNrDuo.src.shading
{
    abstract class ShaderProgram : OpenGlHandle
    {
        List<int> mShaders;
        private Dictionary<string, int> mUniforms;

        protected int mVertexFormat;
        public int VertexFormat { get { return mVertexFormat; } }


        /// <summary>
        /// We use this name for sorting all rendercalls by shader
        /// </summary>
        public string Name { get { return this.GetType().Name; } }

        public ShaderProgram() : base(GL.CreateProgram())
        {
            mShaders = new List<int>();
            mUniforms = new Dictionary<string, int>();
        }

        protected void AddVertexShader(string text)
        {
            AddShader(text, ShaderType.VertexShader);
        }
        protected void AddFragmentShader(string text)
        {
            AddShader(text, ShaderType.FragmentShader);
        }

        /// <summary>
        /// Actuall void for creating, compiling and checking shaders
        /// </summary>
        /// <param name="text">source code of shader</param>
        /// <param name="type">type of shader</param>
        private void AddShader(string text, ShaderType type)
        {
            int handle = GL.CreateShader(type);
            GL.ShaderSource(handle, text);

            GL.CompileShader(handle);
            GL.GetShader(handle, ShaderParameter.CompileStatus, out int statusCode);
            if (statusCode == 0) {
                throw new ApplicationException("Shader compilation failed", new Exception(GL.GetShaderInfoLog(handle)));
            }
            mShaders.Add(handle);
        }

        protected void Compile()
        {
            foreach (int shaderHandle in mShaders) {
                GL.AttachShader(this, shaderHandle);
            }

            GL.LinkProgram(this);
            GL.ValidateProgram(this);

            GL.GetProgram(this, GetProgramParameterName.LinkStatus, out int statusCode);

            if (statusCode == 0) {
                throw new ApplicationException("Program linking failed", new Exception(GL.GetProgramInfoLog(this)));
            }

            foreach (int shaderHandle in mShaders) {
                GL.DetachShader(this, shaderHandle);
            }
        }

        protected void AddUniform(string name)
        {
            int loc = GL.GetUniformLocation(this, name);
            if (loc == -1)
                throw new ApplicationException($"Unknown Uniform: {name}");
            mUniforms.Add(name, loc);
        }
        public virtual void UpdateUniforms(GameObject gameObject) { }


        //Overloaded voids for easy unifom updating 
        //TODO: add more types
        protected void SetUniform(string name, Matrix4 value, bool transpose = false)
        {
            GL.UniformMatrix4(mUniforms[name], transpose, ref value);
        }
        protected void SetUniform(string name, Vector3 value)
        {
            GL.Uniform3(mUniforms[name], ref value);
        }
        protected void SetUniform(string name, float value)
        {
            GL.Uniform1(mUniforms[name], value);
        }

        /// <summary>
        /// Binds ShaderProgram
        /// </summary>
        public void Use()
        {
            GL.UseProgram(this);
        }
        /// <summary>
        /// Unbinds ShaderProgram
        /// </summary>
        public void UnUse()
        {
            GL.UseProgram(0);
        }
    }
}
