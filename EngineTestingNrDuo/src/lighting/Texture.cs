using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

using OpenTK.Graphics.OpenGL4;

namespace EngineTestingNrDuo.src.util
{
    class Texture : OpenGlHandle
    {
        static List<int> texUnitAtlas = new List<int>();

        public Texture(string path) : base(GL.GenTexture())
        {
            //get image data
            Image img = Image.FromFile(path);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] data = ms.ToArray();
            ms.Dispose();
            img.Dispose();
        
            GL.BindTexture(TextureTarget.Texture2D, this);


        }

       void Bind(int texUnit)
        {
            if (texUnit > 31) //TODO: find the actuall max_tex_unit
                throw new System.ArgumentOutOfRangeException("max 31 texUnit");
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, this);
        }
        void Unbind()
        {
            GL.BindTexture(TextureTarget.Texture2D,0);
        }
    }
}
