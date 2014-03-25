using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DragonEngine.SceneManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using DragonEngine;
using Microsoft.Xna.Framework.Input.Touch;

namespace SnakeMobile.GameContent.Scenes
{
    class HighScoreScreen : Scene
    {
        #region Properties
        #endregion

        #region Getter & Setter
        #endregion

        #region Constructor

        public HighScoreScreen(String pSceneName)
            : base(pSceneName)
        {
            mClearColor = Color.Honeydew;

            mUpdateAction.Add(LeaveScreen);
        }
        #endregion

        #region Methoden
        private void LeaveScreen(GameTime gameTime)
        {
            if (EngineSettings.OnAndriod)
            {
                TouchCollection currentTouchState = TouchPanel.GetState();
                while (TouchPanel.IsGestureAvailable)
                {
                    var gesture = TouchPanel.ReadGesture();
                    switch (gesture.GestureType)
                    {
                        case GestureType.Tap:
                            SceneManager.Instance.SetCurrentSceneTo("Start");
                            break;
                    }
                }
            }
            else
            {
                MouseState ms = Mouse.GetState();

                if (ms.LeftButton == ButtonState.Pressed)
                    SceneManager.Instance.SetCurrentSceneTo("Start");
            }
        }
        #endregion
    }
}
