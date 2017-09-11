using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace EngineTestingNrDuo.src.core.components
{
    class Transform : Component
    {
        Matrix4 mModel;
        public Matrix4 Model
        {
            get { return mModel; }
            set { mModel = value; }
        }


        /// <summary>
        /// Constructor, start at (0,0,0), rotation (0), scale (1)
        /// </summary>
        public Transform()
        {
            mModel = Matrix4.Identity;
        }

        /// <summary>
        /// This adds the supplied Vector to the current position
        /// </summary>
        /// <param name="v">In which direction and how far we are moving</param>
        void Translate(Vector3 v)
        {
            mModel *= Matrix4.CreateTranslation(v);
        }

        /// <summary>
        /// Adds the supplied rotation to the model
        /// </summary>
        /// <param name="f">Rotation in degree</param>
        void RotateX(float f)
        {
            throw new NotImplementedException();
        }

        //float scale
        void Scale(float f)
        {
            throw new NotImplementedException();
        }




        /// <summary>
        /// Adds two Transforms together, keeps parent of the first one
        /// </summary>
        /// <param name="org"></param>
        /// <param name="toAdd"></param>
        /// <returns></returns>
        public static Transform operator +(Transform org, Transform add)
        {
            Matrix4 mat = org.mModel * add.mModel;
            return new Transform() {
                mModel = mat
            };
        }
    }
}
