using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL4;

namespace EngineTesting.src.Engine.Shader
{

    public class Shader : OGLHandle
    {


        public Shader(ShaderType type, string[] sources) : base(GL.CreateShader(type)) {
            //get all shader lengths
            int[] lengths = new int[sources.Length];
            for (int i = 0; i < sources.Length; i++)
                lengths[i] = sources[i].Length;

            //give sources to Open Gl
            GL.ShaderSource(this, sources.Length, sources, lengths);

            //compile
            GL.CompileShader(this);

            //check of error
            string infoLog = GL.GetShaderInfoLog(this);
            if (!string.IsNullOrWhiteSpace(infoLog))
                throw new ApplicationException("Shader compilation Failed");
            

        }
    }
}