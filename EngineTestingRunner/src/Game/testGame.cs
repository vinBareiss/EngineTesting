using System;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace EngineTesting
{
    public class TestGame : OpenTK.GameWindow
    {
        VertexArray vao;
        ShaderProgram prog;

        float[] vertices = new float[]
        {
            -1.0f, -1.0f, 0.0f,
             1.0f, -1.0f, 0.0f,
             0.0f, 0.0f, 0.0f
        };
        uint[] indices = new uint[]
        {
            0,1,2
        };


        public TestGame()
            : base(640, 420, GraphicsMode.Default, "BaseGameWindow", GameWindowFlags.Default, DisplayDevice.Default, 4, 0, GraphicsContextFlags.ForwardCompatible)
        {
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("OpenGl: " + GL.GetString(StringName.Version));
            Console.WriteLine("GLSL: " + GL.GetString(StringName.ShadingLanguageVersion));
        }
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
        }

        ParsedObj test;
        protected override void OnLoad(EventArgs e)
        {
            
            ObjFile.LoadFile("res/box.obj");

            vao = new VertexArray();

            vao.Bind();

            int vboHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboHandle);
            //GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            GL.BufferData(BufferTarget.ArrayBuffer, test.Positions.Length * Vector3.SizeInBytes, 
                          test.Positions, BufferUsageHint.StaticDraw);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 12, 0);

            int iboHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, iboHandle);
            //GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
            GL.BufferData(BufferTarget.ElementArrayBuffer, test.Indices.Length * sizeof(uint),
                         test.Indices, BufferUsageHint.StaticDraw);

            string[] src = {
                "#version 430 core\n",
                Shader.FromFile("res/shaders/function.frag"),
                Shader.FromFile("res/shaders/test.frag")
            };

            Shader[] shaders = {

                new Shader(Shader.FromFile("res/shaders/basic.vert"), ShaderType.VertexShader),
                new Shader(src, ShaderType.FragmentShader)
            };
            prog = new ShaderProgram(shaders);
            vao.Unbind();


            GL.Enable(EnableCap.DepthTest);
        }


        float t = 0;
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            t += (float)e.Time;

            GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            prog.Use();
            Matrix4 transform = Matrix4.CreateRotationY(t) * Matrix4.CreateRotationZ(20);
            GL.UniformMatrix4(prog.GetUniformLoc("trans"), false, ref transform);

            vao.Bind();
            GL.DrawElements(BeginMode.Triangles, test.Indices.Length, DrawElementsType.UnsignedInt, 0);
            vao.Unbind();

            prog.UnUse();

            this.SwapBuffers();
        }
    }
}
