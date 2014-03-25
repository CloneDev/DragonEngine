using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DragonEngine.Entities;

namespace SnakeMobile.GameContent
{
    class Stone : AnimatedSprite
    {
        #region Properties
        #endregion

        #region Getter & Setter
        #endregion

        #region Constructor

        public Stone() : base() { }

        public Stone(Vector2 pPosition, String pTextureName, int pRectangleWidth, int pRectangleHeight, List<int> pFrames, int pAnimSpeed, bool pIsRepeat)
            : base(pPosition, pTextureName, pRectangleWidth, pRectangleHeight, pFrames, pAnimSpeed, pIsRepeat)
        {
        }
        #endregion

        #region Methoden
        #endregion
    }
}
