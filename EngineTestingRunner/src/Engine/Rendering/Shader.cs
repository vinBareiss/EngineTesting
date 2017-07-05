using System;

using System.IO;
using OpenTK.Graphics.OpenGL4;


namespace EngineTesting
{


    public class Shader : OGLHandle
    {
        public static string Version430C = "#version 430 core\n";
        public static string Version430 = "#version 430";
        public static string Version450C = "#version 450 core";
        public static string Version450 = "#version 450";

        public Shader(string src, ShaderType type) : base(GL.CreateShader(type))
        {
            GL.ShaderSource(this, src);
            GL.CompileShader(this);

            Console.WriteLine(type.ToString() + " : " + GL.GetShaderInfoLog(this));
        }

        public Shader(string[] src, ShaderType type) : base(GL.CreateShader(type))
        {
            int[] len = new int[src.Length];
            for (int i = 0; i < src.Length; i++)
                len[i] = src[i].Length;

            GL.ShaderSource(this, src.Length, src, len);
            GL.CompileShader(this);

            Console.WriteLine(GL.GetError());
            Console.WriteLine($"{type.ToString()} : {GL.GetShaderInfoLog(this)}");

        }

        public static string FromFile(string path)
        {
            using (StreamReader sr = new StreamReader(path)) {
                return sr.ReadToEnd();
            }
        }
    }
}
