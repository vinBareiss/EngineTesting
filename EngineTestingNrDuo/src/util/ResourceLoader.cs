using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace EngineTestingNrDuo.src.util
{
    static class ResourceLoader
    {
        /// <summary>
        /// Read in a textfile, containing glsl shadercode
        /// </summary>
        /// <param name="path">Path to the file, takes all extentions</param>
        /// <returns></returns>
        public static string LoadShader(string path)
        {
            FileInfo fi = new FileInfo(path);
            if (!fi.Exists)
                throw new ApplicationException("Shader not found");

            using (StreamReader sr = new StreamReader(fi.OpenRead())) {
                return sr.ReadToEnd();
            }
        }
    }
}
