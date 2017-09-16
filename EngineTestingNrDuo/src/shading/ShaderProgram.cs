using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using OpenTK;
using OpenTK.Graphics.OpenGL4;
using EngineTestingNrDuo.src.util;
using EngineTestingNrDuo.src.core;
using System.Text.RegularExpressions;
using System.Diagnostics;
namespace EngineTestingNrDuo.src.shading
{
    abstract class ShaderProgram : OpenGlHandle
    {
        List<int> mShaders;
        private Dictionary<string, int> mUniforms;

        protected VertexFormatFlag[] mVertexFormat;
        public VertexFormatFlag[] VertexFormat { get { return mVertexFormat; } }


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
        private void AddShader(string text, ShaderType type)
        {
            //handle imports
            //replace #import <DATA> with actuall code
            foreach (Match m in Regex.Matches(text, "#include [a-z]+\\.[a-z]+;")) {
                //only get the include part
                string include = m.ToString().Replace("#include ", "").Replace(";","");
                switch (include.Split('.')[0]) {
                    case "structs":
                        text = text.Replace(m.ToString(), ShaderLibary.GetInstance().GetStruct(include.Split('.')[1]));
                        break;
                    case "functions":
                        text = text.Replace(m.ToString(), ShaderLibary.GetInstance().GetFunction(include.Split('.')[1]));
                        break;
                    default:
                        throw new ApplicationException("Unknown include");
                }
            }
            Debug.WriteLine(text);

            //create shader
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
                //Console.WriteLine("Uniform error");
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
