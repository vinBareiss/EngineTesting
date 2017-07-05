using System;

using System.IO;

using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace EngineTesting.src.Engine.Shader
{
    class ShaderProgram : OGLHandle
    {
        //this has to be implemented in this class and cant be done by inheriting the Singelton class due to multiple inhert.
        //not being allowed
        private static ShaderProgram mInstance;
        public static ShaderProgram Instance
        {
            get {
                if (mInstance == null) {
                    return mInstance = new ShaderProgram();
                } else {
                    return mInstance;
                }
            }
        }

        //fields
        protected ShaderLibary ShaderLibary;
        protected Dictionary<string, int> Uniforms;
        
        public ShaderProgram() : base(GL.CreateProgram()) {
            //safeguard against double instanciation
            if (mInstance != null)
                throw new ApplicationException("Double initiation of singelton class");

            //get singelton instance of ShaderLibary
            ShaderLibary = ShaderLibary.Instance;
        }

        protected string LoadFromFile(string path) {
            FileInfo fi = new FileInfo($"res/shaders/{path}");
            if (!fi.Exists)
                throw new ApplicationException("Could not find file");
            using (StreamReader sr = new StreamReader(fi.OpenRead())) {
                return sr.ReadToEnd();
            }
        }

        protected void Create(Shader[] shaders) {
            foreach (Shader shader in shaders)
                GL.AttachShader(this, shader);

            GL.LinkProgram(this);

            foreach (Shader shader in shaders)
                GL.DetachShader(this, shader);
        }

        public void Use() {
            GL.UseProgram(this);
        }
        public void UnUse() {
            GL.UseProgram(0);
        }

        public void AddUniform(string name) {
            int loc = GL.GetUniformLocation(this, name);
            if (loc == 0xFFFFFF)
                throw new ApplicationException("Could not find Unifom");
            Uniforms.Add(name, loc);
        }
        
    }
}
