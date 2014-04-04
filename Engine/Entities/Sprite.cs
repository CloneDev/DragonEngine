/**************************************************************
 * (c) Carsten Baus 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DragonEngine.Manager;

namespace DragonEngine.Entities
{
    class Sprite : GameObject
    {
        #region Properties

        protected String mTextureName;
        protected Color mTint = Color.White;
        protected int mWidth;
        protected int mHeight;

        protected Vector2 mOrigin;
        protected int mRotation = 0;
        protected SpriteEffects mEffekt = SpriteEffects.None;

        protected Rectangle mSpriteBox;
        #endregion

        #region Getter & Setter
        
        public String TextureName { get { return mTextureName; } set { mTextureName = value; } }
        public Color Tint { set { mTint = value; } }
        public int Width { get { return mWidth; } }
        public int Height { get { return mHeight; } }

        public Vector2 Origin { get { return mOrigin; } }
        public int Rotation { get { return mRotation; } set { mRotation = value; } }
        public SpriteEffects Effect { get { return mEffekt; } set { mEffekt = value; } }

        public Rectangle SpriteBox { get { return mSpriteBox; } }
        #endregion

        #region Constructor

        public Sprite() { }

        public Sprite(Vector2 pPosition, String pTextureName)
            : base(pPosition)
        {
            mTextureName = pTextureName;

            mWidth = TextureManager.Instance.GetElementByString<Texture2D>(mTextureName).Width;
            mHeight = TextureManager.Instance.GetElementByString<Texture2D>(mTextureName).Height;

            mOrigin = new Vector2(mWidth / 2, mHeight / 2);

            mSpriteBox = new Rectangle((int)pPosition.X, (int)pPosition.Y, mWidth, mHeight);
        }
        #endregion

        #region Methoden

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.Instance.GetElementByString<Texture2D>(mTextureName), new Rectangle((int)mPosition.X + (int)mOrigin.X, (int)mPosition.Y + (int)mOrigin.Y, mWidth, mHeight), new Rectangle(0, 0, mWidth, mHeight), mTint, MathHelper.ToRadians(mRotation), mOrigin, mEffekt, 0.0f);
            if (EngineSettings.IsDebug)
                spriteBatch.Draw(TextureManager.Instance.GetElementByString<Texture2D>("pixel"), new Rectangle((int)mPosition.X, (int)mPosition.Y, mWidth, mHeight), mDebugColor);
        }

        #endregion
    }
}
