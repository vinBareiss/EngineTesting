using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace EngineTesting
{
    public class Transform
    {
        public Matrix4 Model
        {
            get {
                return mModel;
            }
            set {
                mModel = value;
            }
        }
        public Matrix4 View
        {
            get { return mView; }
            set { mView = value; }
        }
        public Matrix4 Pojection
        {
            get { return mProjection; }
            set { mProjection = value; }
        }


        Matrix4 mModel, mView, mProjection;

        ShaderProgram mProgramm;

        public Transform(ShaderProgram prog)
        {
            mProgramm = prog;
            mModel = mView = mProjection = Matrix4.Identity;
        }

        public void SetUniforms()
        {
            mProgramm.SetUniformMat4("transform.view", mView);
            mProgramm.SetUniformMat4("transform.model", mModel);
            mProgramm.SetUniformMat4("transform.projection", mProjection);
        }
    }
}
