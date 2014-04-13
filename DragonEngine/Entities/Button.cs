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
    class Button : Sprite
    {
        #region Proberties

        private int mBtnId;
        public Action OnButtonPressed;
        #endregion

        #region Getter & Setter
        public int ButtonId { get { return mBtnId; } set { mBtnId = value; } }
        #endregion

        #region Constructor

        public Button() { }

        public Button(Vector2 pPosition, String pTextureName)
            : base(pPosition, pTextureName)
        {
            TextureName = pTextureName;
        }
        #endregion

        #region Methods

        public void IsButtonPressed(Vector2 pClickArea)
        {
            if(mCollisionBox.Contains((int)pClickArea.X, (int)pClickArea.Y) && OnButtonPressed != null)
                OnButtonPressed();
        }

        #endregion
    }
}
