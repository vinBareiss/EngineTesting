using System;
using System.ComponentModel;
using System.Xml;

namespace EngineTesting.src.Engine.Shader
{
    /// <summary>
    /// Utility Class mirroring the ShaderLib XML file containing definitions for Shader Structs / Functions and VersionDeclatations
    /// </summary>

    class ShaderLibary : Singelton<ShaderLibary>
    {
        private XmlNode shaderLib;

        public enum Version
        {
            [Description("430")]
            v430,
            [Description("430Core")]
            v430c,
            [Description("450")]
            v450,
            [Description("450Core")]
            v450c
        }

        public ShaderLibary() {
            XmlDocument doc = new XmlDocument();
            doc.Load("res/shaders/shaderLib.xml");
            shaderLib = doc.ChildNodes[1];
        }

        public string GetFunctionSource(string name) {
            XmlNode StructRoot = shaderLib.ChildNodes[0];
            return "";
        }

        public string GetStructSource(string name) {
            XmlNode StructRoot = shaderLib.ChildNodes[1];
            if (StructRoot[name] == null) {
                throw new ApplicationException("Could not find struct with such name");
            } else {
                return StructRoot[name].InnerText;
            }
        }

        public string GetVersionText(Version v) {
            //dunno, SO c&p to get the Descriptions out of the Enum, this is kinda weird stuff...
            var type = typeof(Version);
            var memInfo = type.GetMember(v.ToString());
            var attr = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            var desc = ((DescriptionAttribute)attr[0]).Description.ToString();

            //build the version string
            return $"#version {desc}";
        }

    }
}
