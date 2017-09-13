using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

using System.IO;
using System.Globalization;

namespace EngineTestingNrDuo.src.util
{
    static class ResourceLoader
    {
        /// <summary>
        /// Read in a textfile, containing glsl shadercode
        /// </summary>
        /// <param name="path">Path to the file, takes all extentions</param>
        /// <returns></returns>
        public static string LoadShader(string path)
        {
            FileInfo fi = new FileInfo(path);
            if (!fi.Exists)
                throw new ApplicationException("Shader not found");

            using (StreamReader sr = new StreamReader(fi.OpenRead())) {
                return sr.ReadToEnd();
            }
        }


        /// <summary>
        /// Read in an OBJ file, containing a model with !ONLY ONE! mesh
        /// TODO: mtl. files
        /// TODO: multiple meshes
        /// </summary>
        /// <param name="path">path to the file</param>
        /// <returns><see cref="ParsedObj"/>, with VAO, VBO, IBO</returns>
        public static Mesh LoadFile(string path)
        {
            //check if file is ok
            FileInfo file = new FileInfo(path);
            if (!file.Exists)
                throw new IOException($"Cant find a File at this Path ({path})");
            if (file.Extension != ".obj")
                throw new IOException("This is not an .OBJ file");

            //allocate space for variables

            float[] outputPositions = null;

            bool hasUv = false;
            Vector2[] outUvCoords = null;
            float[] outputUvCoords = null;

            bool hasNormals = false;
            Vector3[] outNormals = null;
            float[] outputNormals = null;


            bool firstFace = true;
            List<uint> outIndices = new List<uint>();



            List<Vector3> positions = new List<Vector3>();
            List<Vector2> uvCoords = new List<Vector2>();
            List<Vector3> normals = new List<Vector3>();

            //read in file line by line
            using (StreamReader sr = new StreamReader(file.OpenRead())) {
                while (!sr.EndOfStream) {

                    //check read in line
                    string line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        continue;
                    string[] lineFragments = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (!firstFace && lineFragments[0] != "f") {
                        throw new NotSupportedException("Parser currently only supports files with 1 mesh inside");
                    }

                    float x, y, z;
                    switch (lineFragments[0]) {
                        case "v":
                            //Console.WriteLine($"Vertex : {line}");
                            x = float.Parse(lineFragments[1], CultureInfo.InvariantCulture);
                            y = float.Parse(lineFragments[2], CultureInfo.InvariantCulture);
                            z = float.Parse(lineFragments[3], CultureInfo.InvariantCulture);
                            Vector3 pos = new Vector3(x, y, z);

                            positions.Add(pos);
                            break;
                        case "vt":
                            //Console.WriteLine($"VertexTexture : {line}");
                            float u = float.Parse(lineFragments[1], CultureInfo.InvariantCulture);
                            float v = float.Parse(lineFragments[2], CultureInfo.InvariantCulture);
                            Vector2 uv = new Vector2(u, v);

                            uvCoords.Add(uv);
                            break;
                        case "vn":
                            //Console.WriteLine($"VertexNormal : {line}");
                            x = float.Parse(lineFragments[1], CultureInfo.InvariantCulture);
                            y = float.Parse(lineFragments[2], CultureInfo.InvariantCulture);
                            z = float.Parse(lineFragments[3], CultureInfo.InvariantCulture);
                            Vector3 normal = new Vector3(x, y, z);

                            normals.Add(normal);
                            break;
                        case "f":
                            //process face
                            if (firstFace) {
                                firstFace = false;

                                outputPositions = new float[positions.Count * 3];

                                //does our model even have UVCoords?
                                if (uvCoords.Count > 0) {
                                    hasUv = true;
                                    outUvCoords = new Vector2[positions.Count];
                                    outputUvCoords = new float[positions.Count * 2];
                                }
                                //same with normals
                                if (normals.Count > 0) {
                                    hasNormals = true;
                                    outNormals = new Vector3[positions.Count];
                                    outputNormals = new float[positions.Count * 3];
                                }
                            }

                            for (int i = 1; i <= 3; i++) {
                                //5/1/1
                                string[] vertexData = lineFragments[i].Split(new char[] { '/' });

                                //get index of position
                                int positionIndex = int.Parse(vertexData[0]) - 1;
                                int adjustedIndex = positionIndex * 3;

                                outputPositions[adjustedIndex] = positions[positionIndex].X;
                                outputPositions[adjustedIndex + 1] = positions[positionIndex].Y;
                                outputPositions[adjustedIndex + 2] = positions[positionIndex].Z;

                                if (hasUv) {
                                    //get index of uvCoord
                                    int uvIndex = int.Parse(vertexData[1]) - 1;
                                    outUvCoords[positionIndex] = uvCoords[uvIndex];

                                    outputUvCoords[adjustedIndex] = uvCoords[uvIndex].X;
                                    outputUvCoords[adjustedIndex + 1] = uvCoords[uvIndex].Y;
                                }

                                if (hasNormals) {
                                    //get index of normal
                                    int normIndex = int.Parse(vertexData[2]) - 1;
                                    outNormals[positionIndex] = normals[normIndex];

                                    outputNormals[adjustedIndex] = normals[normIndex].X;
                                    outputNormals[adjustedIndex + 1] = normals[normIndex].Y;
                                    outputNormals[adjustedIndex + 2] = normals[normIndex].Z;
                                }


                                //put all the data in the fitting arrays
                                outIndices.Add((uint)positionIndex); //where to find the position when drawing
                            }


                            break;
                        default:
                            //Console.WriteLine($"Disgarded: {line}");
                            break;
                    }
                }
            }

            //debug
            
            Mesh m = new Mesh();
            m.AddIndices(outIndices.ToArray());
            m.AddData(VertexFormatFlag.Position, outputPositions);
            if (hasUv)
                m.AddData(VertexFormatFlag.UvCoord, outputUvCoords);
            if (hasNormals)
                m.AddData(VertexFormatFlag.Normal, outputNormals);

            return m;
            //return new ParsedObj(outIndices.ToArray(), positions.ToArray(), outUvCoords, outNormals);
        }
    }

    class Mesh
    {
        private Dictionary<VertexFormatFlag, float[]> mData = new Dictionary<VertexFormatFlag, float[]>();
        public Dictionary<VertexFormatFlag, float[]> Data { get { return mData; } }

        public bool IsIndexed { get; private set; }
        public uint[] Indices { get; private set; }

        int VertexFormat = 0;

        public Mesh()
        {
            mData = new Dictionary<VertexFormatFlag, float[]>();
        }

        public void AddData(VertexFormatFlag dataType, float[] data)
        {
            if (mData.ContainsKey(dataType))
                throw new ApplicationException("Allready have this data");

            mData.Add(dataType, data);
            VertexFormat |= (int)dataType;
        }
        public void AddIndices(uint[] indices)
        {
            IsIndexed = true;
            this.Indices = indices;
        }


    }

    [Obsolete("Use Mesh class", true)]
    public struct ParsedObj
    {
        public uint[] Indices;
        public Vector3[] Positions;
        public Vector2[] UvCoords;
        public Vector3[] Normals;

        public bool hasUv;
        public bool hasNormal;

        public ParsedObj(uint[] indices, Vector3[] positions, Vector2[] uvCoords, Vector3[] normals)
        {
            this.Indices = indices;
            this.Positions = positions;
            this.UvCoords = uvCoords;
            hasUv = !(uvCoords == null);
            this.Normals = normals;
            hasNormal = !(normals == null);
        }

        public int GetVertexFormat()
        {
            int format = 0;
            format |= (int)VertexFormatFlag.Position;
            if (hasNormal)
                format |= (int)VertexFormatFlag.Normal;
            if (hasUv)
                format |= (int)VertexFormatFlag.UvCoord;


            return format;
        }
    }
}
