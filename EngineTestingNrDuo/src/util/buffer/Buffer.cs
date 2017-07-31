using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineTestingNrDuo.src.util.buffer
{
    abstract class Buffer : OpenGlHandle
    {
        public bool IsBound { get; private set; }

        public Buffer(int handle) : base(handle) { }

        //simple virtual methods to keep track of the status of this Buffer (bound/ unbound)
        //allways call base.bind()/.unbind() in the overridden 
        public virtual void Bind()
        {
            this.IsBound = true;
        }
        public virtual void Unbind()
        {
            this.IsBound = false;
        }
    }
}
