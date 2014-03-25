using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Spine;
//using DragonEngine.SceneManagement;


namespace DragonEngine.Manager
{
    class SpineManager : Manager
    {
        public struct RawSpineData
        {
            #region Properties
            public SkeletonRenderer skeletonRenderer;
            public Atlas atlas;
            public SkeletonJson json;
            #endregion

            #region Constructor
            public RawSpineData(string pSkeletonName)
            {
                skeletonRenderer = new SkeletonRenderer(EngineSettings.Graphics.GraphicsDevice);
                skeletonRenderer.PremultipliedAlpha = SpineSettings.PremultipliedAlphaRendering;
                atlas = new Atlas(SpineSettings.DefaultDataPath + pSkeletonName + ".atlas", new XnaTextureLoader(EngineSettings.Graphics.GraphicsDevice));
                json = new SkeletonJson(atlas);
            }

            public RawSpineData(string pSkeletonDataPath, string pSkeletonName)
            {
                skeletonRenderer = new SkeletonRenderer(EngineSettings.Graphics.GraphicsDevice);
                skeletonRenderer.PremultipliedAlpha = SpineSettings.PremultipliedAlphaRendering;
                atlas = new Atlas(pSkeletonDataPath + pSkeletonName + ".atlas", new XnaTextureLoader(EngineSettings.Graphics.GraphicsDevice));
                json = new SkeletonJson(atlas);
            }
            #endregion
            


        }

        #region Singleton
        private static SpineManager mInstance;
        public static SpineManager Instance { get { if (mInstance == null) mInstance = new SpineManager(); return mInstance; } }
        #endregion

        #region Constructor
        SpineManager()
        {
        }
        #endregion

        #region Methoden


        public override void Initialize()
        {

        }

        /// <summary>
        /// Fügt ein neues Element in mRessourcenManager ein.
        /// </summary>
        /// <param name="pName">Name des Skeletons.</param>
        /// <param name="pPath">Pfad zu den Skeleton-Daten.</param>
        public override void Add(String pName, String pPath)
        {
            if (!mRessourcen.ContainsKey(pName))
            {
                RawSpineData data = new RawSpineData(pPath, pName);
                mRessourcen.Add(pName, data);
            }
        }

        /// <summary>
        /// Fügt ein neues Element in mRessourcenManager ein.
        /// </summary>
        /// <param name="pName">Name des Skeletons.</param>
        public void Add(String pName)
        {
            if (!mRessourcen.ContainsKey(pName))
            {
                RawSpineData data = new RawSpineData(pName);
                mRessourcen.Add(pName, data);
            } 
        }

        /// <summary>
        /// Gibt RawSpineData zurück.
        /// </summary>
        public override T GetElementByString<T>(string pElementName)
        {
            if (mRessourcen.ContainsKey(pElementName))
                return (T)mRessourcen[pElementName];
            else
                return (T)new object();
        }

        public override void Unload()
        {
            mRessourcen.Clear();
        }

        #endregion
    }
}
