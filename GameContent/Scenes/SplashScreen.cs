using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DragonEngine.SceneManagement;
using Microsoft.Xna.Framework;
using DragonEngine.Entities;

namespace SnakeMobile.GameContent.Scenes
{
    class SplashScreen : Scene
    {
        #region Properties

        private Timer timer = new Timer(1500);
        #endregion

        #region Getter & Setter
        #endregion

        #region Construct

        public SplashScreen(String pSceneName)
            : base(pSceneName)
        {
            mUpdateAction.Add(UpdateSplashScreen);
        }
        #endregion

        #region Methoden

        private void UpdateSplashScreen(GameTime gameTime)
        {
            if (!timer.IsRunning) timer.StartTimer(gameTime);

            if (timer.IsTimerFinish(gameTime)) SceneManager.Instance.SetCurrentSceneTo("Start");
        }

        protected override void FillTexture2DList()
        {
            mTexture2DStringList.Add("SplashScreen", @"gfx\splash");
        }
        #endregion
    }
}
