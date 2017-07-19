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
    class ShaderProgram : Singelton<ShaderProgram>
    {
        List<int> mShaders;
        int mHandle = -1;
        public int Handle { get { return mHandle; } }

        private Dictionary<string, int> mUniforms;


        public ShaderProgram()
        {
            mHandle = GL.CreateProgram();
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
            int handle = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(handle, text);
            GL.CompileShader(handle);
            if (!string.IsNullOrWhiteSpace(GL.GetShaderInfoLog(handle))) {
                throw new ApplicationException("Shader compilation failed");
            }
            mShaders.Add(handle);
        }

        protected void Compile()
        {
            foreach (int shaderHandle in mShaders) {
                GL.AttachShader(this, shaderHandle);
            }

            GL.LinkProgram(this);
            if (!String.IsNullOrWhiteSpace(GL.GetProgramInfoLog(this))) {
                throw new ApplicationException("Program linking failed");
            }

            foreach (int shaderHandle in mShaders) {
                GL.DetachShader(this, shaderHandle);
            }
        }

        protected void AddUniform(string name)
        {
            int loc = GL.GetUniformLocation(this, name);
            if (loc == -1)
                throw new ApplicationException("Unknown Uniform");
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
        /// Allows for implicit cast of derived type to its handle
        /// </summary>
        /// <param name="prop"></param>
        public static implicit operator int(ShaderProgram prop)
        {
            return prop.mHandle;
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
