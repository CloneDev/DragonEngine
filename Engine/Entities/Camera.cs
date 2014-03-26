using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DragonEngine.Entities
{
    class Camera : BaseObject
    {
        #region Properties

        private Rectangle mViewArea;
        private Rectangle mViewport;
        private Rectangle mScreenViewport;
        private Matrix mViewportTransform;
        private Matrix mScreenScale;
        private Matrix mScreenTransform;

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

        public virtual void Update(Player player, Map map)
        {
            if (mViewport.X + leftspace > player.position.X) //Scrolling nach links
            {
                mViewport.X = (int)player.position.X - leftspace;
            }
            else if (mViewport.X + mViewport.Width - rightspace < player.position.X) //Scrolling nach rechts
            {
                mViewport.X = (int)player.position.X - (mViewport.Width - rightspace);
            }
            if (mViewport.X < 0) //Linker Maprand
            {
                mViewport.X = 0;
            }
            else if (mViewport.X > map.size.X - mViewport.Width) //Rechter Maprand
            {
                mViewport.X = (int)map.size.X - mViewport.Width;
            }

            if (mViewport.Y + topspace > player.position.Y) //Scrolling nach oben
            {
                mViewport.Y = (int)player.position.Y - topspace;
            }
            else if (mViewport.Y + mViewport.Height - bottomspace < player.position.Y) //Scrolling nach unten
            {
                mViewport.Y = (int)player.position.Y - (mViewport.Height - bottomspace);
            }
            if (mViewport.Y < 0) //Oberer Maprand
            {
                mViewport.Y = 0;
            }
            else if (mViewport.Y > map.size.Y - mViewport.Height) //Unterer Maprand
            {
                mViewport.Y = (int)map.size.Y - mViewport.Height;
            }
            UpdateTransformation(); //Abgekapselt damit Camera für Menü ohne Spieler verwendbar ist.
        }

        public void UpdateViewportTransformation()
        {
            mViewportTransform = Matrix.CreateTranslation(-mViewport.X, -mViewport.Y, 0);
        }

        //public void InitializeBlackBandTransformation()
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
