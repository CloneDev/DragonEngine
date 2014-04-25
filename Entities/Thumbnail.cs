using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DragonEngine.Manager;
using Microsoft.Xna.Framework.Graphics;

namespace DragonEngine.Entities
{
    public class Thumbnail : Sprite
    {
        #region Properties

        private const int THUMBNAIL_WIDTH = 64;
        private const int THUMBNAIL_HEIGHT = 64;
        #endregion

        #region Getter & Setter
        #endregion

        #region Constructor
        public Thumbnail() { }

        public Thumbnail(Vector2 pPosition, String pTextureName, String pPathName)
            : base(pPosition, pTextureName, pPathName)
        {
        }

        public Thumbnail(Vector2 pPosition, String pTextureName)
            : base(pPosition, pTextureName)
        {
        }

        #endregion

        #region Override Methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mTexture, new Rectangle(PositionX, PositionY, THUMBNAIL_WIDTH, THUMBNAIL_HEIGHT), Color.White);
            if (EngineSettings.IsDebug)
                spriteBatch.Draw(mTexture, new Rectangle(PositionX, PositionY, mWidth, mHeight), mDebugColor);
        }
        #endregion
    }
}
