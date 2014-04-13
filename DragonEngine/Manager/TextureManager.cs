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

        TextureManager()
        {
        }

        #endregion

        #region Methods

        public override void LoadContent()
        {
            mRessourcen.Add("pixel", EngineSettings.Content.Load<Texture2D>(@"gfx\pixel"));
        }

        /// <summary>
        /// Fügt ein neues Element in mRessourcenManager ein.
        /// </summary>
        /// <param name="pName">ID der Texture für den Zugriff.</param>
        /// <param name="pPath">Pfad zur Texture.</param>
        public override void Add(String pName, String pPath)
        {
            if (!mRessourcen.ContainsKey(pName))
            {
                Texture2D tex = EngineSettings.Content.Load<Texture2D>(pPath);
                mRessourcen.Add(pName, tex);
            }
        }

        //public override void LoadStringDictionary(Dictionary<string, string> pLoadDictionary)
        //{
        //    foreach (KeyValuePair<String, String> pair in pLoadDictionary)
        //    {
        //        if (!mRessourcen.ContainsKey(pair.Key))
        //        {
        //            Texture2D tex = EngineSettings.EngineContent.Load<Texture2D>(pair.Value);
        //            mRessourcen.Add(pair.Key, tex);
        //        }
        //    }
        //}

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

        //public override void UnloadStringList(List<string> pUnloadList)
        //{
        //    foreach (String s in pUnloadList)
        //    {
        //        if(mRessourcen.ContainsKey(s))
        //            mRessourcen.Remove(s);
        //    }
        //}

        #endregion
    }
}
