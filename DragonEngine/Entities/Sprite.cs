/**************************************************************
 * (c) Carsten Baus, Jens Richter 2014
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
    public class Sprite : GameObject
    {
        #region Properties

        protected String mTextureName;
        protected Color mTint = Color.White;
        protected int mWidth;
        protected int mHeight;

        protected Vector2 mOrigin;
        protected int mRotation = 0;
        protected SpriteEffects mEffekt = SpriteEffects.None;

        #region Getter & Setter
        
        public String TextureName
        {
            get { return mTextureName; }
            set
            {
                mTextureName = value;
                UpdateTextureDimensions();
            }
        }
        public Color Tint { set { mTint = value; } }
        public int Width { get { return mWidth; } }
        public int Height { get { return mHeight; } }

        public Vector2 Origin { get { return mOrigin; } }
        public int Rotation { get { return mRotation; } set { mRotation = value; } }
        public SpriteEffects Effect { get { return mEffekt; } set { mEffekt = value; } }

        public Texture2D Texture { get { return TextureManager.Instance.GetElementByString<Texture2D>(mTextureName); } }

        #endregion

        #endregion

        #region Constructor

        public Sprite() { }

        public Sprite(Vector2 pPosition, String pTextureName)
            : base(pPosition)
        {
            TextureName = pTextureName;
        }

        #endregion

        #region Methods

        #region Pool

        public void CleanUp()
        {
            TextureName = "pixel";
            Tint = Color.White;
            Rotation = 0;
            Effect = SpriteEffects.None;
        }

        #endregion

        public void UpdateTextureDimensions()
        {
            mWidth = TextureManager.Instance.GetElementByString<Texture2D>(mTextureName).Width;
            mHeight = TextureManager.Instance.GetElementByString<Texture2D>(mTextureName).Height;
            mOrigin = new Vector2(Width / 2, Height / 2);
            mCollisionBox = new Rectangle(PositionX, PositionY, Width, Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.Instance.GetElementByString<Texture2D>(mTextureName), new Rectangle(PositionX + (int)mOrigin.X, PositionY + (int)mOrigin.Y, mWidth, mHeight), new Rectangle(0, 0, mWidth, mHeight), mTint, MathHelper.ToRadians(mRotation), mOrigin, mEffekt, 0.0f);
            if (EngineSettings.IsDebug)
                spriteBatch.Draw(TextureManager.Instance.GetElementByString<Texture2D>("pixel"), new Rectangle(PositionX, PositionY, mWidth, mHeight), mDebugColor);
        }

        #endregion
    }
}
