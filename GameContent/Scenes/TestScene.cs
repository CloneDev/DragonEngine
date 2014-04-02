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
        }
        #endregion

        #region Methoden

        #region Initialize & Load

        public override void Initialize()
        {
            mCamera = new Camera(new Rectangle(0, 0, EngineSettings.VirtualResX, EngineSettings.VirtualResY));
            mCamera.Initialize();
            mCamera.Move(-50, -50);
            mFluffy = new SpineObject("fluffy", new Vector2(600, 650));
            mFluffy.Initialize();

            base.Initialize();
        }

        public override void LoadContent()
        {
            TextureManager.Instance.Add("BackgroundStart", @"gfx\menuBackground");
            mFluffy.Load();

            base.LoadContent();
        }

        #endregion

        #region Update

        public override void Update()
        {
            CheckClick();
            mFluffy.Update();
            mCamera.UpdateViewportTransformation();
        }

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

        #region Draw

        public override void Draw()
        {
            DrawBackground();
            //mSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, null, null, null, null, mCamera.ViewportTransform);
            // //Draw with SpriteBatch
            //mSpriteBatch.End();
            mFluffy.Draw();
            DrawToScreen();
        }

        #endregion

        #endregion
    }
}
