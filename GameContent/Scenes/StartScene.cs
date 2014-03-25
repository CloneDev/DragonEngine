using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DragonEngine.SceneManagement;
using Microsoft.Xna.Framework;
using DragonEngine.Entities;
using Microsoft.Xna.Framework.Input;
using DragonEngine;
using Microsoft.Xna.Framework.Input.Touch;
using Snessy.GameContent.Scenes;

namespace SnakeMobile.GameContent.Scenes
{
    class StartScene : Scene
    {
        #region Properties
        private Sprite BtnHighScore;
        private Sprite BtnShare;
        private Sprite BtnCredits;
        private Sprite BtnInstruction;
        private Sprite BtnStart;
        private Sprite MiddleLogo;
        #endregion

        #region Getter & Setter
        #endregion

        #region Construct

        public StartScene(String pSceneName)
            : base(pSceneName)
        {
            mClearColor = Color.CadetBlue;
            if (EngineSettings.OnAndriod)
            {
                TouchPanel.EnabledGestures = GestureType.Tap;
                mUpdateAction.Add(CheckTap);
            }
            else
                mUpdateAction.Add(CheckClick);
        }
        #endregion

        #region Methoden

        private void CheckTap(GameTime gameTime)
        {
            Point tapPosition;

            TouchCollection currentTouchState = TouchPanel.GetState();
            if (TouchPanel.IsGestureAvailable)
            {
                var gesture = TouchPanel.ReadGesture();
                tapPosition = new Point((int)gesture.Position.X, (int)gesture.Position.Y);

                if (BtnStart.SpriteBox.Contains(tapPosition) && gesture.GestureType == GestureType.Tap)
                {
                    if (EngineSettings.OnAndriod)
                    {
                        AndroidGameScene gc = (AndroidGameScene)SceneManager.Instance.GetScene("Game");
                        //gc.StartNewGame();
                    }
                    else
                    {
                        GameScene gc = (GameScene)SceneManager.Instance.GetScene("Game");
                        gc.StartNewGame();
                    }
                    SceneManager.Instance.SetCurrentSceneTo("Game");
                }

                if (BtnHighScore.SpriteBox.Contains(tapPosition) && gesture.GestureType == GestureType.Tap)
                    SceneManager.Instance.SetCurrentSceneTo("Highscore");
                if (BtnCredits.SpriteBox.Contains(tapPosition) && gesture.GestureType == GestureType.Tap)
                    SceneManager.Instance.SetCurrentSceneTo("Credits");
            }
        }

        private void CheckClick(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();

            if (BtnStart.SpriteBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed)
            {
                GameScene gc = (GameScene)SceneManager.Instance.GetScene("Game");
                gc.StartNewGame();
                SceneManager.Instance.SetCurrentSceneTo("Game");
            }

            if (BtnHighScore.SpriteBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed)
                SceneManager.Instance.SetCurrentSceneTo("Highscore");
            if (BtnCredits.SpriteBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed)
                SceneManager.Instance.SetCurrentSceneTo("Credits");
        }

        public override void LoadContent()
        {
            base.LoadContent();

            BtnHighScore = new Sprite(new Vector2(32, 96), "Dummy");
            BtnShare = new Sprite(new Vector2(32, 284), "Dummy");
            BtnStart = new Sprite(new Vector2(360, 424), "PlayBtn");
            BtnCredits = new Sprite(new Vector2(968, 96), "Dummy");
            BtnInstruction = new Sprite(new Vector2(968, 284), "Dummy");

            if (EngineSettings.IsDebug)
            {
                mUpdateGameObjects.Add(BtnHighScore);
                mUpdateGameObjects.Add(BtnShare);
                mUpdateGameObjects.Add(BtnStart);
                mUpdateGameObjects.Add(BtnCredits);
                mUpdateGameObjects.Add(BtnInstruction);
            }
        }

        protected override void FillTexture2DList()
        {
            mTexture2DStringList.Add("Dummy", @"gfx\dummybtn");
            mTexture2DStringList.Add("PlayBtn", @"gfx\playbtn");
            mTexture2DStringList.Add("BackgroundStart", @"gfx\menuBackground");
        }
        #endregion
    }
}
