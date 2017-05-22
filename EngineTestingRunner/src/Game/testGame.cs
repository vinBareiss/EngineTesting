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
            : base(1280, 720, GraphicsMode.Default, "BaseGameWindow", GameWindowFlags.Default, DisplayDevice.Default, 4, 0, GraphicsContextFlags.ForwardCompatible) {
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("OpenGl: " + GL.GetString(StringName.Version));
            Console.WriteLine("GLSL: " + GL.GetString(StringName.ShadingLanguageVersion));
        }
        protected override void OnResize(EventArgs e) {
            GL.Viewport(0, 0, Width, Height);
        }

        
        protected override void OnLoad(EventArgs e) {
            vao = new VertexArray();

            vao.Bind();

            int vboHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 12, 0);
            
            int iboHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, iboHandle);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            prog = new ShaderProgram(Shader.FromFile("res/shaders/basic.vert"), Shader.FromFile("res/shaders/test.frag"));

            vao.Unbind();
        }


        protected override void OnRenderFrame(FrameEventArgs e) {
            GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            prog.Use();
            Console.WriteLine(GL.GetAttribLocation(prog, "pos"));            
            vao.Bind();
            GL.DrawElements(BeginMode.Triangles, 3, DrawElementsType.UnsignedInt, 0);
            vao.Unbind();


            this.SwapBuffers();
        }


        public string GetFps(double deltaTime) {
            return $"FPS: {1f / deltaTime:0}";
        }
    }
}
