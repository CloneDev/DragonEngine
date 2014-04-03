﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DragonEngine.Entities;

namespace DragonEngine.Pool
{
    class SpinePool : Pool<SpineObject>
    {

        #region Properties

        private string mName;

        #endregion

        #region Constructor

        public SpinePool(string pName) : base()
        {
            mName = pName;
        }

        #endregion

        #region Methoden

        protected override SpineObject CreateInstance()
        {
            return new SpineObject(mName);
        }

        protected override void CleanUpInstance(SpineObject pObject)
        {
            pObject.CleanUp();
        }

        #endregion

    }
}
