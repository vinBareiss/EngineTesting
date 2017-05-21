using System;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace EngineTesting
{
    public class TestGame : OpenTK.GameWindow
    {
        VertexArray vao;
        float[] vertices = new float[]
        {
            -0.5f, -0.5f, 0.0f,
             0.5f, -0.5f, 0.0f,
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

            int vboHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length, vertices, BufferUsageHint.StaticDraw);

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 12, 0);
            
            int iboHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, iboHandle);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length, indices, BufferUsageHint.StaticDraw);

            vao.Unbind();


        }


        protected override void OnRenderFrame(FrameEventArgs e) {
            vao.Bind();
            GL.DrawElements(BeginMode.Triangles, 3, DrawElementsType.UnsignedInt, 0);
            vao.Unbind();
        }


        public string GetFps(double deltaTime) {
            return $"FPS: {1f / deltaTime:0}";
        }
    }
}
