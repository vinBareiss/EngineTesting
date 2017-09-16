using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;

namespace EngineTestingNrDuo.src.shading
{
    class ShaderLibary
    {
        #region "Singelton"
        private static ShaderLibary mInstance = null;
        public static ShaderLibary GetInstance()
        {
            if (mInstance == null)
                return mInstance = new ShaderLibary();
            else
                return mInstance;
        }
        #endregion

        XmlDocument mLibary;
        XmlNode structNode;
        XmlNode funcNode;

        public ShaderLibary()
        {
            mLibary = new XmlDocument();
            mLibary.Load("res/shaders/shaderLibary.xml");
            structNode = mLibary.ChildNodes.Item(1).ChildNodes.Item(0);
            funcNode = mLibary.ChildNodes.Item(1).ChildNodes.Item(1);
        }

        public string GetStruct(string name)
        {
            if (structNode[name] == null)
                throw new ApplicationException("Requested import struct doesnt exist", new Exception(name));
            else
                return structNode[name].InnerText;
        }

       public string GetFunction(string name)
        {
            if (funcNode[name] == null)
                throw new ApplicationException("Requested import func doesnt exist", new Exception(name));
            else
                return funcNode[name].InnerText;
        }
    }
}
