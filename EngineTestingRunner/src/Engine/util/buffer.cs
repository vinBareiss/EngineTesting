using OpenTK.Graphics.OpenGL4;



namespace EngineTesting
{
    public abstract class Buffer : OGLHandle
    {
        public Buffer(int handle) : base(handle) {
        }

        public abstract void Bind();
        public abstract void Unbind();
    }

    class VertexBuffer : Buffer
    {
        public VertexBuffer() : base(GL.GenBuffer()) {        }
        
        public void BufferData(Vertex[] data) {
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * Vertex.SizeInBytes, data, BufferUsageHint.StaticDraw);
        }

        public override void Bind() {
            GL.BindBuffer(BufferTarget.ArrayBuffer, this);
        }

        public override void Unbind() {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }

    public class VertexArray : Buffer
    {
        public VertexArray() : base(GL.GenVertexArray()) {

        }

        public void SetVertexAttrib(int index, int size, VertexAttribPointerType type, bool normalize, int stride, int offset) {
            
            //TODO: seems wrong
            GL.VertexAttribPointer(index, size, type, normalize, stride * Vertex.SizeInBytes, offset * Vertex.SizeInBytes);
            GL.EnableVertexAttribArray(index);
        }

        public override void Bind() {
            GL.BindVertexArray(this);
        }

        public override void Unbind() {
            GL.BindVertexArray(0);
        }
    }

    public class IndexBuffer : Buffer
    {
        public IndexBuffer() : base(GL.GenBuffer()) { }

        public void BufferData(uint[] data) {
            GL.BufferData(BufferTarget.ElementArrayBuffer, data.Length * sizeof(uint), data, BufferUsageHint.StaticDraw);
        }

        public override void Bind() {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, this);
        }

        public override void Unbind() {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }
    }



}
