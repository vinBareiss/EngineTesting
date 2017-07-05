using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.Diagnostics;

namespace EngineTesting
{
    public class ShaderProgram : OGLHandle
    {
        public bool IsUsed { get; private set; }

        #region Constructor

        public ShaderProgram(params Shader[] shaders) : base(GL.CreateProgram())
        {
            foreach (Shader shader in shaders)
                GL.AttachShader(this, shader);

            GL.LinkProgram(this);
            Console.WriteLine(GL.GetProgramInfoLog(this));

            foreach (Shader shader in shaders)
                GL.DetachShader(this, shader);
        }

        public ShaderProgram(string vSrc, string fSrc) : this(new Shader[] { new Shader(vSrc, ShaderType.VertexShader), new Shader(fSrc, ShaderType.FragmentShader) })
        {
        }

        #endregion

        #region UniformHandling
        public int GetUniformLoc(string name)
        {
            int loc = GL.GetUniformLocation(this, name);
            if (loc == -1) {
                Debug.WriteLine($"Uniform {name} nicht gefunden");
                return -1;
            }
            else {
                return loc;
            }
        }

        public void SetUniformMat4(string name, Matrix4 mat)
        {
            GL.UniformMatrix4(this.GetUniformLoc(name), false, ref mat);
        }
        public void SetUniformF1(string name, float f)
        {
            GL.Uniform1(this.GetUniformLoc(name), f);
        }
        public void SetUniform2(string name, Vector2 vec)
        {
            GL.Uniform2(this.GetUniformLoc(name), vec);
        }
        public void SetUniform3(string name, Vector3 vec)
        {
            GL.Uniform3(this.GetUniformLoc(name), vec);
        }
        #endregion

        #region Binding

        public void Use()
        {
            IsUsed = true;
            GL.UseProgram(this);
        }

        public void UnUse()
        {
            IsUsed = false;
            GL.UseProgram(0);
        }

        #endregion        }
    }
}
