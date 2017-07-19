using System;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using EngineTesting.src.Engine.Shader;

namespace EngineTesting
{
    public class TestGame : OpenTK.GameWindow
    {
        #region Setup Stuff

        public TestGame()
            : base(640, 420, GraphicsMode.Default, "BaseGameWindow", GameWindowFlags.Default, DisplayDevice.Default, 4, 0, GraphicsContextFlags.ForwardCompatible)
        {
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("OpenGl: " + GL.GetString(StringName.Version));
            Console.WriteLine("GLSL: " + GL.GetString(StringName.ShadingLanguageVersion));
        }
        protected override void OnResize(EventArgs e)
        {
            if (Width - 2 > Height)
                GL.Viewport(0, 0, Width, Height);
            else
                Console.WriteLine("Invalid aspect ration");
            GL.Viewport(0, 0, Width, Width);
        }

        #endregion

        ParsedObj test;
        VertexArray vao;
        UnlitShader testShader;
        Camera cam;

        #region Hardcoded Vertices

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
        Vector3[] vertices2 = new Vector3[] {
            new Vector3(-1, -1, 0),
            new Vector3(0,0,0),
            new Vector3(1,-1,0)
        };

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            cam = new Camera(this, new Vector3(4, 2, 2), new Vector3(0, 1, 0), 0.1f, 0.1f, 45f);

            test = MeshLoader.LoadFile("res/teapot.obj");
            vao = new VertexArray();
            vao.Bind();


            VertexBuffer<Vector3> posVbo = new VertexBuffer<Vector3>();
            posVbo.Bind();
            posVbo.BufferData(test.Positions);
            posVbo.SetVertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 12, 0);

            VertexBuffer<Vector3> normVbo = new VertexBuffer<Vector3>();
            normVbo.Bind();
            normVbo.BufferData(test.Normals);
            normVbo.SetVertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 12, 0);

            IndexBuffer ibo = new IndexBuffer();
            ibo.Bind();
            ibo.BufferData(test.Indices);

            vao.Unbind();

            testShader = new UnlitShader();


            Console.WriteLine(GL.GetError());
            GL.Enable(EnableCap.DepthTest);
            //GL.CullFace(CullFaceMode.Back);
        }

        float t = 0;
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            t += (float)e.Time;

            Transform trans = new Transform(null);

            GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            testShader.Use();
            Matrix4 model = Matrix4.Identity;
            model *= Matrix4.CreateScale(2f);
            //model *= Matrix4.CreateRotationY(t / 2.3f);
            //model *= Matrix4.CreateRotationZ(t / 5);

            trans.Model = model;
            trans.View = cam.ViewMatrix;
            trans.Pojection = cam.ProjectionMatrix;

            trans.SetUniforms();

            vao.Bind();
            GL.DrawElements(BeginMode.Triangles, test.Indices.Length, DrawElementsType.UnsignedInt, 0);
            vao.Unbind();

            //.UnUse();

            this.SwapBuffers();
        }
    }
}
