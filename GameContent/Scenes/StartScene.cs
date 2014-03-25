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
    class StartScene : Scene
    {
        #region Properties

        SpineObject mFluffy;

        #endregion

        #region Getter & Setter
        #endregion

        #region Construct

        public StartScene(String pSceneName)
            : base(pSceneName)
        {
            mClearColor = Color.CadetBlue;
            mUpdateAction.Add(CheckClick);
        }
        #endregion

        #region Methoden

        public override void Initialize()
        {
            mFluffy = new SpineObject("fluffy", new Vector2(300, 300));
            mDrawAction.Add(mFluffy.Draw);
            
            base.Initialize();
        }
        
        public override void LoadContent()
        {
            TextureManager.Instance.Add("BackgroundStart", @"gfx\menuBackground");
            mFluffy.Load();

            base.LoadContent();
        }

        private void CheckClick(GameTime gameTime)
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
    }
}
