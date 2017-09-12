using System;
using EngineTestingNrDuo.src.util;

namespace EngineTestingNrDuo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Mesh test = ResourceLoader.LoadFile("res/models/box.obj");
            new GameWindow().Run(60);
        }
    }
}
