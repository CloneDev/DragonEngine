/**************************************************************
 * (c) Carsten Baus 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DragonEngine.Entities
{
    public class GameObject : BaseObject
    {
        #region Properties

        private Vector2 mPosition;
        /// <summary>
        /// Kollisionsbox des Objekts relativ zum Objekt.
        /// </summary>
        protected Rectangle mCollisionBox = new Rectangle();

        protected Color mDebugColor = Color.OrangeRed;
        #endregion

        #region Getter & Setter
        public Vector2 Position { set { mPosition = value; } get { return mPosition; } }
        public int PositionX { set { mPosition.X = value; } get { return (int)mPosition.X; } }
        public int PositionY { set { mPosition.Y = value; } get { return (int)mPosition.Y; } }
        /// <summary>
        /// Kollisionsbox des Objekts an den Weltkoordinaten.
        /// </summary>
        public Rectangle CollisionBox { get { return new Rectangle(mCollisionBox.X + PositionX, mCollisionBox.Y + PositionY, mCollisionBox.Width, mCollisionBox.Height); } }

        #endregion

        #region Constructor

        public GameObject()
            : base()
        {
        }

        public GameObject(Vector2 pPosition)
            : base()
        {
            Position = pPosition;
        }
        #endregion

        #region Virtual Methoden
        public virtual void Draw(SpriteBatch spriteBatch) { }
        #endregion
    }
}
