/**************************************************************
 * (c) Joss Lattner 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DragonEngine.Light
{
    class Light
    {
        #region Proberties
        private Vector3 mPosition;
        private float mIntesity;
        private Vector3 mColor;
        #endregion

        #region Constructor
        public Light(Vector3 pPosition, float pIntesity, Vector3 pColor)
        {
            this.mPosition = pPosition;
            this.mIntesity = pIntesity;
            this.mColor = pColor;
        }
        public Light(Vector3 pPosition, float pIntesity)
        {
            this.mPosition = pPosition;
            this.mIntesity = pIntesity;
            this.mColor = Vector3.Zero;
            
        }
        public Light(Vector3 pPosition)
        {
            this.mPosition = pPosition;
            this.mIntesity = 1.0f;
            this.mColor = Vector3.Zero;
        
        }
        public Light()
        {
            this.mPosition = Vector3.Zero;
            this.mIntesity = 1.0f;
            this.mColor = Vector3.Zero;
        }
        #endregion

        #region Methods
        public virtual void Update() { }
        #endregion

    }
}
