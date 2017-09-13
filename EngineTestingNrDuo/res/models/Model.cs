using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using OpenTK;
using EngineTestingNrDuo.src.util;
using EngineTestingNrDuo.src.util.buffer;
using EngineTestingNrDuo.src.core.components;
using EngineTestingNrDuo.src.shading;
using EngineTestingNrDuo.src.core;


namespace EngineTestingNrDuo.res.models
{
    /// <summary>
    /// Class for all Models to inherit, contains fields for VAO, IBO
    /// Acutall data filling / retriving happens in derived classes
    /// All derived classes implement singelton pattern
    /// </summary>
    /// TODO: Implement singelton here?
    abstract class Model
    {
        protected Mesh mData;


        public GameObject GetGameObject(ShaderProgram shader)
            {
            VertexArray vao = new VertexArray();
            vao.Bind();

            IndexBuffer ibo = new IndexBuffer();
            ibo.Bind();
            ibo.BufferData(mData.Indices);

            //for every flag in VertexFormatFlat
            foreach (VertexFormatFlag f in shader.VertexFormat) {
                

                if (!mData.Data.ContainsKey(f))
                    throw new ApplicationException("The data requested by the shader is not supplied by the mesh");
                VertexBuffer<float> vbo = new VertexBuffer<float>();
                vbo.Bind();
                vbo.BufferData(mData.Data[f]);

                //set vertexattribpointer
                switch (f) {
                    case VertexFormatFlag.Position:
                        vbo.SetVertexAttribPointer(0, 3, OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, false, 3 * vbo.ElementSize, 0);
                        break;
                    case VertexFormatFlag.UvCoord:
                        vbo.SetVertexAttribPointer(1, 2, OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, false, 2 * vbo.ElementSize, 0);
                        break;
                    case VertexFormatFlag.Normal:
                        vbo.SetVertexAttribPointer(2, 3, OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, false, 3 * vbo.ElementSize, 0);
                        break;
                    case VertexFormatFlag.Color:
                        throw new NotImplementedException();
                    default:
                        throw new ApplicationException("WTF, how did this happen?");
                }

                vbo.Unbind();
            }

            vao.Unbind();

            GameObject gameObject = new GameObject();
            RenderInfo renderInfo = new RenderInfo(shader, vao, 18);
            gameObject.AddComponent("renderInfo",renderInfo);
            return gameObject;
        }
    }
}
