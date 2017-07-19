

using OpenTK.Graphics.OpenGL4;

namespace EngineTestingNrDuo.src.core.buffer
{
    class IndexBuffer : Buffer
    {
        public IndexBuffer() : base(GL.GenBuffer()) { }

        public void BufferData(uint[] data)
        {
            GL.BufferData(BufferTarget.ElementArrayBuffer, data.Length * sizeof(uint), data, BufferUsageHint.StaticDraw);
        }

        public override void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, this);
            base.Bind();
        }

        public override void Unbind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            base.Unbind();
        }
    }
}
