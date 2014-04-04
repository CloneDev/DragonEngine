/**************************************************************
 * (c) Carsten Baus 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DragonEngine.Entities;
using Microsoft.Xna.Framework;
using DragonEngine.Manager;
using Microsoft.Xna.Framework.Graphics;

namespace DragonEngine.Entities
{
    public class TiledSprite : Sprite
    {
        #region Properties

        protected Rectangle[] mSourceRectangle;
        protected int mSourceRectanglePosition = 0;
        protected int mSourceRectangleHeight;
        protected int mSourceRectangleWidth;
        #endregion

        #region Getter & Setter

        public int CurrentTile { get { return mSourceRectanglePosition; } 
            set { 
                if (value < mSourceRectangle.Length && value >= 0 ) 
                    mSourceRectanglePosition = value; 
                else mSourceRectanglePosition = 0;
            } 
        }

        public int CurrentTileWidth { get { return mSourceRectangle[mSourceRectanglePosition].Width; } }
        public int CurrentTileHeight { get { return mSourceRectangle[mSourceRectanglePosition].Height; } }
        #endregion

        #region Constructor

        public TiledSprite() { }

        public TiledSprite(Vector2 pPosition, String pTextureName, int pRectangleWidth, int pRectangleHeight)
            : base(pPosition, pTextureName)
        {
            mSourceRectangleWidth = pRectangleWidth;
            mSourceRectangleHeight = pRectangleHeight;

            int rectangleRows = //(mWidth % pRectangleHeight == 0) ? (int)(mWidth / pRectangleWidth) : (int)(mWidth / pRectangleWidth)
                (int)(mWidth / pRectangleWidth);

            int rectangleColumns = (int)(mHeight / pRectangleHeight);

            mSourceRectangle = new Rectangle[rectangleColumns * rectangleRows];

            for (int y = 0; y < rectangleRows; y++)
                for (int x = 0; x < rectangleColumns; x++)
                    mSourceRectangle[y * rectangleRows + x] = new Rectangle(x * pRectangleWidth, y * pRectangleHeight, pRectangleWidth, pRectangleHeight);
        }

        public TiledSprite(Vector2 pPosition, String pTextureName, List<Rectangle> pSourceRectangleList)
            : base(pPosition, pTextureName)
        {
            mSourceRectangle = new Rectangle[pSourceRectangleList.Count];

            for (int i = 0; i < pSourceRectangleList.Count; i++)
                mSourceRectangle[i] = pSourceRectangleList[i];
        }
        #endregion

        #region Methoden

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.Instance.GetElementByString<Texture2D>(mTextureName), Position, mSourceRectangle[mSourceRectanglePosition], mTint);
            if (EngineSettings.IsDebug)
                spriteBatch.Draw(TextureManager.Instance.GetElementByString<Texture2D>("pixel"), new Rectangle((int)mPosition.X, (int)mPosition.Y, mSourceRectangleWidth, mSourceRectangleHeight), mDebugColor);
        }

        public Texture2D GetTileTexture2D(int pTile)
        {
            Color[] imageData = new Color[mWidth * mHeight];
            TextureManager.Instance.GetElementByString<Texture2D>(mTextureName).GetData<Color>(imageData);

            Color[] color = new Color[mSourceRectangle[pTile].Width * mSourceRectangle[pTile].Height];
            for (int x = 0; x < mSourceRectangle[pTile].Width; x++)
                for (int y = 0; y < mSourceRectangle[pTile].Height; y++)
                    color[x + y * mSourceRectangle[pTile].Width] = imageData[x + mSourceRectangle[pTile].X + (y + mSourceRectangle[pTile].Y) * TextureManager.Instance.GetElementByString<Texture2D>(mTextureName).Width];

            Texture2D subtexture = new Texture2D(EngineSettings.Graphics.GraphicsDevice, mSourceRectangle[pTile].Width, mSourceRectangle[pTile].Height);
            subtexture.SetData<Color>(color);

            return subtexture;
        }
        #endregion
    }
}
