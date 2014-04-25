/**************************************************************
 * (c) Carsten Baus 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DragonEngine.Entities
{
    public class Camera : BaseObject
    {
        #region Properties

        private Vector2 mPosition;
        private Rectangle mViewport;
        private Rectangle mGameScreen;
        #endregion

        #region Getter & Setter
        public Vector2 Position { get { return mPosition; } set { mPosition = value; } }
        #endregion

        #region Constructor

        public Camera()
        {
            mPosition = Vector2.Zero;
            Initialize();
        }

        public Camera(Vector2 pPosition)
        {
            mPosition = pPosition;
            Initialize();
        }

        public Camera(Vector2 pPosition, Rectangle pGameScreen)
        {

        }

        #endregion

        #region Override Methods

        public override void Initialize()
        {
            mViewport = new Rectangle(0, 0, EngineSettings.WindowWidth, EngineSettings.WindowHeight);
        }

        public override void Update()
        {
            
        }

        #endregion

        #region Methods

        public Matrix GetTranslationMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(mPosition, 0));
        }
        #endregion

    }
}
