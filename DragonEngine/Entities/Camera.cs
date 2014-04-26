﻿/**************************************************************
 * (c) Jens Richter 2014
 *************************************************************/
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

        #region Getter & Setter

        public Matrix ViewportTransform { get { return mViewportTransform; } }
        public Matrix ScreenTransform { get { return mScreenTransform; } }
        public Vector2 Position { get { return new Vector2(mViewport.X, mViewport.Y); } }
        public int PositionX { set { mViewport.X = value; } get { return (int)mViewport.X; } }
        public int PositionY { set { mViewport.Y = value; } get { return (int)mViewport.Y; } }
        public Rectangle ViewArea { get { return mViewArea; } set { mViewArea = value; } }
        public int Width { get { return mViewport.Width; } }
        public int Height { get { return mViewport.Height; } }

        #endregion

        #endregion

        #region Constructor

        public Camera(Rectangle pViewArea)
        {
            mViewArea = pViewArea;
            mViewport = new Rectangle(0, 0, EngineSettings.VirtualResX, EngineSettings.VirtualResY);
            mScreenViewport = new Rectangle(0, 0, EngineSettings.ScreenResX, EngineSettings.ScreenResY);
            mScreenScale = Matrix.CreateScale((float)mScreenViewport.Width / EngineSettings.VirtualResX, (float)mScreenViewport.Height / EngineSettings.VirtualResY, 1);
            mScreenTransform = mScreenScale * Matrix.CreateTranslation(mScreenViewport.X, mScreenViewport.Y, 0);
        }

        #endregion

        #region Methods

        public override void Update()
        {
            UpdateViewportTransformation();
        }

        public void Move(int pDeltaX, int pDeltaY)
        {
            PositionX += pDeltaX;
            PositionY += pDeltaY;
            ForceInViewArea();
        }

        public void Move(Vector2 pDelta)
        {
            PositionX += (int)pDelta.X;
            PositionY += (int)pDelta.Y;
            ForceInViewArea();
        }

        public void FocusOn(Rectangle pFocus)
        {
            if (mViewport.Left > pFocus.Left) //Scrolling nach links
            {
                mViewport.X = pFocus.Left;
            }
            else if (mViewport.Right < pFocus.Right) //Scrolling nach rechts
            {
                mViewport.X = pFocus.Right - mViewport.Width;
            }
            if (mViewport.Top > pFocus.Top) //Scrolling nach oben
            {
                mViewport.Y = pFocus.Top;
            }
            else if (mViewport.Bottom < pFocus.Bottom) //Scrolling nach unten
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