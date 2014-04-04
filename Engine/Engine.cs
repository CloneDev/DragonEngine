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
    static class Engine
    {
        #region Properties

        public static Dictionary<String, SpinePool> SpinePools = new Dictionary<string, SpinePool>();

        #endregion

        #region Methoden

        /// <summary>
        /// Setup engine-environment.
        /// </summary>
        public static void Setup()
        {
            //Settings
            SpineSettings.Setup();
            //Pools
            SpinePools.Add("fluffy", new SpinePool("fluffy"));
        }

        /// <summary>
        /// Load basic engine-content.
        /// </summary>
        public static void Load()
        {
            //Manager
            TextureManager.Instance.LoadContent();
            SpineManager.Instance.LoadContent();
            SceneManager.Instance.LoadContent();
        }

        #endregion

    }
}
