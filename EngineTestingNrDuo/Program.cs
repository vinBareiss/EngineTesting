using System;
using EngineTestingNrDuo.src.util;
using OpenTK;

namespace EngineTestingNrDuo
{
    class Program
    {
        static void Main(string[] args)
        {
            new GameWindow().Run(60);
            /*Mesh test = ResourceLoader.LoadFile("res/models/box.obj");
            for (int i = 0; i < test.Indices.Length; i++) {
                uint index = test.Indices[i];
                Vector3 pos = new Vector3(test.Data[VertexFormatFlag.Position][index], test.Data[VertexFormatFlag.Position][index + 1], test.Data[VertexFormatFlag.Position][index + 2]);
                Vector3 norm = new Vector3(test.Data[VertexFormatFlag.Normal][index], test.Data[VertexFormatFlag.Normal][index + 1], test.Data[VertexFormatFlag.Normal][index + 2]);
                Console.WriteLine($"i: {index}   p:{pos}    n:{norm}");
                if((i+1)%3==0)
                    Console.WriteLine();
            }
           
            Console.Read();*/
        }
    }
}
