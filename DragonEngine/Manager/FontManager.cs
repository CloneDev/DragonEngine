﻿/**************************************************************
 * (c) Carsten Baus 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace DragonEngine.Manager
{
    class FontManager : Manager
    {
         #region Singleton

        private static FontManager mInstance;
        public static FontManager Instance { get { if (mInstance == null) mInstance = new FontManager(); return mInstance; } }

        #endregion

        #region Constructor

        FontManager() { }

        #endregion

        #region Methods

        public override void LoadContent()
        {
            if(!mRessourcen.ContainsKey("MenueFont"))
                mRessourcen.Add("MenueFont", EngineSettings.Content.Load<SpriteFont>(@"font\font"));
        }

        /// <summary>
        /// Fügt ein neues Element in mRessourcenManager ein.
        /// </summary>
        /// 
        /// <param name="pName">ID der Texture für den Zugriff.</param>
        /// <param name="pPath">Pfad zur Texture.</param>
        public override SpriteFont Add<SpriteFont>(String pName, String pPath)
        {
            SpriteFont font;
            if (!mRessourcen.ContainsKey(pName))
            {
                font = EngineSettings.Content.Load<SpriteFont>(pPath);
                mRessourcen.Add(pName, font);
            }
            else
                font = (SpriteFont)mRessourcen[pName];

            return font;
        }

        /// <summary>
        /// Gibt eine Texture2D zurück.
        /// </summary>
        public override T GetElementByString<T>(string pElementName)
        {
            if (mRessourcen.ContainsKey(pElementName))
                return (T) mRessourcen[pElementName];
            else
                return (T) new object();
        }

        public override void Unload()
        {
            mRessourcen.Clear();
        }

        #endregion
    }
}
