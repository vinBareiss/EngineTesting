﻿using System;
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


            VertexArray vao = new VertexArray();
            vao.Bind();

            VertexBuffer<float> Vbo = new VertexBuffer<float>();
            Vbo.Bind();
            Vbo.BufferData(vertices);
            Vbo.SetVertexAttribPointer(1, 3, VertexAttribPointerType.Float,
                                       false, 3 * Vbo.ElementSize, 0);

            IndexBuffer ibo = new IndexBuffer();
            ibo.Bind();
            ibo.BufferData(indices);

            vao.Unbind();

            
            GameObject planeObj = new GameObject();
            scenegraph.Root.Children.Add(planeObj);
            RenderInfo planeInfo = new RenderInfo(UnlitShader.GetInstance(), vao, indices.Length);
            planeObj.AddComponent("renderInfo", planeInfo);


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
            Scenegraph.GetInstance().Root.Transform.Model *= Matrix4.CreateRotationZ(t);

            GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            RenderingEngine.GetInstance().Render();
            this.SwapBuffers();
            base.OnRenderFrame(e);
        }
    }
}
