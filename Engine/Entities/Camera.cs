using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DragonEngine.Entities
{
    public class Camera : BaseObject
    {
        #region Properties

        private Rectangle mViewArea;
        private Rectangle mViewport;
        private Rectangle mScreenViewport;
        private Matrix mViewportTransform;
        private Matrix mScreenScale;
        private Matrix mScreenTransform;

        public Matrix ViewportTransform { get { return mViewportTransform; } }
        public Matrix ScreenTransform { get { return mScreenTransform; } }

        #endregion

        #region Constructor

        public Camera()
        {
        }

        public Camera(Rectangle pViewArea)
        {
            mViewArea = pViewArea;
        }

        #endregion

        #region Methoden

        public virtual void Initialize()
        {
            mViewport = new Rectangle(0, 0, EngineSettings.VirtualResX, EngineSettings.VirtualResY);
            mScreenViewport = new Rectangle(0, 0, EngineSettings.ScreenResX, EngineSettings.ScreenResY);
            mScreenScale = Matrix.CreateScale((float)mScreenViewport.Width / EngineSettings.VirtualResX, (float)mScreenViewport.Height / EngineSettings.VirtualResY, 1);
            mScreenTransform = mScreenScale * Matrix.CreateTranslation(mScreenViewport.X, mScreenViewport.Y, 0);
        }

        public override void Update()
        {
            UpdateViewportTransformation();
        }

        public void Move(int pDeltaX, int pDeltaY)
        {
            mViewport.X += pDeltaX;
            mViewport.Y += pDeltaY;
            ForceInViewArea();
        }

        public void Move(Vector2 pDelta)
        {
            mViewport.X += (int)pDelta.X;
            mViewport.Y += (int)pDelta.Y;
            ForceInViewArea();
        }

        public void FocusOn(Rectangle pFocus)
        {
            if (mViewport.Left < pFocus.Left) //Scrolling nach links
            {
                mViewport.X = pFocus.Left;
            }
            else if (mViewport.Right > pFocus.Right) //Scrolling nach rechts
            {
                mViewport.X = pFocus.Right - mViewport.Width;
            }
            if (mViewport.Top < pFocus.Top) //Scrolling nach oben
            {
                mViewport.Y = pFocus.Top;
            }
            else if (mViewport.Bottom > pFocus.Bottom) //Scrolling nach unten
            {
                mViewport.Y = pFocus.Bottom - mViewport.Height;
            }
            ForceInViewArea();
        }

        public void ForceInViewArea()
        {
            if (mViewport.Left < mViewArea.Left) //Linker Rand
            {
                mViewport.X = mViewArea.Left;
            }
            else if (mViewport.Right > mViewArea.Right) //Rechter Rand
            {
                mViewport.X = mViewArea.Right - mViewport.Width;
            }
            if (mViewport.Top < mViewArea.Top) //Oberer Rand
            {
                mViewport.Y = mViewArea.Top;
            }
            else if (mViewport.Bottom > mViewArea.Bottom) //Unterer Rand
            {
                mViewport.Y = mViewArea.Bottom - mViewport.Height;
            }
        }

        public void UpdateViewportTransformation()
        {
            mViewportTransform = Matrix.CreateTranslation(-mViewport.X, -mViewport.Y, 0);
        }

        public void InitializeBlackBandTransformation()
        {
            if (mScreenViewport.X < mScreenViewport.Y) //Balken oben/unten
            {
                mScreenViewport.Width = (int)EngineSettings.ScreenResX;
                mScreenViewport.Height = (int)(EngineSettings.ScreenResX / EngineSettings.VirtualResX * EngineSettings.VirtualResY);
            }
            else //Balken links/rechts
            {
                mScreenViewport.Height = (int)EngineSettings.ScreenResY;
                mScreenViewport.Width = (int)(EngineSettings.ScreenResY / EngineSettings.VirtualResY * EngineSettings.VirtualResX);
            }
            mScreenViewport.X = (EngineSettings.ScreenResX - (int)mScreenViewport.Width) / 2;
            mScreenViewport.Y = (EngineSettings.ScreenResY - (int)mScreenViewport.Height) / 2;

            mScreenScale = Matrix.CreateScale((float)mScreenViewport.Width / EngineSettings.VirtualResX, (float)mScreenViewport.Height / EngineSettings.VirtualResY, 1);
            mScreenTransform = mScreenScale * Matrix.CreateTranslation(mScreenViewport.X, mScreenViewport.Y, 0);
            //mViewportTransform = Matrix.CreateTranslation(-mViewport.X, -mViewport.Y, 0);
        }

        #endregion

    }
}
