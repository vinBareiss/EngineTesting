namespace EngineTesting
{
    public abstract class OGLHandle
    {
        public int Handle { get; private set; }
       

        public OGLHandle(int handle) {
            this.Handle = handle;
        }

        public static implicit operator int(OGLHandle h) {
            return h.Handle;
        }
    }
}
