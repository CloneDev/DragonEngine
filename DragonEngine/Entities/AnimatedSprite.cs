/**************************************************************
 * (c) Carsten Baus 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DragonEngine.Entities
{
    class AnimatedSprite : TiledSprite
    {
        #region Properties
        protected List<int> mFrames = new List<int>();
        protected bool mRepeatAnimation = true;
        protected bool mAnimDone = false;
        protected int mAnimSpeed;
        protected int mAnimElapsedTime = 0;
        protected int mFrameInList = 0;
        #endregion

        #region Getter & Setter
        #endregion

        #region Constructor

        public AnimatedSprite() { }

        public AnimatedSprite(Vector2 pPosition, String pTextureName, int pRectangleWidth, int pRectangleHeight, List<int> pFrames, int pAnimSpeed)
            : base(pPosition, pTextureName, pRectangleWidth, pRectangleHeight)
        {
            mFrames = pFrames;
            mAnimSpeed = pAnimSpeed;
        }

        public AnimatedSprite(Vector2 pPosition, String pTextureName, List<Rectangle> pSourceRectangleList, List<int> pFrames, int pAnimSpeed)
            : base(pPosition, pTextureName, pSourceRectangleList)
        {
            mFrames = pFrames;
            mAnimSpeed = pAnimSpeed;
        }

        public AnimatedSprite(Vector2 pPosition, String pTextureName, int pRectangleWidth, int pRectangleHeight, List<int> pFrames, int pAnimSpeed, bool pIsRepeat)
            : base(pPosition, pTextureName, pRectangleWidth, pRectangleHeight)
        {
            mFrames = pFrames;
            mAnimSpeed = pAnimSpeed;
            mRepeatAnimation = pIsRepeat;
        }

        public AnimatedSprite(Vector2 pPosition, String pTextureName, List<Rectangle> pSourceRectangleList, List<int> pFrames, int pAnimSpeed, bool pIsRepeat)
            : base(pPosition, pTextureName, pSourceRectangleList)
        {
            mFrames = pFrames;
            mAnimSpeed = pAnimSpeed;
            mRepeatAnimation = pIsRepeat;
        }
        #endregion

        #region Methoden

        public override void Update()
        {
            Animate();
        }


        private void Animate()
        {
            mAnimElapsedTime += (EngineSettings.Time.ElapsedGameTime.Milliseconds);
            if (mAnimElapsedTime >= mAnimSpeed)
            {
                mFrameInList++;

                if (mFrameInList == mFrames.Count)
                {
                    mFrameInList = 0;
                    if (!mRepeatAnimation)
                    {
                        CurrentTile = mFrames[mFrames.Count - 1];
                        //mUpdateActionGameTime.Remove(Animate);
                        mAnimDone = true;
                        return;
                    }
                }

                CurrentTile = mFrames[mFrameInList];
                mAnimElapsedTime = 0;
            }
        }
        #endregion
    }
}
