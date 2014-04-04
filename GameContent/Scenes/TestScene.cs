/**************************************************************
 * (c) Carsten Baus, Jens Richter 2014
 *************************************************************/
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
using DragonEngine.Pool;
using Spine;

namespace SnakeMobile.GameContent.Scenes
{
    class TestScene : Scene
    {
        #region Properties

        SpineObject mFluffy = Engine.SpinePools["fluffy"].GetObject();
        Map mMap = new Map(MapLayout.profile);

        #endregion

        #region Construct

        public TestScene(String pSceneName)
            : base(pSceneName)
        {
            //mCamera.Move(-50, -50);
            mClearColor = Color.CadetBlue;
        }
        #endregion

        #region Methoden

        public override void LoadContent()
        {
            TextureManager.Instance.Add("BackgroundStart", @"gfx\menuBackground");
            mFluffy.Load();
            mFluffy.Position = new Vector2(600, 650);

            base.LoadContent();
        }

        #region Update

        public override void Update()
        {
            CheckClick();
            mFluffy.Update();
            mCamera.Update();
        }

        private void CheckClick()
        {
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
