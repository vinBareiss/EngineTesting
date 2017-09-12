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
using EngineTestingNrDuo.src.util.buffer;
using EngineTestingNrDuo.src.core.components;
using EngineTestingNrDuo.res.models;


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
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
        }
        float[] vertices = {
           -0.5f, 0.0f, 0.0f,
            0.5f, 0.0f, 0.0f,
            0.0f, 0.5f, 0.0f,
            0.0f,-0.5f, 0,0f
        };
        uint[] indices = { 0,1,2,
                           0,1,3};

        CoreEngine coreEngine;
        Scenegraph scenegraph;
        RenderingEngine renderingEngine;


        /// <summary>
        /// Wird beim Start aufgerufen
        /// </summary>
        /// <param name="e">asd</param>
        protected override void OnLoad(EventArgs e)
        {
            coreEngine = CoreEngine.GetInstance();
            scenegraph = Scenegraph.GetInstance();
            renderingEngine = RenderingEngine.GetInstance();
  

            GameObject boxTest = Box.GetInstance().GetGameObject(NormalViewShader.GetInstance());
           
            scenegraph.Root.AddChild(boxTest);

            Camera.GetInstance().LookAt(new Vector3(0));
            coreEngine.Start(this);
        }

        /// <summary>
        /// Wird für jeden Frame aufgerufen
        /// </summary>
        /// <param name="e"></param>
        float t;
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            t += (float)e.Time;
            //test
            scenegraph.Root.Transform.Model *= Matrix4.CreateRotationZ((float)Math.Sin(t) / 1000);

            GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            renderingEngine.Render();
            Console.WriteLine(GL.GetError());
            this.SwapBuffers();
            base.OnRenderFrame(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            coreEngine.Update();
        }
    }
}
