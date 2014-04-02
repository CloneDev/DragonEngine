using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DragonEngine.Entities;

namespace DragonEngine.Pool
{
    class SpinePool : Pool<SpineObject>
    {

        #region Singleton

        private static Dictionary<String, SpinePool> mPoolInstance;

        public static Dictionary<String, SpinePool> Pools
        {
            get
            {
                if (mPoolInstance == null)
                {
                    mPoolInstance = new Dictionary<string, SpinePool>();
                    mPoolInstance.Add("fluffy", new SpinePool("fluffy"));
                }
                return mPoolInstance;
            }
        }

        #endregion

        #region Properties

        private string mName;

        #endregion

        #region Getter & Setter
        #endregion

        #region Constructor

        public SpinePool(string pName)
            //:base()
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
