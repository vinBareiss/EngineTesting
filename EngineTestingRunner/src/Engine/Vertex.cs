using OpenTK;

namespace EngineTesting
{
    public struct Vertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 UV;

        public static int SizeInBytes = (3 + 3 + 2) * 4;

        public Vertex(Vector3 pos, Vector2 uv, Vector3 norm) {
            this.Position = pos;
            this.Normal = norm;
            this.UV = uv;          
        }
    }
}
