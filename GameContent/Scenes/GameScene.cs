using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DragonEngine.SceneManagement;
using Microsoft.Xna.Framework;
using DragonEngine.Manager;
using Microsoft.Xna.Framework.Graphics;
using DragonEngine.Entities;
using Microsoft.Xna.Framework.Input;
using DragonEngine;
using Microsoft.Xna.Framework.Input.Touch;

namespace SnakeMobile.GameContent.Scenes
{
    class GameScene : Scene
    {
        #region Properties

        private int[,] mGameWorld = new int[36, 18];
        private readonly int WORLD_PADDERN = 64;

        private readonly int mSnakeLengthDefault = 2;
        private int mSnakeLength = 2;

        /*
         * 1 = Up
         * 2 = Right
         * 3 = Down
         * 4 = Left
         */
        private int MoveDirection = 1;
        private int AnimStep = 0;
        private int AnimStepNextTile = 1;

        private Vector2 mStartPosition = new Vector2(18, 9);
        private Vector2[] mPositionInGrid = new Vector2[36 * 18];
        private int[] SnakeAnim = new int[36 * 18];
        private TiledSprite SnakeSheet;

        private KeyboardState ksLast;
        private KeyboardState ksCurrent;

        private Random rnd = new Random();

        private static bool IsGameEnd = false;

        private int Score = 0;
        private readonly int ScorePaddingRight = 16;
        private DSpriteFont spriteFont;

        private AnimatedSprite FishItem;
        private Vector2 ItemPosition = new Vector2(-3, -3);
        private bool ItemCollected = false;

        private Rectangle TapLeft;
        private Rectangle TapRight;

        #region Steine
        private static readonly int StoneAppear = 2;
        private static readonly int StoneMaxCount = 20;
        private int StoneAppearCounter = 0;
        private int StoneCount = 0;
        private Vector2[] StonePosition = new Vector2[StoneMaxCount];
        private Stone[] StoneClass = new Stone[StoneMaxCount];
        #endregion

        #region BigFish
        private readonly int mBigFishAppear = 1;
        private BigFish mBigFish;
        #endregion

        #region UpdateSkip
        private readonly int updateSkipDefault = 19;
        private readonly int updateSkipMax = 3;
        private readonly int updateSkipSpeed = 1;
        private int updateSkip = 0;
        private int updateSkipInGame = 19;
        #endregion

        #region GameOverScreen
        private Sprite BtnRetry;
        private Sprite BtnHighscore;
        private Sprite BtnHome;

        private List<Sprite> BtnList = new List<Sprite>();
        #endregion

        //private SpriteFont font;

        #endregion

        #region Getter & Setter
        #endregion

        #region Construct

        public GameScene(String pSceneName)
            : base(pSceneName)
        {
            mClearColor = Color.CadetBlue;

            ResetSnake();

            mDrawAction.Add(DrawGrid);
            mDrawAction.Add(DrawScore);

            if (EngineSettings.IsDebug)
                mDrawAction.Add(DrawDebug);


            int ThirdScreenWidth = EngineSettings.ScreenResX / 3;

            TapLeft = new Rectangle(0, 0, ThirdScreenWidth, EngineSettings.ScreenResY);
            TapRight = new Rectangle(EngineSettings.ScreenResX - ThirdScreenWidth, 0, ThirdScreenWidth, EngineSettings.ScreenResY);

        }
        #endregion

        #region Methoden

        #region DrawMethoden
        
        private void DrawGrid()
        {
            mSpriteBatch.Begin();

            for (int y = 0; y < mGameWorld.GetLength(1); y++)
                for (int x = 0; x < mGameWorld.GetLength(0); x++)
                {
                    switch (mGameWorld[x, y])
                    {
                        case 4: FishItem.Draw(mSpriteBatch);
                            break;
                        case 5: mBigFish.Draw(mSpriteBatch);
                            break;
                    }
                }

            mSpriteBatch.Draw(SnakeSheet.GetTileTexture2D(SnakeAnim[0]), new Rectangle((int)mPositionInGrid[0].X * 32 + WORLD_PADDERN, (int)mPositionInGrid[0].Y * 32 + WORLD_PADDERN, 32, 32), Color.White);
            
            for (int i = 1; i < mSnakeLength - 1; i++)
                mSpriteBatch.Draw(SnakeSheet.GetTileTexture2D(SnakeAnim[i]), new Rectangle((int)mPositionInGrid[i].X * 32 + WORLD_PADDERN, (int)mPositionInGrid[i].Y * 32 + WORLD_PADDERN, 32, 32), Color.White);

            mSpriteBatch.Draw(SnakeSheet.GetTileTexture2D(SnakeAnim[mSnakeLength-1]), new Rectangle((int)mPositionInGrid[mSnakeLength-1].X * 32 + WORLD_PADDERN, (int)mPositionInGrid[mSnakeLength-1].Y * 32 + WORLD_PADDERN, 32, 32), Color.White);

            mSpriteBatch.End();
        }

        private void DrawDebug()
        {
            mSpriteBatch.Begin();
            //mSpriteBatch.DrawString(font, "Game Ende : " + IsGameEnd, Vector2.Zero, Color.Azure);
            //mSpriteBatch.DrawString(font, "Score : " + Score * 10, new Vector2(0, 20), Color.Azure);
            //mSpriteBatch.DrawString(font, "Speed : " + updateSkipInGame, new Vector2(0, 40), Color.Azure);
            //mSpriteBatch.DrawString(font, "Stones : " + StoneCount, new Vector2(0, 60), Color.Azure);
            //mSpriteBatch.DrawString(font, "Fish RestTime : " + mBigFish.FishTimer.GetInfo(), new Vector2(0, 80), Color.Azure);
            mSpriteBatch.Draw(TextureManager.Instance.GetElementByString<Texture2D>("pixel"), TapLeft, Color.AliceBlue);
            mSpriteBatch.Draw(TextureManager.Instance.GetElementByString<Texture2D>("pixel"), TapRight, Color.AntiqueWhite);
            mSpriteBatch.End();
        }

        private void DrawScore()
        {
            mSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            mSpriteBatch.Draw(spriteFont.GetTileTexture2D(11), new Vector2(EngineSettings.VirtualResX - ScorePaddingRight - 7 * 40 - spriteFont.GetTileTexture2D(11).Width, 5), Color.White);

            mSpriteBatch.Draw(spriteFont.GetTileTexture2D(0), new Vector2(EngineSettings.VirtualResX - ScorePaddingRight - 40, 5), Color.White);

            bool stillDrawing = true;
            int drawPositionX = EngineSettings.VirtualResX - 80 - ScorePaddingRight;
            int tmpScore = Score;
            
            if(Score > 0)
                while (stillDrawing)
                {
                    int tileID = tmpScore % 10;
                    mSpriteBatch.Draw(spriteFont.GetTileTexture2D(tileID), new Vector2(drawPositionX, 5), Color.White);
                    tmpScore /= 10;
                    drawPositionX -= 40;
                    if (tmpScore == 0)
                        stillDrawing = false;
                }

            mSpriteBatch.End();
        }
        
        #endregion
        
        #region Update Methoden

        public override void Update(GameTime gameTime)
        {
            foreach (GameObject go in mUpdateGameObjects)
                go.Update(gameTime);

            UpdatePlayerPosition(gameTime);
        }
        
        private void UpdatePlayerPosition(GameTime gameTime)
        {
            if (IsGameEnd) EndGame();
            if (updateSkip >= updateSkipInGame && !IsGameEnd)
            {
                updateSkip = 0;
                Vector2 tmpPosition = mPositionInGrid[0];
                Vector2 tmpPosition2 = mPositionInGrid[1];

                MovePlayer();
                if (AlreadyWalkedOn(MoveDirection))
                {
                    mPositionInGrid[0] = tmpPosition;
                    IsGameEnd = true;
                    return;
                }

                if (IsOutsidePlayground())
                {
                    mPositionInGrid[0] = tmpPosition;
                    IsGameEnd = true;
                    return;
                }

                if (mPositionInGrid[0] == ItemPosition)
                {
                    Score++;
                    mSnakeLength++;
                    ItemCollected = true;
                    StoneAppearCounter++;

                    if (StoneAppearCounter == StoneAppear)
                        AddNewStone();

                    if (Score % mBigFishAppear == 0)
                        mBigFish.ResetBigFish(GetFreePositionOnGrid());
                }

                if (mPositionInGrid[0] == mBigFish.Position)
                {
                    if (mBigFish.IsPoisonFish)
                    {
                        IsGameEnd = true;
                    }
                    else
                    {
                        Score += 5;
                        mBigFish.Position = new Vector2(-3, -3);
                    }
                }

                for (int i = 0; i < StoneCount; i++)
                {
                    if (mPositionInGrid[0] == StonePosition[i])
                    {
                        mPositionInGrid[0] = tmpPosition;
                        IsGameEnd = true;
                        return;
                    }
                }

                for (int i = 1; i < mSnakeLength; i++)
                {
                    tmpPosition2 = mPositionInGrid[i];
                    mPositionInGrid[i] = tmpPosition;
                    tmpPosition = tmpPosition2;
                }

                if (ItemCollected)
                {
                    //mPositionInGrid[mSnakeLength - 1] = ItemPosition;
                    ItemCollected = false;
                    ItemPosition = new Vector2(-3, -3);
                }
            CalculateSnakeSpriteAnim();

            SetWorldElements();
            }
            else
                updateSkip++;

            //SetWorldElements();
        }

        private void UpdateTapInput(GameTime gameTime)
        {
            Point tapPosition;

            TouchCollection currentTouchState = TouchPanel.GetState();
            if (TouchPanel.IsGestureAvailable)
            {
                var gesture = TouchPanel.ReadGesture();
                tapPosition = new Point((int)gesture.Position.X, (int)gesture.Position.Y);

                if (TapLeft.Contains(tapPosition) && gesture.GestureType == GestureType.Tap)
                {
                    MoveDirection--;
                    if (MoveDirection == 0) MoveDirection = 4;
                }

                if (TapRight.Contains(tapPosition) && gesture.GestureType == GestureType.Tap)
                {
                    MoveDirection++;
                    if (MoveDirection == 5) MoveDirection = 1;
                }
            }
        }

        private void UpdateInput(GameTime gameTime)
        {
            ksLast = ksCurrent;
            ksCurrent = Keyboard.GetState();

            if (ksCurrent.IsKeyUp(Keys.Left) && ksLast.IsKeyDown(Keys.Left))
            {
                MoveDirection--;
                if (MoveDirection == 0) MoveDirection = 4;
            }

            if (ksCurrent.IsKeyUp(Keys.Right) && ksLast.IsKeyDown(Keys.Right))
            {
                MoveDirection++;
                if (MoveDirection == 5) MoveDirection = 1;
            }
        }

        private void UpdateItem(GameTime gameTime)
        {
            if (ItemPosition == new Vector2(-3, -3))
            {
                bool endloop = false;

                while (!endloop)
                {
                    Vector2 tmpPosi = GetFreePositionOnGrid();
                    if (mGameWorld[(int)tmpPosi.X, (int)tmpPosi.Y] == 0)
                    {
                        ItemPosition = tmpPosi;
                        endloop = true;
                    }
                }
            }
            mGameWorld[(int)ItemPosition.X, (int)ItemPosition.Y] = 4;
            FishItem.Position = ItemPosition * new Vector2(32, 32) + new Vector2(64, 64);
        }

        private void UpdateGameEndScreen(GameTime gameTime)
        {
            int clickId = -1;
            if (EngineSettings.OnAndriod)
            {
                Point tapPosition;

                TouchCollection currentTouchState = TouchPanel.GetState();
                while (TouchPanel.IsGestureAvailable)
                {
                    var gesture = TouchPanel.ReadGesture();
                    tapPosition = new Point((int)gesture.Position.X, (int)gesture.Position.Y);

                    foreach (Sprite s in BtnList)
                        if (s.SpriteBox.Contains(tapPosition) && gesture.GestureType == GestureType.Tap)
                            clickId = BtnList.IndexOf(s);
                }


            }
            else
            {
                MouseState ms = Mouse.GetState();

                foreach (Sprite s in BtnList)
                    if (s.SpriteBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed)
                        clickId = BtnList.IndexOf(s);
            }

            switch (clickId)
            {
                case 0: StartNewGame();
                    break;
                case 1: SceneManager.Instance.SetCurrentSceneTo("Highscore");
                    break;
                case 2: SceneManager.Instance.SetCurrentSceneTo("Start");
                    break;
                default: break;
            }
        }
        
        #endregion
        
        private void AddNewStone()
        {
            int id = rnd.Next(0, 2);
            if (id == 0)
            {
                if (StoneCount < StoneMaxCount)
                    CreateNewStone();
                else if (updateSkipInGame > updateSkipMax)
                    updateSkipInGame -= updateSkipSpeed;
            }
            else
            {
                if (updateSkipInGame > updateSkipMax)
                    updateSkipInGame -= updateSkipSpeed;
                else if (StoneCount < StoneMaxCount)
                    CreateNewStone();
            }
            StoneAppearCounter = 0;
        }

        private void SetWorldElements()
        {
            for (int y = 0; y < mGameWorld.GetLength(1); y++)
                for (int x = 0; x < mGameWorld.GetLength(0); x++)
                    mGameWorld[x, y] = 0;

            for (int i = 0; i < StoneCount; i++)
                mGameWorld[(int)StonePosition[i].X, (int)StonePosition[i].Y] = 6;

            if (mBigFish.PositionX > -1 || mBigFish.PositionY > -1)
                mGameWorld[mBigFish.PositionX, mBigFish.PositionY] = 5;

            for (int i = 1; i < mSnakeLength - 1; i++)
                mGameWorld[(int)mPositionInGrid[i].X, (int)mPositionInGrid[i].Y] = 2;

            int tailPosition = (mSnakeLength >= 3) ? 2 : 1;

            mGameWorld[(int)mPositionInGrid[mSnakeLength - tailPosition].X, (int)mPositionInGrid[mSnakeLength - tailPosition].Y] = 3;
            if (mPositionInGrid[0].X < 0 || mPositionInGrid[0].Y < 0 || mPositionInGrid[0].X >= 36 || mPositionInGrid[0].Y >= 18) return;
            mGameWorld[(int)mPositionInGrid[0].X, (int)mPositionInGrid[0].Y] = 1;
        }

        private void CreateNewStone()
        {
            bool endloop = false;

            while (!endloop)
            {
                Vector2 tmpPosi = GetFreePositionOnGrid();
                if (StoneCanBePlaced(tmpPosi))
                {
                    StonePosition[StoneCount] = tmpPosi;
                    StoneClass[StoneCount] = new Stone(Vector2.Zero, "SpriteAtlas", 96, 96, new List<int> { 3, 4, 8, 9 }, 260, false);
                    StoneClass[StoneCount].Position = tmpPosi * new Vector2(32, 32) + new Vector2(32, 32);
                    mUpdateGameObjects.Add(StoneClass[StoneCount]);
                    StoneCount++;
                    endloop = true;
                }
            }
        }

        private Vector2 GetFreePositionOnGrid()
        {
            Vector2 tmpPosi;
            tmpPosi.X = rnd.Next(0, 36);
            tmpPosi.Y = rnd.Next(0, 18);

            if (mGameWorld[(int)tmpPosi.X, (int)tmpPosi.Y] == 0)
                return tmpPosi;
            else
                return GetFreePositionOnGrid();
        }

        private bool AlreadyWalkedOn(int pMove)
        {
            Vector2 tmpPosition;

                switch (MoveDirection)
                {
                    case 1: tmpPosition = mPositionInGrid[0] + new Vector2(0, -1);
                        break;
                    case 2: tmpPosition = mPositionInGrid[0] +  new Vector2(1, 0);
                        break;
                    case 3: tmpPosition = mPositionInGrid[0] +  new Vector2(0, 1);
                        break;
                    case 4: tmpPosition = mPositionInGrid[0] +  new Vector2(-1, 0);
                        break;
                    default: tmpPosition = mPositionInGrid[0];
                        break;
                }

            for (int i = 1; i < mSnakeLength - 1; i++)
            {
                if (tmpPosition == mPositionInGrid[i]) 
                    return true;
            }

            return false;
        }

        private bool IsOutsidePlayground()
        {
            if ((mPositionInGrid[0].X < 0 || mPositionInGrid[0].X > 35)
                || mPositionInGrid[0].Y < 0 || mPositionInGrid[0].Y > 17)
                return true;
            return false;
        }

        private void MovePlayer()
        {
            switch (MoveDirection)
            {
                case 1: mPositionInGrid[0] += new Vector2(0, -1);
                    break;
                case 2: mPositionInGrid[0] += new Vector2(1, 0);
                    break;
                case 3: mPositionInGrid[0] += new Vector2(0, 1);
                    break;
                case 4: mPositionInGrid[0] += new Vector2(-1, 0);
                    break;
            }
        }

        private bool StoneCanBePlaced(Vector2 pPosition)
        {
            Vector2 Startposi = new Vector2(mPositionInGrid[0].X - 2, mPositionInGrid[0].Y - 2);

            if(mGameWorld[(int)pPosition.X, (int)pPosition.Y] != 0) return false;
            for(int x = 0; x < 5; x++)
                for(int y = 0; y < 5;y++)
                {
                    Vector2 tmpPosi = Startposi + new Vector2(x,y);
                    if(tmpPosi == pPosition) return false;
                }

            foreach (Vector2 v in StonePosition)
            {
                Vector2 startPosi = v - new Vector2(-1,-1);
                for(int x = 0; x < 3;x++)
                    for (int y = 0; y < 3; y++)
                        if (startPosi + new Vector2(x, y) == pPosition)
                            return false;
            }

            return true;
        }

        public void CalculateSnakeSpriteAnim()
        {
            int moveID = 0;
            switch(MoveDirection)
            {
                case 1: moveID = 4 + AnimStep;
                    break;
                case 2: moveID = 0 + AnimStep;
                    break;
                case 3: moveID = 20 + AnimStep;
                    break;
                case 4: moveID = 16 + AnimStep;
                    break;
            }
            SnakeAnim[0] = moveID;

            Vector2 tmpPosi1;
            Vector2 tmpPosi2;
            for(int i = 1; i < mSnakeLength; i++)
            {
                tmpPosi1 = mPositionInGrid[i-1];
                tmpPosi2 = mPositionInGrid[i+1];

                Vector2 pos = tmpPosi1 - tmpPosi2;

                if (pos == new Vector2(0, -2))
                    SnakeAnim[i] = 36;
                else if (pos == new Vector2(0, 2))
                    SnakeAnim[i] = 52;
                else if (pos == new Vector2(-2, 0))
                    SnakeAnim[i] = 48;
                else if (pos == new Vector2(2, 0))
                    SnakeAnim[i] = 32;
            }

            Vector2 posi = mPositionInGrid[mSnakeLength - 2] - mPositionInGrid[mSnakeLength - 1];

            if (posi == new Vector2(0, -1))
                SnakeAnim[mSnakeLength - 1] = 68;
            else if (posi == new Vector2(0, 1))
                SnakeAnim[mSnakeLength - 1] = 84;
            else if (posi == new Vector2(-1, 0))
                SnakeAnim[mSnakeLength - 1] = 80;
            else if (posi == new Vector2(1, 0))
                SnakeAnim[mSnakeLength - 1] = 64;
        }

        public void StartNewGame()
        {
            IsGameEnd = false;
            ItemCollected = false;

            StoneCount = 0;
            for (int i = 0; i < StonePosition.Length; i++)
                StonePosition[i] = Vector2.Zero; 

            Score = 0;
            updateSkip = 0;
            updateSkipInGame = updateSkipDefault;

            for (int i = 0; i < mPositionInGrid.Length; i++)
                mPositionInGrid[i] = Vector2.Zero;

            for (int i = 0; i < StoneMaxCount; i++)
            {
                mUpdateGameObjects.Remove(StoneClass[i]);
                StoneClass[i] = new Stone();
            }

            MoveDirection = 1;

            ResetSnake();

            CalculateSnakeSpriteAnim();

            mUpdateAction.Add(UpdatePlayerPosition);
            if (EngineSettings.OnAndriod)
                mUpdateAction.Add(UpdateTapInput);
            else
                mUpdateAction.Add(UpdateInput);
            mUpdateAction.Add(UpdateItem);
            mUpdateAction.Add(mBigFish.UpdateTimer);

            mUpdateGameObjects.Remove(BtnRetry);
            mUpdateGameObjects.Remove(BtnHighscore);
            mUpdateGameObjects.Remove(BtnHome);

            mUpdateAction.Remove(UpdateGameEndScreen);
        }

        private void ResetSnake()
        {
            mSnakeLength = mSnakeLengthDefault;

            for (int y = 0; y < mGameWorld.GetLength(1); y++)
                for (int x = 0; x < mGameWorld.GetLength(0); x++)
                    mGameWorld[x, y] = 0;

            mGameWorld[(int)mStartPosition.X, (int)mStartPosition.Y] = 1;
            mGameWorld[(int)mStartPosition.X, (int)mStartPosition.Y + 1] = 3;

            mPositionInGrid[0] = mStartPosition;
            mPositionInGrid[1] = mStartPosition + new Vector2(0, 1);
        }

        private void EndGame()
        {
            mUpdateGameObjects.Add(BtnRetry);
            mUpdateGameObjects.Add(BtnHighscore);
            mUpdateGameObjects.Add(BtnHome);

            mUpdateAction.Remove(UpdatePlayerPosition);
            if (EngineSettings.OnAndriod)
                mUpdateAction.Remove(UpdateTapInput);
            else
                mUpdateAction.Remove(UpdateInput);
            mUpdateAction.Remove(UpdateItem);
            mUpdateAction.Remove(mBigFish.UpdateTimer);

            mUpdateAction.Add(UpdateGameEndScreen);
        }

        public override void LoadContent()
        {
            base.LoadContent();

            //font = EngineSettings.EngineContent.Load<SpriteFont>(@"font\font");

            BtnRetry = new Sprite(new Vector2(1000, 20), "PlayBtn");
            BtnHighscore = new Sprite(new Vector2(1000, 260), "PlayBtn");
            BtnHome = new Sprite(new Vector2(1000, 500), "PlayBtn");

            FishItem = new AnimatedSprite(new Vector2(-3, -3), "SpriteAtlas", 32, 32, new List<int>() { 160, 161, 162, 163 }, 200, true);
            mBigFish = new BigFish(new Vector2(-3, -3), "SpriteAtlas", 32, 32, new List<int>() { 1, 2, 3, 4 }, 200, true);
            SnakeSheet = new TiledSprite(new Vector2(-32, -32), "SpriteAtlas", 32, 32);

            for (int i = 0; i < StoneMaxCount; i++)
                StoneClass[i] = new Stone(new Vector2(-3, -3), "SpriteAtlas", 96, 96, new List<int> { 3, 4, 8, 9 }, 3000, false);

            spriteFont = new DSpriteFont(new Vector2(0, 0), "Font", new List<Rectangle>()
                {
                    new Rectangle(0,0,40,64), 
                    new Rectangle(40,0,40,64), 
                    new Rectangle(80,0,40,64), 
                    new Rectangle(120,0,40,64), 
                    new Rectangle(160,0,40,64), 
                    new Rectangle(200,0,40,64), 
                    new Rectangle(240,0,40,64), 
                    new Rectangle(280,0,40,64), 
                    new Rectangle(320,0,40,64), 
                    new Rectangle(360,0,40,64), 
                    new Rectangle(400,0,16,16), 
                    new Rectangle(0,64,192,64)
                }
            );

            BtnList.Add(BtnRetry);
            BtnList.Add(BtnHighscore);
            BtnList.Add(BtnHome);

            mUpdateGameObjects.Add(FishItem);
            mUpdateGameObjects.Add(mBigFish);
        }
       
        #endregion
    }
}
