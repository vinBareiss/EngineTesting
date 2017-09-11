

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

        /// <summary>
        /// Buffers the content of a single array on the gpu
        /// </summary>
        /// <param name="data"></param>
        /// <param name="hint"></param>
        public void BufferData(T[] data, BufferUsageHint hint = BufferUsageHint.StaticDraw)
        {
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * ElementSize, data, hint);
        }

        /// <summary>
        /// Buffers the content of multiple arrays into memory in an intereaved format
        /// </summary>
        /// <param name="data"></param>
        /// <param name="hint"></param>
        public void BufferDataInterleaved(T[][] data, BufferUsageHint hint = BufferUsageHint.StaticDraw)
        {
            //check if the length of all arrays is uniform
            int l = data[0].Length;
            for (int i = 1; i < data.Length; i++) {
                if (data[i].Length != l)
                    throw new System.ApplicationException("To create an interleaved vbo, all supplied buffers have to be the same length");
            }
            //create the buffer
            T[] interleaved = new T[data.Length * l];
            for (int i = 0; i < l; i++) {
                for (int j = 0; j < data.Length; j++) {
                    interleaved[i * l + j] = data[i][j];
                }
            }

            //use the existing function to actually ceate the vbo
            BufferData(interleaved, hint);
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
