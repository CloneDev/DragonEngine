﻿/**************************************************************
 * (c) Carsten Baus 04.03.2014 
 * 
 * 
 * 
 *************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DragonEngine.Manager;
using DragonEngine;

namespace DragonEngine.SceneManagement
{
    public class SceneManager
    {
        #region Singleton

        private static SceneManager mInstance;

        public static SceneManager Instance { get { if (mInstance == null) mInstance = new SceneManager(); return mInstance; } }
        #endregion

        #region Properties

        private static Scene mCurrentScene;
        private static Scene mLastScene;
        private static Scene mNextScene;

        // Dictionary für Scene
        private Dictionary<String, Scene> mSceneDictionary = new Dictionary<String, Scene>();

        #region FadeToColor
        // FadeSpeed 1 / 30 statt 1 / 60 für die dauer da in beide Richtungen gefadet wird.
        // Eine Richtung dauert dann die hälfte der angegebenen Zeit.
        private float mFadeSpeed = 1.0f / 30.0f / 0.5f; // Default Zeit : 0.5 sekunden.
        private static Color mFadeColor = Color.Black; // Default Farbe : Schwarz.
        private float mFadeAlpha = 0.0f;
        private bool mFadeActiv = false;

        #endregion

        #endregion

        #region Getter & Setter

        public static Scene CurrentScene { get { return mCurrentScene; } }
        public static Color FadeColor { set { mFadeColor = value; } }
        public float FadeSpeed { set { mFadeSpeed = 1.0f / 30.0f / value; } }
        #endregion

        #region Constructor

        public SceneManager()
        {
        }
        #endregion

        #region Methoden

        public void Initialize()
        {
            for (int i = 0; i < mSceneDictionary.Count; i++)
                mSceneDictionary.ElementAt(i).Value.Initialize();
        }

        public void LoadContent()
        {
            foreach (KeyValuePair<string, Scene> pair in mSceneDictionary)
                pair.Value.LoadContent();
        }

        /// <summary>
        /// Fügt eine neue Scene zum SceneManager hinzu. Die übergebene Scene muss von Scene erben.
        /// </summary>
        /// <param name="pScene"></param>
        public void AddScene(Scene pScene)
        {
            mSceneDictionary.Add(pScene.Name, pScene);
        }

        public Scene GetScene(String pName)
        {
            if (mSceneDictionary.ContainsKey(pName))
                return mSceneDictionary[pName];
            else
                return null;
        }
        /// <summary>
        /// Setz die Scene zu dem angegebenen Namen der übergeben wird.
        /// Immer als erstes nach dem erstellen der Scenen aufrufen bevor 
        /// SetCurrentSceneTo aufgerufen wird.
        /// </summary>
        /// <param name="pSceneName"></param>
        public void SetStartSceneTo(String pSceneName)
        {
            if (mSceneDictionary.ContainsKey(pSceneName))
                mCurrentScene = mSceneDictionary[pSceneName];
            else
                Console.WriteLine("Scene nicht im SceneManager enthalten.");
        }

        /// <summary>
        /// Setzt die Scene zur Angegebenen Scene
        /// </summary>
        /// <param name="pSceneName"></param>
        public void SetCurrentSceneTo(String pSceneName)
        {
            if (mSceneDictionary.ContainsKey(pSceneName) && !mCurrentScene.Name.Equals(pSceneName))
            {
                mNextScene = mSceneDictionary[pSceneName];
                mFadeActiv = true;
            }
            else
                Console.WriteLine("Scene nicht im SceneManager enthalten.");
        }

        public void Update()
        {
            if (!mFadeActiv)
                mCurrentScene.Update();
            else
                FadeColorScene();
        }

        public void Draw(SpriteBatch spritebatch)
        {
            mCurrentScene.Draw();

            if (mFadeActiv)
            {
                spritebatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                spritebatch.Draw((Texture2D)TextureManager.Instance.GetElementByString<Texture2D>("pixel"), new Rectangle(0, 0, EngineSettings.Graphics.PreferredBackBufferWidth, EngineSettings.Graphics.PreferredBackBufferHeight), mFadeColor * mFadeAlpha);
                spritebatch.End();
            }
        }

        /// <summary>
        /// Fadet in die gewählte Farbe, Geschwindigkeit und Farbe sind dynamisch wechselbar.
        /// </summary>
        protected void FadeColorScene()
        {
            mFadeAlpha += mFadeSpeed;
            if (mFadeAlpha > 1)
            {
                mLastScene = mCurrentScene;
                mCurrentScene = mNextScene;
                mNextScene = null;
                mFadeSpeed *= -1;
            }
            else if (mFadeAlpha < 0)
            {
                mFadeSpeed *= -1;
                mFadeActiv = false;
            }
        }
        #endregion
    }
}
