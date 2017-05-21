using System;

using System.IO;
using OpenTK.Graphics.OpenGL4;


namespace EngineTesting.src.Engine
{
    public class Shader : OGLHandle
    {
        public Shader(string src, ShaderType type) : base(GL.CreateShader(type)) {
            GL.ShaderSource(this, src);
            GL.CompileShader(this);
        }

        public static string FromFile(string path) {
            using (StreamReader sr = new StreamReader(path)) {
                return sr.ReadToEnd();
            }
        }
    }

    public class ShaderProgram : OGLHandle
    {
        public bool IsUsed { get; private set; }

        public ShaderProgram(params Shader[] shaders) : base(GL.CreateProgram()) {
            foreach (Shader shader in shaders)
                GL.AttachShader(this, shader);

            GL.LinkProgram(this);
            Console.WriteLine(GL.GetProgramInfoLog(this));

            foreach (Shader shader in shaders)
                GL.DetachShader(this, shader);
        }

        public ShaderProgram(string vSrc, string fSrc) : this(new Shader[] { new Shader(vSrc, ShaderType.VertexShader), new Shader(fSrc, ShaderType.FragmentShader) }) {
        }

        public int GetUniformLoc(string name) {
            int loc = GL.GetUniformLocation(this, name);
            if (loc == -1) {
                Console.WriteLine($"Uniform {name} nicht gefunden");
                return -1;
            } else {
                return loc;
            }
        }


        public void Use() {
            IsUsed = true;
            GL.UseProgram(this);
        }

        public void UnUse() {
            IsUsed = false;
            GL.UseProgram(0);
        }
        }
    }
