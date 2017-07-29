using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Input;

namespace EngineTestingNrDuo.src.core
{
    class Camera
    {
        Vector3 mPosition, mFront, mUp;
        float mLookSpeed, mMoveSpeed, mPitch, mYaw, mFov, mAspect;

        private Matrix4 mViewMatrix;
        public Matrix4 ViewMatrix
        {
            get { return mViewMatrix; }
        }

        private Matrix4 mProjectionMatrix;
        public Matrix4 ProjectionMatrix
        {
            get { return mProjectionMatrix; }
        }

        bool[] mKeys;

        public Camera(GameWindow w, Vector3 startPos, Vector3 startLook, float moveSpeed, float lookSpeed, float fov)
        {
            mMoveSpeed = moveSpeed;
            mLookSpeed = lookSpeed;
            mFov = fov;

            mPosition = startPos;
            mUp = new Vector3(0, 1, 0);
            LookAt(startLook);
            //make cam look at starpos

            mViewMatrix = Matrix4.LookAt(mPosition, mPosition + mFront, mUp);
            mFov = fov;
            mAspect = w.Width / w.Height;
            mProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(mFov), mAspect, 0.1f, 100.0f);

            //add eventhandlers for the given window (thx for that openTk)
            w.KeyUp += KeyUp;
            w.KeyDown += KeyDown;

            w.MouseMove += MouseMove;
            w.MouseWheel += MouseWheelChange;

            w.UpdateFrame += TargetUpdateFrame;

            mKeys = new bool[1024];
        }

        bool debug_printInfo = true;
        private void TargetUpdateFrame(object sender, FrameEventArgs e)
        {
            if (mKeys[(int)Key.E] == true && debug_printInfo) {
                //print debug info
                debug_printInfo = false;
                Console.WriteLine("P " + mPosition);
                Console.WriteLine("U " + mUp);
                Console.WriteLine("F " + mFront);
                Console.WriteLine();
            } else {
                debug_printInfo = true;
            }

            if (mKeys[(int)Key.W] == true)
                mPosition += mFront * mMoveSpeed;
            if (mKeys[(int)Key.S] == true)
                mPosition -= mFront * mMoveSpeed;
            if (mKeys[(int)Key.A] == true)
                mPosition -= Vector3.Normalize(Vector3.Cross(mFront, mUp)) * mMoveSpeed;
            if (mKeys[(int)Key.D] == true)
                mPosition += Vector3.Normalize(Vector3.Cross(mFront, mUp)) * mMoveSpeed;
            if (mKeys[(int)Key.F] == true)
                mPosition.Y += mMoveSpeed;
            if (mKeys[(int)Key.C] == true)
                mPosition.Y -= mMoveSpeed;

            mViewMatrix = Matrix4.LookAt(mPosition, mPosition + mFront, mUp);


        }

        private void MouseWheelChange(object sender, MouseWheelEventArgs e)
        {
            mFov += e.Delta;
            mProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(mFov), mAspect, 0.1f, 100.0f);

        }
        private void MouseMove(object sender, MouseMoveEventArgs e)
        {
            //only move the camera if mouse1 is pressed
            if (!e.Mouse.IsAnyButtonDown)
                return;
            else {
                mYaw -= e.XDelta * mLookSpeed;
                mPitch += e.YDelta * mLookSpeed;

                mPitch %= 360;
                mYaw %= 360;

                if (mPitch > 89.0f)
                    mPitch = 89.0f;
                if (mPitch < -89.0f)
                    mPitch = -89.0f;

                Vector3 front = new Vector3() {
                    X = (float)Math.Cos(MathHelper.DegreesToRadians(mPitch)) * (float)Math.Cos(MathHelper.DegreesToRadians(mYaw)),
                    Y = (float)Math.Sin(MathHelper.DegreesToRadians(mPitch)),
                    Z = (float)Math.Cos(MathHelper.DegreesToRadians(mPitch)) * (float)Math.Sin(MathHelper.DegreesToRadians(mYaw))
                };

                mFront = Vector3.Normalize(front);
            }


        }

        private void KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            mKeys[(int)e.Key] = true;
        }
        private void KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            mKeys[(int)e.Key] = false;
        }

        private void LookAt(Vector3 startLook)
        {
            throw new NotImplementedException();
        }
    }
}
