using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DragonEngine.SceneManagement;
using Microsoft.Xna.Framework;
using DragonEngine.Entities;
using Microsoft.Xna.Framework.Input;
using DragonEngine;
using DragonEngine.Manager;
using Spine;
using Microsoft.Xna.Framework.Graphics;

namespace SnakeMobile.GameContent.Scenes
{
    class TestScene : Scene
    {
        #region Properties

        SpineObject mFluffy;

        #endregion

        #region Getter & Setter
        #endregion

        #region Construct

        public TestScene(String pSceneName)
            : base(pSceneName)
        {
            mClearColor = Color.CadetBlue;
            //mUpdateAction.Add(CheckClick);
        }
        #endregion

        #region Override Methoden

        public override void Update()
        {
            CheckClick();
            mFluffy.UpdateAnimation(EngineSettings.Time);
        }

        public override void Draw()
        {
            EngineSettings.Graphics.GraphicsDevice.SetRenderTarget(mRenderTarget);

            mSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, null, null, null, null, mCamera.ViewportTransform);
            mSpriteBatch.Draw(TextureManager.Instance.GetElementByString<Texture2D>(mBackgroundName), new Rectangle(0, 0, EngineSettings.VirtualResX, EngineSettings.VirtualResY), mClearColor);

            mFluffy.Draw();
            mSpriteBatch.End();

            mSpriteBatch.End();

            EngineSettings.Graphics.GraphicsDevice.SetRenderTarget(null);

            mSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, mCamera.ScreenTransform);
            mSpriteBatch.Draw(mRenderTarget, new Rectangle(0, 0, EngineSettings.VirtualResX, EngineSettings.VirtualResY), Color.White);
            mSpriteBatch.End();
        }
        
        public override void Initialize()
        {
            mCamera = new Camera(new Rectangle(0,0,EngineSettings.VirtualResX,EngineSettings.VirtualResY));
            mCamera.Initialize();
            mCamera.Move(-50, -50);
            mFluffy = new SpineObject("fluffy", new Vector2(600, 650));
            mFluffy.Initialize();
            //mDrawAction.Add(mFluffy.Draw);
            //mUpdateAction.Add(mFluffy.UpdateAnimation);

            base.Initialize();
        }
        
        public override void LoadContent()
        {
            TextureManager.Instance.Add("BackgroundStart", @"gfx\menuBackground");
            mFluffy.Load();

            base.LoadContent();
        }
        #endregion

        #region Methoden



        private void CheckClick()
        {
            mCamera.UpdateViewportTransformation();
            MouseState ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Pressed && mFluffy.AnimationState.GetCurrent(0) != null)
            {
                if (mFluffy.AnimationState.GetCurrent(0).animation.name == mFluffy.Skeleton.Data.FindAnimation("idle").name)
                    mFluffy.AnimationState.SetAnimation(0, "attack", false);
            }
            else if (ms.RightButton == ButtonState.Pressed)
            {
                Environment.Exit(0);
            }
            else
            {
                mFluffy.AnimationState.AddAnimation(0, "idle", true, 0);
            }
        }
        #endregion
    }
}
