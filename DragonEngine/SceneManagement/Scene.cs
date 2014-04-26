﻿/**************************************************************
 * (c) Carsten Baus 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DragonEngine.Manager;
using DragonEngine.Entities;

namespace DragonEngine.SceneManagement
{
    public class Scene
    {
        #region Properties

        protected String mName;
        protected String mBackgroundName = "pixel";
        protected SpriteBatch mSpriteBatch;
        protected RenderTarget2D mRenderTarget;
        protected Camera mCamera;

        protected Color mClearColor = Color.Transparent;

        #region Getter & Setter

        public String Name { get { return this.mName; } }
        public String Background { set { mBackgroundName = value; mClearColor = Color.White; } }

        #endregion

        #endregion

        #region Constructor

        public Scene(String pSceneName)
        {
            this.mName = pSceneName;
            mSpriteBatch = new SpriteBatch(EngineSettings.Graphics.GraphicsDevice);
            mRenderTarget = new RenderTarget2D(EngineSettings.Graphics.GraphicsDevice, EngineSettings.VirtualResX, EngineSettings.VirtualResY);
            mCamera = new Camera(new Rectangle(0, 0, EngineSettings.VirtualResX, EngineSettings.VirtualResY));
        }

        #endregion

        #region Methods

        public virtual void LoadContent()
        {

        }

        /// <summary>
        /// Updatet Funktionen und GameObjects
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(){}

        public virtual void Draw(){}

        protected void DrawBackground()
        {
            EngineSettings.Graphics.GraphicsDevice.SetRenderTarget(mRenderTarget);
            EngineSettings.Graphics.GraphicsDevice.Clear(mClearColor);
            mSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, null, null, null, null, mCamera.ViewportTransform);
                mSpriteBatch.Draw(TextureManager.Instance.GetElementByString<Texture2D>(mBackgroundName), new Rectangle(0, 0, EngineSettings.VirtualResX, EngineSettings.VirtualResY), mClearColor);
            mSpriteBatch.End();
        }

        protected void DrawToScreen()
        {
            EngineSettings.Graphics.GraphicsDevice.SetRenderTarget(null);
            mSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, mCamera.ScreenTransform);
                mSpriteBatch.Draw(mRenderTarget, new Rectangle(0, 0, EngineSettings.VirtualResX, EngineSettings.VirtualResY), Color.White);
            mSpriteBatch.End();
        }

        #endregion

    }
}