

using OpenTK.Graphics.OpenGL4;

namespace EngineTestingNrDuo.src.util.buffer
{
    class VertexArray : Buffer
    {
        public VertexArray() : base(GL.GenVertexArray()) { }

        public override void Bind()
        {
            GL.BindVertexArray(this);
            base.Bind();
        }

        public override void Unbind()
        {
            GL.BindVertexArray(0);
            base.Unbind();
        }
    }
}
