/**************************************************************
 * (c) Carsten Baus 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using DragonEngine.SceneManagement;
using Microsoft.Xna.Framework;

namespace DragonEngine.Manager
{
    public class TextureManager : Manager
    {
        #region Singleton

        private static TextureManager mInstance;
        public static TextureManager Instance { get { if (mInstance == null) mInstance = new TextureManager(); return mInstance; } }

        #endregion

        #region Constructor

        TextureManager() { }

        #endregion

        #region Methods

        public override void LoadContent()
        {
            if(!mRessourcen.ContainsKey("pixel"))
                mRessourcen.Add("pixel", EngineSettings.Content.Load<Texture2D>(@"gfx\pixel"));
        }

        /// <summary>
        /// Fügt ein neues Element in mRessourcenManager ein.
        /// </summary>
        /// 
        /// <param name="pName">ID der Texture für den Zugriff.</param>
        /// <param name="pPath">Pfad zur Texture.</param>
        public override Texture2D Add<Texture2D>(String pName, String pPath)
        {
            Texture2D tex;
            if (!mRessourcen.ContainsKey(pName))
            {
                tex = EngineSettings.Content.Load<Texture2D>(pPath);
                mRessourcen.Add(pName, tex);
            }
            else
                tex = (Texture2D)mRessourcen[pName];

            return tex;
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

        public Dictionary<String, T> GetAllEntitys<T>()
        {
            Dictionary<String, T> result = new Dictionary<String, T>();

            foreach (KeyValuePair<string, object> pair in mRessourcen)
            {
                if (pair.Key.IndexOf("Engine") == -1)
                    result.Add(pair.Key, (T)pair.Value);
            }
            return result;
        }

        #endregion
    }
}
