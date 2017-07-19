using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EngineTestingNrDuo.src.util
{
    /// <summary>
    /// Class to inherit from that enables implicit conversion from Derived type to its handle
    /// </summary>
    abstract class OpenGlHandle
    {
        //field for saving handle of derived type
        int mHandle = -1;
        /// <summary>
        /// OpenGl handle of this Object
        /// </summary>
        public int Handle { get { return mHandle; } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="handle"></param>
        public OpenGlHandle(int handle)
        {
            mHandle = handle;
        }

        /// <summary>
        /// Implicit cast of derived type to its handle (int)
        /// </summary>
        /// <param name="h"></param>
        public static implicit operator int(OpenGlHandle h)
        {
            return h.mHandle;
        }
    }
}
