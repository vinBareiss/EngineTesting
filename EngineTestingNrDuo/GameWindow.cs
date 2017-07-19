using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

using EngineTestingNrDuo.src.core;
using EngineTestingNrDuo.src.shading;
using EngineTestingNrDuo.src.core.buffer;
using EngineTestingNrDuo.src.core.components;

namespace EngineTestingNrDuo
{
    class GameWindow : OpenTK.GameWindow
    {
        /// <summary>
        /// Constructor, setzt TestWerte
        /// </summary>
        public GameWindow() : base(640, 420, GraphicsMode.Default, "BaseGameWindow", GameWindowFlags.Default, DisplayDevice.Default, 4, 0, GraphicsContextFlags.ForwardCompatible)
        {
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("OpenGl: " + GL.GetString(StringName.Version));
            Console.WriteLine("GLSL: " + GL.GetString(StringName.ShadingLanguageVersion));
        }
        float[] vertices = {0.5f, 0.5f, 0.0f,
                   0.5f, -0.5f, 0.0f,
                   -0.5f, 0.5f, 0.0f,
                   -0.5f, -0,5f, 0.0f
            };
        uint[] indices = { 1, 0, 2,
                           2, 3, 1 };

        Scenegraph scenegraph;
        RenderingEngine renderengine;
               

        /// <summary>
        /// Wird beim Start aufgerufen
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            scenegraph = Scenegraph.GetInstance();
            renderengine = RenderingEngine.GetInstance();

            VertexArray vao = new VertexArray();
            vao.Bind();

            VertexBuffer<float> Vbo = new VertexBuffer<float>();
            Vbo.Bind();
            Vbo.BufferData(vertices);
            Vbo.SetVertexAttribPointer(1, 3, VertexAttribPointerType.Float,
                                       false, 12, 0);

            IndexBuffer ibo = new IndexBuffer();
            ibo.Bind();
            ibo.BufferData(indices);

            vao.Unbind();
            


            GameObject planeObj = new GameObject();
            RenderInfo planeInfo = new RenderInfo(UnlitShader.GetInstance(), vao, indices.Length);
            planeObj.AddComponent("renderInfo", planeInfo);
        }

        /// <summary>
        /// Wird für jeden Frame aufgerufen
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            renderengine.Render();

            this.SwapBuffers();
            base.OnRenderFrame(e);
        }
    }
}
