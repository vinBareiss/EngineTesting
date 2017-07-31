

using OpenTK.Graphics.OpenGL4;

namespace EngineTestingNrDuo.src.util.buffer
{
    class VertexBuffer<T> : Buffer
    where T : struct
    {
        public int ElementSize { get; private set; }
        public VertexBuffer() : base(GL.GenBuffer())
        {
            ElementSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(T));
        }

        public void BufferData(T[] data, BufferUsageHint hint = BufferUsageHint.StaticDraw)
        {
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * ElementSize, data, hint);
        }

        public void SetVertexAttribPointer(int index, int size, VertexAttribPointerType type, bool normalize, int stride, int offset)
        {
            GL.EnableVertexAttribArray(index);
            GL.VertexAttribPointer(index, size, type, normalize, stride, offset);

        }

        public override void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, this);
            base.Bind();
        }

        public override void Unbind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            base.Unbind();
        }
    }
}
