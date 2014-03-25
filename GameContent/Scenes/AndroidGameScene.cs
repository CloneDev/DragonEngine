using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DragonEngine.SceneManagement;
using Microsoft.Xna.Framework;
using DragonEngine;
using DragonEngine.Entities;

namespace Snessy.GameContent.Scenes
{
    class AndroidGameScene : Scene
    {
        #region Properties 

            #region TapInput
        private Rectangle TapLeft;
        private Rectangle TapRight;
            #endregion

            #region WorldSettings
        private static int[,] mGameWorld = new int[36, 18];
        private static readonly int WORLD_PADDERN = 64;
            #endregion


            #region FishItem
        private AnimatedSprite FishItem;
        private Vector2 ItemPosition = new Vector2(-3, -3);
        private bool ItemCollected = false;
            #endregion
        #endregion

        #region Constructor
        public AndroidGameScene(String pSceneName)
            : base(pSceneName)
        {
            mClearColor = Color.CadetBlue;

            //ResetSnake();

            //mDrawAction.Add(DrawGrid);
            //mDrawAction.Add(DrawScore);

            //if (EngineSettings.IsDebug)
            //    mDrawAction.Add(DrawDebug);


            int ThirdScreenWidth = EngineSettings.WindowWidth / 3;

            TapLeft = new Rectangle(0, 0, ThirdScreenWidth, EngineSettings.WindowHeight);
            TapRight = new Rectangle(EngineSettings.WindowWidth - ThirdScreenWidth, 0, ThirdScreenWidth, EngineSettings.WindowHeight);

        }


        public override void LoadContent()
        {
            base.LoadContent();

            FishItem = new AnimatedSprite(new Vector2(-3, -3), "SpriteAtlas", 32, 32, new List<int>() { 160, 161, 162, 163 }, 200, true);

            mUpdateGameObjects.Add(FishItem);
        }

        protected override void FillTexture2DList()
        {
            mTexture2DStringList.Add("Background", @"gfx\background");
            mTexture2DStringList.Add("Item", @"gfx\coin");
            mTexture2DStringList.Add("SpriteAtlas", @"gfx\textureAtlas");
            mTexture2DStringList.Add("Font", @"gfx\menuAtlas");
        }
        #endregion
    }
}