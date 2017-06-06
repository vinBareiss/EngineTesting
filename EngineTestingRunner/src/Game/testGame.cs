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

        Vector3[] vertices2 = new Vector3[] {
            new Vector3(-1, -1, 0),
            new Vector3(0,0,0),
            new Vector3(1,-1,0)
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

            test = MeshLoader.LoadFile("res/teddy.obj");
            vao = new VertexArray();
            vao.Bind();


            VertexBuffer<Vector3> posVbo = new VertexBuffer<Vector3>();
            posVbo.Bind();
            posVbo.BufferData(test.Positions);
            posVbo.SetVertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 12, 0);

            /* VertexBuffer<Vector3> normVbo = new VertexBuffer<Vector3>();
             normVbo.Bind();
             normVbo.BufferData(test.Normals);
             normVbo.SetVertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 12, 0);
             */

            IndexBuffer ibo = new IndexBuffer();
            ibo.Bind();
            ibo.BufferData(test.Indices);




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

            Console.WriteLine(GL.GetError());
            GL.Enable(EnableCap.DepthTest);
            //GL.CullFace(CullFaceMode.Back);
        }


        float t = 0;

        Vector3 camPos = new Vector3(0, 1, 2);
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            t += (float)e.Time;

            GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            prog.Use();
            Matrix4 model = Matrix4.Identity;
            model *= Matrix4.CreateScale(0.1f);
            model *= Matrix4.CreateRotationY(t / 2.3f);
            //model *= Matrix4.CreateRotationZ(t / 5);

            Matrix4 transform = Matrix4.Identity;
            transform *= model; //model
            transform *= Matrix4.LookAt(camPos, new Vector3(0), new Vector3(0, 1, 0)); //view
            transform *= Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), Width / Height, 0.1f, 100.0f);

            GL.UniformMatrix4(prog.GetUniformLoc("trans"), false, ref transform);

            vao.Bind();
            GL.DrawElements(BeginMode.Triangles, test.Indices.Length, DrawElementsType.UnsignedInt, 0);
            vao.Unbind();

            prog.UnUse();

            this.SwapBuffers();
        }

        float camSpeed = 0.5f;
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            float i = camPos.X;
            Console.WriteLine(camPos);
            switch (e.KeyChar) {
                case 'w':
                    camPos.Z -= camSpeed;
                    break;
                case 's':
                    camPos.Z += camSpeed;
                    break;
                case 'a':
                    camPos.X -= camSpeed;
                    break;
                case 'd':
                    camPos.X += camSpeed;
                    break;



                default:
                    Console.WriteLine(e.KeyChar);
                    break;
            }
        }
    }
}
