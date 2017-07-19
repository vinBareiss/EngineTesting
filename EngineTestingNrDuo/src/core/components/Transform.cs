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
        public Transform(GameObject parent) : base(parent)
        {
            mModel = Matrix4.Identity;
        }

        /// <summary>
        /// Adds two Transforms together, keeps parent of the first one
        /// </summary>
        /// <param name="org"></param>
        /// <param name="toAdd"></param>
        /// <returns></returns>
        public static Transform operator +(Transform org, Transform toAdd)
        {
            Transform res = new Transform(org.Parent) {
                mModel = org.mModel * toAdd.mModel
            };
            return res;
        }

    }
}
