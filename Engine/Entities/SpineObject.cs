using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DragonEngine.Entities;
using DragonEngine.Manager;
using Spine;

namespace DragonEngine.Entities
{
    class SpineObject : BaseObject
    {
        #region Properties

        private SkeletonRenderer mSkeletonRenderer;
        private Skeleton mSkeleton;
        private AnimationState mAnimationState;
        private SkeletonBounds mBounds;

        private string mName;
        private Vector2 mInitPosition;
        private float mScale;
        

        #endregion

        #region Get_Set

        public string Name { get { return mName; } }
        public Vector2 Position { get { return new Vector2(mSkeleton.X, mSkeleton.Y); } set { mSkeleton.X = value.X; mSkeleton.Y = value.Y; } }
        public float Scale { get { return mScale; } }
        public bool Flip { get { return mSkeleton.FlipX; } set { mSkeleton.FlipX = value; } }
        public bool FlipY { get { return mSkeleton.FlipY; } set { mSkeleton.FlipY = value; } }
        public Skeleton Skeleton { get { return mSkeleton; } }
        public AnimationState AnimationState { get { return mAnimationState; } }//set { mAnimationState = value; } }

        #endregion

        #region Constructor

        public SpineObject(string pName)
        {
            Initialize();
            mName = pName;
        }

        public SpineObject(string pName, float pScale)
        {
            Initialize();
            mName = pName;
            mScale = pScale;
        }

        public SpineObject(string pName, Vector2 pPosition)
        {
            Initialize();
            mName = pName;
            mInitPosition = pPosition;
        }

        public SpineObject(string pName, Vector2 pPosition, float pScale)
        {
            Initialize();
            mName = pName;
            mInitPosition = pPosition;
            mScale = pScale;
        }

        #endregion

        #region Methoden

        public void Initialize()
        {
            mSkeletonRenderer = new SkeletonRenderer(EngineSettings.Graphics.GraphicsDevice);
            mBounds = new SkeletonBounds();
            mInitPosition = new Vector2(0, 0);
            mScale = 1.0f;
            //mUpdateActionGameTime.Add(UpdateAnimation);
        }

        public void Load()
        {
            mSkeleton = SpineManager.Instance.NewSkeleton(mName, mScale);
            mSkeleton.SetSlotsToSetupPose(); // Without this the skin attachments won't be attached. See SetSkin.
            mAnimationState = SpineManager.Instance.NewAnimationState(mSkeleton.Data);
            mSkeleton.X = mInitPosition.X;
            mSkeleton.Y = mInitPosition.Y;
        }

        public void UpdateAnimation(GameTime gameTime)
        {
            //Player -> Drawposition
            //skeleton.X = position.X - camera.viewport.X;
            //skeleton.Y = position.Y - camera.viewport.Y;
            //skeleton.Update(gameTime.ElapsedGameTime.Milliseconds / 1000f);
            mAnimationState.Update(gameTime.ElapsedGameTime.Milliseconds / 1000f);
            mAnimationState.Apply(mSkeleton);
            mSkeleton.UpdateWorldTransform();
        }

        public void Draw()
        {
            mSkeletonRenderer.Begin();
            mSkeletonRenderer.Draw(mSkeleton);
            mSkeletonRenderer.End();
            //Player -> Worldposition
            //skeleton.X = position.X;
            //skeleton.Y = position.Y;
        }

        private bool BoundingBoxCollision(Rectangle cbox) //Checken ob Rectangle mit bb-Attachement (z.B. Keule) kollidiert
        {
            mBounds.Update(mSkeleton, true);
            bool collision = false;
            if (mBounds.AabbIntersectsSegment(cbox.X, cbox.Y, cbox.X, cbox.Y + cbox.Height)
                || mBounds.AabbIntersectsSegment(cbox.X + cbox.Width, cbox.Y, cbox.X + cbox.Width, cbox.Y + cbox.Height)
                || mBounds.AabbIntersectsSegment(cbox.X, cbox.Y, cbox.X + cbox.Width, cbox.Y)
                || mBounds.AabbIntersectsSegment(cbox.X, cbox.Y + cbox.Height, cbox.X + cbox.Width, cbox.Y + cbox.Height)
                )
            {
                if (mBounds.IntersectsSegment(cbox.X, cbox.Y, cbox.X, cbox.Y + cbox.Height) != null
                    ||
                    mBounds.IntersectsSegment(cbox.X + cbox.Width, cbox.Y, cbox.X + cbox.Width, cbox.Y + cbox.Height) != null
                    ||
                    mBounds.IntersectsSegment(cbox.X, cbox.Y, cbox.X + cbox.Width, cbox.Y) != null
                    ||
                    mBounds.IntersectsSegment(cbox.X, cbox.Y + cbox.Height, cbox.X + cbox.Width, cbox.Y + cbox.Height) != null
                    )
                {
                    collision = true;
                }
            }
            return collision;
        }

        #endregion
    }
}
