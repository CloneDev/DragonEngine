using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DragonEngine.Entities
{
    public class ParallaxPlane : GameObject
    {
        #region Properties

        protected Vector2 mSize;

        #region Get&Set

        public Vector2 Size { get { return mSize; } }
        public int PositionX { set { mSize.X = value; } get { return (int)mSize.X; } }
        public int PositionY { set { mSize.Y = value; } get { return (int)mSize.Y; } }
        public int Width { get { return (int)mSize.X; } }
        public int Height { get { return (int)mSize.Y; } }

        #endregion

        #endregion

        #region Constructor

        public ParallaxPlane()
            : base()
        {  
        }

        public ParallaxPlane(Vector2 pPosition)
            : base(pPosition)
        {
        }

        #endregion

        #region Methoden

        /// <summary>
        /// Updated die Ebenenverschiebung anhand der übergebenen Kamera, relativ zu Viewport und Viewarea.
        /// </summary>
        /// <param name="pCamera">Zu verwendende Kamera.</param>
        public override void Update(Camera pCamera)
        {
            PositionX = pCamera.PositionX - ((Width - pCamera.Width) * ((pCamera.PositionX - pCamera.ViewArea.X) / (pCamera.ViewArea.Width - pCamera.Width)));
            PositionY = pCamera.PositionY - ((Height - pCamera.Height) * ((pCamera.PositionY - pCamera.ViewArea.Y) / (pCamera.ViewArea.Height - pCamera.Height)));
        }

        #endregion


    }
}
