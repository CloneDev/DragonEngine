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
        private Matrix mScreenTransform;

        #endregion

        #region Constructor

        public Camera()
        {
            //mViewport = new Rectangle(0, 0, (int)Game1.resolution.X, (int)Game1.resolution.Y);
        }

        #endregion

        #region Methoden

        public virtual void Initialize()
        {
            mScreenViewport = new Rectangle();
        }

        //public virtual void Update(Player player, Map map)
        //{
        //    if (mViewport.X + leftspace > player.position.X) //Scrolling nach links
        //    {
        //        mViewport.X = (int)player.position.X - leftspace;
        //    }
        //    else if (mViewport.X + mViewport.Width - rightspace < player.position.X) //Scrolling nach rechts
        //    {
        //        mViewport.X = (int)player.position.X - (mViewport.Width - rightspace);
        //    }
        //    if (mViewport.X < 0) //Linker Maprand
        //    {
        //        mViewport.X = 0;
        //    }
        //    else if (mViewport.X > map.size.X - mViewport.Width) //Rechter Maprand
        //    {
        //        mViewport.X = (int)map.size.X - mViewport.Width;
        //    }

        //    if (mViewport.Y + topspace > player.position.Y) //Scrolling nach oben
        //    {
        //        mViewport.Y = (int)player.position.Y - topspace;
        //    }
        //    else if (mViewport.Y + mViewport.Height - bottomspace < player.position.Y) //Scrolling nach unten
        //    {
        //        mViewport.Y = (int)player.position.Y - (mViewport.Height - bottomspace);
        //    }
        //    if (mViewport.Y < 0) //Oberer Maprand
        //    {
        //        mViewport.Y = 0;
        //    }
        //    else if (mViewport.Y > map.size.Y - mViewport.Height) //Unterer Maprand
        //    {
        //        mViewport.Y = (int)map.size.Y - mViewport.Height;
        //    }
        //    UpdateTransformation(); //Abgekapselt damit Camera für Menü ohne Spieler verwendbar ist.
        //}

        //public void UpdateTransformation()
        //{
        //    int width = EngineSettings.Graphics.GraphicsDevice.PresentationParameters.BackBufferWidth;
        //    int height = EngineSettings.Graphics.GraphicsDevice.PresentationParameters.BackBufferHeight;
        //    if (Game1.stretch) //Viewport screenfüllend
        //    {
        //        mScreenViewport.X = 0;
        //        mScreenViewport.Y = 0;
        //        mScreenViewport.Width = width;
        //        mScreenViewport.Height = height;
        //    }
        //    else //Viewport mit Offset auf Screen
        //    {
        //        if (mScreenViewport.X < mScreenViewport.Y) //Balken oben/unten
        //        {
        //            mScreenViewport.Width = (int)width;
        //            mScreenViewport.Height = (int)(width / Game1.resolution.X * Game1.resolution.Y);
        //        }
        //        else //Balken links/rechts
        //        {
        //            mScreenViewport.Height = (int)height;
        //            mScreenViewport.Width = (int)(height / Game1.resolution.Y * Game1.resolution.X);
        //        }
        //        mScreenViewport.X = (width - (int)mScreenViewport.Width) / 2;
        //        mScreenViewport.Y = (height - (int)mScreenViewport.Height) / 2;
        //        //= viewport.Width / resolution.X;
        //        //= viewport.Height / resolution.Y;
        //    }
        //    Matrix screenScale = Matrix.CreateScale((float)mScreenViewport.Width / Game1.resolution.X, (float)mScreenViewport.Height / Game1.resolution.Y, 1);
        //    mScreenTransform = screenScale * Matrix.CreateTranslation(mScreenViewport.X, mScreenViewport.Y, 0);

        //    mViewportTransform = Matrix.CreateTranslation(-mViewport.X, -mViewport.Y, 0);
        //}

        #endregion

    }
}
