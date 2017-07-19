using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL4;

using EngineTestingNrDuo.src.core.components;
using EngineTestingNrDuo.src.util;


namespace EngineTestingNrDuo.src.core
{
    class RenderingEngine : Singelton<RenderingEngine>
    {
        private List<RenderInfo> mRenderRegistry;

        public RenderingEngine()
        {
            mRenderRegistry = new List<RenderInfo>();
        }

        public void AddRenderInfo(RenderInfo toAdd)
        {
            //TODO: view render void
            mRenderRegistry.Add(toAdd);
        }


        public void Render()
        {
            foreach (RenderInfo rInfo in mRenderRegistry) {
                rInfo.Shader.Use();
                rInfo.VAO.Bind();
                rInfo.Shader.UpdateUniforms(rInfo.Parent);
                GL.DrawElements(BeginMode.Triangles, rInfo.Length, DrawElementsType.UnsignedInt, 0);
                rInfo.VAO.Unbind();
                rInfo.Shader.UnUse(); //do i need that?

                //TODO: Sort the RenderRegistry by shader and vao to minimize state changes
            }
        }
    }
}
