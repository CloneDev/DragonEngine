using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DragonEngine.Entities;
using Microsoft.Xna.Framework;

namespace SnakeMobile.GameContent
{
    class DSpriteFont : TiledSprite
    {
        #region Properties
        #endregion

        #region Getter & Setter
        #endregion

        #region Constructor
        public DSpriteFont(Vector2 pPosition, String pTextureName, List<Rectangle> pSourceRectangleList)
            : base(pPosition, pTextureName, pSourceRectangleList)
        {
            for (int i = 0; i < pSourceRectangleList.Count; i++)
                mSourceRectangle[i] = pSourceRectangleList[i];
        }
        #endregion

        #region Methoden
        #endregion

    }
}
