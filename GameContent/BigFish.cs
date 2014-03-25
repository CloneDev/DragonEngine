using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DragonEngine.Entities;
using Microsoft.Xna.Framework;
using DragonEngine;
using DragonEngine.Manager;
using Microsoft.Xna.Framework.Graphics;

namespace SnakeMobile.GameContent
{
    class BigFish : AnimatedSprite
    {
        #region Properties

        private Timer pTimer;
        private bool poisonFish = false;
        private bool fishAppear = false;
        private Random rnd = new Random();
        #endregion

        #region Getter & Setter

        public bool IsPoisonFish { get { return poisonFish; } }
        public bool FishCollected { get { return fishAppear; } set { fishAppear = value; } }
        public Timer FishTimer { get { return pTimer; } }
        public Vector2 PositionOnGrid { get; set; }
        #endregion

        #region Constructor

        public BigFish(Vector2 pPosition, String pTextureName, int pRectangleWidth, int pRectangleHeight, List<int> pFrames, int pAnimSpeed, bool pIsRepeat)
            : base(pPosition, pTextureName, pRectangleWidth, pRectangleHeight, pFrames, pAnimSpeed, pIsRepeat)
        {
            pTimer = new Timer(8000);
        }
        #endregion

        #region Methoden
        public void ResetBigFish(Vector2 pPosition)
        {
            poisonFish = (rnd.Next(0, 100) > 50) ? true : false;

            if (poisonFish)
                mFrames = new List<int>() { 176, 177, 178, 179};
            else
                mFrames = new List<int>() { 164, 165, 166, 167};
            mPosition = pPosition;

            mFrameInList = 0;
            mSourceRectanglePosition = mFrames[mFrameInList];

            pTimer.ResetTimer(EngineSettings.Time);
            pTimer.IsRunning = true;
            fishAppear = true;



        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.Instance.GetElementByString<Texture2D>(mTextureName), Position * new Vector2(32,32) + new Vector2(64,64), mSourceRectangle[mSourceRectanglePosition], mTint);
        }

        public void UpdateTimer(GameTime gameTime)
        {
            if (pTimer.IsTimerFinish(gameTime))
            {
                mPosition = new Vector2(-3, -3);
                fishAppear = false;
            }
        }

        #endregion
    }
}
