/**************************************************************
 * (c) Jens Richter 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DragonEngine.Manager;
using DragonEngine.SceneManagement;
using DragonEngine.Pool;

namespace DragonEngine
{
    class Dragon
    {
        #region Singleton
        private static Dragon mInstance;
        public static Dragon Engine { get { if (mInstance == null) mInstance = new Dragon(); return mInstance; } }
        #endregion

        #region Properties

        public Dictionary<String, SpinePool> SpinePools;

        #endregion

        #region Constructor

        public Dragon()
        {

        }

        #endregion

        #region Methoden

        public void Load()
        {
            //Load Settings
            SpineSettings.LoadSettings();
            //Load Manager
            TextureManager.Instance.LoadContent();
            SpineManager.Instance.LoadContent();
            SceneManager.Instance.LoadContent();
            //Load Pools
            SpinePools.Add("fluffy", new SpinePool("fluffy"));
        }

        #endregion

    }
}
