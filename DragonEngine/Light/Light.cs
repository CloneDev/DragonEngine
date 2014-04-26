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
        #region Properties
        private float mIntesity;
        private Vector3 mColor;
        #endregion

        #region Constructor
        public Light( float pIntesity, Vector3 pColor)
        {
            this.mIntesity = pIntesity;
            this.mColor = pColor;
        }
        public Light(float pIntesity)
        {
            this.mIntesity = pIntesity;
            this.mColor = Vector3.Zero;
            
        }
        public Light()
        {
            this.mIntesity = 1.0f;
            this.mColor = Vector3.Zero;
        }
        #endregion

        #region Methods
        public virtual void Update() { }
        #endregion

    }
}
