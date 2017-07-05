using System;
using System.Xml;
using OpenTK.Graphics.OpenGL4;

namespace EngineTesting.src.Engine.Shader
{
    class UnlitShader : ShaderProgram
    {
        public UnlitShader() {
            string[] vertexSrc = {
                ShaderLibary.GetVersionText(ShaderLibary.Version.v430),
                ShaderLibary.GetStructSource("Transform"),
                LoadFromFile("basicVert.vert")
            };
            Shader vertexShader = new Shader(ShaderType.VertexShader, vertexSrc);

            string[] fragmentSrc = {
               ShaderLibary.GetVersionText(ShaderLibary.Version.v430),
               LoadFromFile("basicFrag.frag")
            };
            Shader fragmentShader = new Shader(ShaderType.FragmentShader, fragmentSrc);

            Shader[] shaders = { vertexShader, fragmentShader };

            
            Create(shaders);

        }
    }
}
