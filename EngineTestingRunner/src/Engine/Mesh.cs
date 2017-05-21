using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL4;


namespace EngineTesting
{
    class Mesh
    {
        VertexArray vao;

        int elementCount;

        public Mesh(Vertex[] vertices, uint[] indices) {
            elementCount = indices.Length;

            vao = new VertexArray();
            VertexBuffer vbo = new VertexBuffer();
            IndexBuffer ibo = new IndexBuffer();

            vao.Bind();
            vbo.Bind();
            vbo.BufferData(vertices);
            vao.SetVertexAttrib(0, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, 0); //vec3 position
            vao.SetVertexAttrib(1, 2, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, 12); //vec2 uv
            vao.SetVertexAttrib(2, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, 20);  //vec3 normal

            ibo.Bind();
            ibo.BufferData(indices);

            vao.Unbind();
        }


        public void Draw() {
            vao.Bind();

            GL.DrawElements(BeginMode.Triangles, elementCount, DrawElementsType.UnsignedInt, 0);

            vao.Unbind();
        }
    }
}
