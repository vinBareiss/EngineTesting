using OpenTK;

using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;

using System.Linq;

namespace EngineTesting
{
    public struct ParsedObj
    {
        public uint[] Indices;
        public Vector3[] Positions;
        public Vector2[] UvCoords;
        public Vector3[] Normals;

        public ParsedObj(uint[] indices, Vector3[] positions, Vector2[] uvCoords, Vector3[] normals)
        {
            this.Indices = indices;
            this.Positions = positions;
            this.UvCoords = uvCoords;
            this.Normals = normals;
        }
    }

    //small container to hold the data of a mesh until we have it all and can convert to a real mesh
    class ParsedObjMesh
    {
        public List<Vector3> Positions;
        public List<Vector2> UvCoords;
        public List<Vector3> Normals;
        public List<string[]> Faces;

        public ParsedObjMesh()
        {
            this.Positions = new List<Vector3>();
            this.UvCoords = new List<Vector2>();
            this.Normals = new List<Vector3>();
            this.Faces = new List<string[]>();
        }


        public ParsedObj ProcessData()
        {
            Vector3[] outNormals = null;
            Vector2[] outUvCoords = null;
            List<uint> outIndices = new List<uint>();

            bool hasUv = false;
            bool hasNormals = false;



            //does our model even have UVCoords?
            if (this.UvCoords.Count > 0) {
                hasUv = true;
                outUvCoords = new Vector2[this.Positions.Count];
            }
            //same with normals
            if (this.Normals.Count > 0) {
                hasNormals = true;
                outNormals = new Vector3[this.Positions.Count];
            }
            foreach (string[] faceLine in this.Faces) {

                //f 5/1/1 1/2/1 4/3/1
                //lineFragment[1-3] are the faces
                for (int i = 1; i <= 3; i++) {
                    //5/1/1
                    string[] faceData = faceLine[i].Split(new char[] { '/' });

                    //get index of position
                    int vertexIndex = int.Parse(faceData[0]) - 1;
                    if (hasUv) {
                        //get index of uvCoord
                        int uvIndex = int.Parse(faceData[1]) - 1;
                        outUvCoords[vertexIndex] = this.UvCoords[uvIndex];
                    }

                    if (hasNormals) {
                        //get index of normal
                        int normIndex = int.Parse(faceData[2]) - 1;
                        outNormals[vertexIndex] = outNormals[normIndex];
                    }


                    //put all the data in the fitting arrays
                    outIndices.Add((uint)vertexIndex); //where to find the position when drawing
                }
            }
            return new ParsedObj(outIndices.ToArray(), Positions.ToArray(), outUvCoords, outNormals);
        }

        public void Clean()
        {
            this.Positions.Clear();
            this.UvCoords.Clear();
            this.Normals.Clear();
            this.Faces.Clear();
        }
    }



    public static class ObjFile
    {

        public static void LoadFile(string path)
        {
            FileInfo file = new FileInfo(path);
            if (!file.Exists)
                throw new IOException($"Cant find a File at this Path ({path})");
            if (file.Extension != ".obj")
                throw new IOException("This is not an .OBJ file");

            bool lastWasFace = false;
            int l = 0;
            ParsedObjMesh workingMesh = new ParsedObjMesh();

            using (StreamReader sr = new StreamReader(file.OpenRead())) {
                while (!sr.EndOfStream) {
                    l++;
                    string line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        continue;
                    string[] lineFragments = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (lastWasFace && lineFragments[0] != "f") {
                        //we have a new mesh, process the old one and clean up for a new run
                        Console.WriteLine("New Mesh detected, processing old one");
                        Console.WriteLine($"Found {workingMesh.Positions.Count} Vertices,\n" +
                            $" {workingMesh.Faces.Count} Faces");

                        workingMesh.ProcessData();
                        workingMesh.Clean();
                        lastWasFace = false;
                    }

                    float x, y, z;
                    switch (lineFragments[0]) {
                        case "v":
                            //Console.WriteLine($"Vertex : {line}");
                            x = float.Parse(lineFragments[1], CultureInfo.InvariantCulture);
                            y = float.Parse(lineFragments[2], CultureInfo.InvariantCulture);
                            z = float.Parse(lineFragments[3], CultureInfo.InvariantCulture);
                            Vector3 pos = new Vector3(x, y, z);

                            workingMesh.Positions.Add(pos);

                            break;
                        case "vt":
                            //Console.WriteLine($"VertexTexture : {line}");
                            float u = float.Parse(lineFragments[1], CultureInfo.InvariantCulture);
                            float v = float.Parse(lineFragments[2], CultureInfo.InvariantCulture);
                            Vector2 uv = new Vector2(u, v);

                            workingMesh.UvCoords.Add(uv);
                            break;
                        case "vn":
                            //Console.WriteLine($"VertexNormal : {line}");
                            x = float.Parse(lineFragments[1], CultureInfo.InvariantCulture);
                            y = float.Parse(lineFragments[2], CultureInfo.InvariantCulture);
                            z = float.Parse(lineFragments[3], CultureInfo.InvariantCulture);
                            Vector3 normal = new Vector3(x, y, z);

                            workingMesh.Normals.Add(normal);
                            break;
                        case "f":
                            //add this to our temp list of faces, we will have to wait till we are trough the entire file before
                            //we can deal with this
                            lastWasFace = true;
                            workingMesh.Faces.Add(lineFragments);
                            break;
                        default:
                            Console.WriteLine($"Disgarded: {line}");
                            break;
                    }
                }
            }
        }


    }


    public static class MeshLoader
    {
        public static ParsedObj LoadFile(string path)
        {
            List<string[]> faces = new List<string[]>();

            //setup lists
            List<Vector2> uvCoords = null;
            List<Vector3> positions = null;
            List<Vector3> normals = null;

            Vector3[] outNormals = null;
            Vector2[] outUvCoords = null;
            List<uint> outIndices = new List<uint>();

            uvCoords = new List<Vector2>();
            positions = new List<Vector3>();
            normals = new List<Vector3>();


            FileInfo fi = new FileInfo(path);
            if (!fi.Exists) {
                throw new ApplicationException($"Supplyed .obj file does not exist ({path})");
            }
            else {
                using (StreamReader sr = new StreamReader(fi.OpenRead())) {
                    while (!sr.EndOfStream) {
                        string line = sr.ReadLine();
                        if (string.IsNullOrEmpty(line))
                            continue;

                        string[] linefragments = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);



                        float x, y, z;
                        switch (linefragments[0]) {
                            case "v":
                                //Console.WriteLine($"Vertex : {line}");
                                x = float.Parse(linefragments[1], CultureInfo.InvariantCulture);
                                y = float.Parse(linefragments[2], CultureInfo.InvariantCulture);
                                z = float.Parse(linefragments[3], CultureInfo.InvariantCulture);
                                Vector3 pos = new Vector3(x, y, z);

                                positions.Add(pos);

                                break;
                            case "vt":
                                Console.WriteLine($"VertexTexture : {line}");
                                float u = float.Parse(linefragments[1], CultureInfo.InvariantCulture);
                                float v = float.Parse(linefragments[2], CultureInfo.InvariantCulture);
                                Vector2 uv = new Vector2(u, v);

                                uvCoords.Add(uv);
                                break;
                            case "vn":
                                Console.WriteLine($"VertexNormal : {line}");
                                x = float.Parse(linefragments[1], CultureInfo.InvariantCulture);
                                y = float.Parse(linefragments[2], CultureInfo.InvariantCulture);
                                z = float.Parse(linefragments[3], CultureInfo.InvariantCulture);
                                Vector3 normal = new Vector3(x, y, z);

                                normals.Add(normal);
                                break;
                            case "f":

                                //add this to our temp list of faces, we will have to wait till we are trough the entire file before
                                //we can deal with this
                                faces.Add(linefragments);
                                break;
                            default:
                                Console.WriteLine($"Disgarded: {line}");
                                break;
                        }
                        //parsing end, lets process the faces
                        //ok, now we have all positions, uvs, normals in our arrays
                        bool hasUv = false;
                        bool hasNormals = false;



                        //does our model even have UVCoords?
                        if (uvCoords.Count > 0) {
                            hasUv = true;
                            outUvCoords = new Vector2[positions.Count];
                        }
                        //same with normals
                        if (normals.Count > 0) {
                            hasNormals = true;
                            outNormals = new Vector3[positions.Count];
                        }
                        foreach (string[] faceLine in faces) {

                            //f 5/1/1 1/2/1 4/3/1
                            //lineFragment[1-3] are the faces
                            for (int i = 1; i <= 3; i++) {
                                //5/1/1
                                string[] faceData = faceLine[i].Split(new char[] { '/' });

                                //get index of position
                                int vertexIndex = int.Parse(faceData[0]) - 1;
                                if (hasUv) {
                                    //get index of uvCoord
                                    int uvIndex = int.Parse(faceData[1]) - 1;
                                    outUvCoords[vertexIndex] = uvCoords[uvIndex];
                                }

                                if (hasNormals) {
                                    //get index of normal
                                    int normIndex = int.Parse(faceData[2]) - 1;
                                    outNormals[vertexIndex] = outNormals[normIndex];
                                }


                                //put all the data in the fitting arrays
                                outIndices.Add((uint)vertexIndex); //where to find the position when drawing
                            }
                        }
                    }
                }
                ParsedObj result = new ParsedObj(outIndices.ToArray(), positions.ToArray(), outUvCoords, outNormals);
                return result;
            }
        }
    }
}
