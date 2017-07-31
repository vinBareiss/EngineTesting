using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EngineTestingNrDuo.src.util;
using EngineTestingNrDuo.src.util.buffer;
using OpenTK;

namespace EngineTestingNrDuo.res.models
{
    class Box : Model
    {
        #region Singelton
        static Box mInstance;
        public static Box GetInstance()
        {
            if (mInstance == null)
                return mInstance = new Box();
            else
                return mInstance;
        }
        #endregion


        public Box()
        {
            //this is the first time we want this Model, load it from its OBJ file
            ParsedObj data =ResourceLoader.LoadFile("res/models/Box.obj");
            VAO = new VertexArray();
            VertexBuffer<Vector3> VBO = new VertexBuffer<Vector3>();
            IBO = new IndexBuffer();

            VAO.Bind();
            VBO.Bind();
            VBO.BufferData(data.Positions);
            VBO.SetVertexAttribPointer()

        }
    }
}
