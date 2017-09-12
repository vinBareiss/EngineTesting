using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineTestingNrDuo.src.util
{
    /// <summary>
    /// Enum that maps vertexFormats to binary values, we can perform operations on
    /// Maps directly to the Layout location in the shader
    /// </summary>
    enum VertexFormatFlag : int
    {
        Position = 0, //0b0001,
        UvCoord = 1,// 0b0010,
        Normal = 2,//0b0100,
        Color = 3,//0b1000,
    }
}
