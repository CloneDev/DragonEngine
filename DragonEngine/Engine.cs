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
using DragonEngine.Pools;

namespace DragonEngine
{
    public static class Engine
    {
        #region Properties

        #endregion

        #region Methoden

        /// <summary>
        /// Setup engine-environment.
        /// </summary>
        public static void Setup()
        {
            //Settings
            SpineSettings.Setup();
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
