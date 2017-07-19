using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL4;
using EngineTestingNrDuo.src.util;

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

        public void AddVertexShader(string text)
        {
            AddShader(text, ShaderType.VertexShader);
        }
        public void AddFragmentShader(string text)
        {
            AddShader(text, ShaderType.FragmentShader);
        }

        /// <summary>
        /// Actuall void for creating, compiling and checking shaders
        /// </summary>
        /// <param name="text">source code of shader</param>
        /// <param name="type">type of shader</param>
        public void AddShader(string text, ShaderType type)
        {
            int handle = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(handle, text);
            GL.CompileShader(handle);
            if (!string.IsNullOrWhiteSpace(GL.GetShaderInfoLog(handle))) {
                throw new ApplicationException("Shader compilation failed");
            }
            mShaders.Add(handle);
        }

        public void AddUniform(string name)
        {
            int loc = GL.GetUniformLocation(this, name);
            if (loc == -1)
                throw new ApplicationException("Unknown Uniform");
            mUniforms.Add(name, loc);
        }

        public void Compile()
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

        public static implicit operator int(ShaderProgram prop)
        {
            return prop.mHandle;
        }
    }
}
