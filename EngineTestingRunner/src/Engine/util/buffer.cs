using OpenTK.Graphics.OpenGL4;



namespace EngineTesting
{
    public abstract class Buffer : OGLHandle
    {
        public bool IsBound { get; private set; }

        public Buffer(int handle) : base(handle)
        {
        }

        //simple virtual methods to keep track of the status of this Buffer (bound/ unbound)
        //allways call base.bind()/.unbind() in the overridden 
        public virtual void Bind()
        {
            this.IsBound = true;
        }
        public virtual void Unbind()
        {
            this.IsBound = false;
        }
    }

    class VertexBuffer<T> : Buffer
    where T : struct
    {
        public int ElementSize { get; private set; }
        public VertexBuffer() : base(GL.GenBuffer())
        {
            ElementSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(T));
        }

        public void BufferData(T[] data)
        {
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * ElementSize, data, BufferUsageHint.StaticDraw);
        }

        public void SetVertexAttribPointer(int index, int size, VertexAttribPointerType type, bool normalize, int stride, int offset)
        {
            GL.EnableVertexAttribArray(index);
            GL.VertexAttribPointer(index, size, type, normalize, stride , offset);

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

    public class VertexArray : Buffer
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

    public class IndexBuffer : Buffer
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
