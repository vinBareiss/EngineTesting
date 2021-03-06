﻿using OpenTK;

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

    public static class MeshLoader
    {

        public static ParsedObj LoadFile(string path)
        {
            //check if file is ok
            FileInfo file = new FileInfo(path);
            if (!file.Exists)
                throw new IOException($"Cant find a File at this Path ({path})");
            if (file.Extension != ".obj")
                throw new IOException("This is not an .OBJ file");

            //allocate space for variables
            bool hasUv = false;
            Vector2[] outUvCoords = null;
            bool hasNormals = false;
            Vector3[] outNormals = null;
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
                            }

                            for (int i = 1; i <= 3; i++) {
                                //5/1/1
                                string[] faceData = lineFragments[i].Split(new char[] { '/' });

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
                                    outNormals[vertexIndex] = normals[normIndex];
                                }


                                //put all the data in the fitting arrays
                                outIndices.Add((uint)vertexIndex); //where to find the position when drawing
                            }


                            break;
                        default:
                            //Console.WriteLine($"Disgarded: {line}");
                            break;
                    }
                }
            }
            return new ParsedObj(outIndices.ToArray(), positions.ToArray(), outUvCoords, outNormals);
        }
    }
}
