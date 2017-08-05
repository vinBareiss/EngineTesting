using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineTestingNrDuo.src.util
{
    /// <summary>
    /// Enum that maps vertexFormats to binary values, we can perform operations on
    /// </summary>
    enum VertexFormatFlag : int
    {
        Position = 0b0001,
        UvCoord = 0b0010,
        Normal = 0b0100,
        Color = 0b1000
    }
}
