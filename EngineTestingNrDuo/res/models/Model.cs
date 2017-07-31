using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EngineTestingNrDuo.src.util.buffer;

namespace EngineTestingNrDuo.res.models
{
    /// <summary>
    /// Class for all Models to inherit, contains fields for VAO, IBO
    /// Acutall data filling / retriving happens in derived classes
    /// All derived classes implement singelton pattern
    /// </summary>
    /// TODO: Implement singelton here?
    abstract class Model
    {
        protected VertexArray VAO;
        protected IndexBuffer IBO;
               
    }
}
