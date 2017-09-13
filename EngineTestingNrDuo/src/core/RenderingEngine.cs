using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL4;

using EngineTestingNrDuo.src.core.components;
using EngineTestingNrDuo.src.util;
using EngineTestingNrDuo.src.shading;

namespace EngineTestingNrDuo.src.core
{
    class RenderingEngine
    {
        #region "Singelton"
        private static RenderingEngine mInstance = null;
        public static RenderingEngine GetInstance()
        {
            if (mInstance == null)
                return mInstance = new RenderingEngine();
            else
                return mInstance;
        }
        #endregion

        private Dictionary<ShaderProgram, List<RenderInfo>> mRenderRegistry;

        public RenderingEngine()
        {
            mRenderRegistry = new Dictionary<ShaderProgram, List<RenderInfo>>();
        }

        public void AddRenderInfo(RenderInfo toAdd)
        {
            //1. check if we allready have an entry with this shader
            if (mRenderRegistry.ContainsKey(toAdd.Shader)) {
                //yes, just add this renderInfo to its batch
                mRenderRegistry[toAdd.Shader].Add(toAdd);
            } else {
                //no, create new batch, and add renderinfo to it
                mRenderRegistry.Add(toAdd.Shader, new List<RenderInfo>() { toAdd });
            }
        }

        public void Render()
        {
            //for each shaderbatch
            //key = shader
            //value =   list<renderInfo>
            foreach (KeyValuePair<ShaderProgram, List<RenderInfo>> batch in mRenderRegistry) {
                //use this shader
                ShaderProgram shader = batch.Key;
                List<RenderInfo> renderInfos = batch.Value;
                shader.Use();
                //for all renderinfos, belonging to this shader
                foreach (RenderInfo renderInfo in renderInfos) {
                    //TODO: batch by vao (big change)
                    renderInfo.VAO.Bind(); //bind the vao
                    shader.UpdateUniforms(renderInfo.Parent); //apply transforms, etc
                    GL.DrawElements(renderInfo.Mode, renderInfo.Length, DrawElementsType.UnsignedInt, 0);
                    renderInfo.VAO.Unbind();
                }
                shader.UnUse();
            }
        }
    }
}
