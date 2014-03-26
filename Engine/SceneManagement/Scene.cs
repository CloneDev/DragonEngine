using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DragonEngine.Manager;
using DragonEngine.Entities;

namespace DragonEngine.SceneManagement
{
    public class Scene
    {
        #region Properties

        protected String mName;
        protected String mBackgroundName = "pixel";
        protected SpriteBatch mSpriteBatch;
        protected RenderTarget2D mRenderTarget;
        protected Camera mCamera;

        protected Color mClearColor = Color.LawnGreen;

        // String1 für AssetName, String2 für AssetPfad
        protected List<GameObject> mUpdateGameObjects = new List<GameObject>();
        protected List<Action<GameTime>> mUpdateAction = new List<Action<GameTime>>();
        protected List<Action> mDrawAction = new List<Action>();
        #endregion

        #region Getter & Setter
        public String Name { get { return this.mName; } }
        public List<GameObject> UpdateGameObjects { get { return mUpdateGameObjects; } }
        public String Background { set { mBackgroundName = value; mClearColor = Color.White; } }
        #endregion

        #region Constructor

        public Scene()
        {
        }
        public Scene(String pSceneName)
        {
            this.mName = pSceneName;
        }
        #endregion

        #region Virtual Methods

        public virtual void Initialize()
        {
            mRenderTarget = new RenderTarget2D(EngineSettings.Graphics.GraphicsDevice, EngineSettings.VirtualResX, EngineSettings.VirtualResY);
            mSpriteBatch = new SpriteBatch(EngineSettings.Graphics.GraphicsDevice);
        }

        public virtual void LoadContent()
        {

        }

        /// <summary>
        /// Updatet Funktionen und GameObjects
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            for (int i = 0; i < mUpdateGameObjects.Count; i++)
                mUpdateGameObjects[i].Update(gameTime);

            for (int i = 0; i < mUpdateAction.Count; i++)
                mUpdateAction[i](gameTime);
        }

        public virtual void Draw()
        {
            EngineSettings.Graphics.GraphicsDevice.SetRenderTarget(mRenderTarget);

            mSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, null, null, null, null, mCamera.ViewportTransform);
            mSpriteBatch.Draw(TextureManager.Instance.GetElementByString<Texture2D>(mBackgroundName), new Rectangle(0, 0, EngineSettings.VirtualResX, EngineSettings.VirtualResY), mClearColor);
            mSpriteBatch.End();

            for(int i = 0; i < mDrawAction.Count; i++)
                mDrawAction[i]();

            mSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, null, null, null, null, mCamera.ViewportTransform);
            foreach (GameObject go in mUpdateGameObjects)
                go.Draw(mSpriteBatch);
            mSpriteBatch.End();

            EngineSettings.Graphics.GraphicsDevice.SetRenderTarget(null);

            mSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, mCamera.ScreenTransform);
            mSpriteBatch.Draw(mRenderTarget, new Rectangle(0, 0, EngineSettings.VirtualResX, EngineSettings.VirtualResY), Color.White);
            mSpriteBatch.End();

        }

        /// <summary>
        /// Fügt ein zusätzliches GameObject hinzu, das geupdatet wird.
        /// </summary>
        /// <param name="go"></param>
        protected void AddGameObjectToScene(GameObject go)
        {
            mUpdateGameObjects.Add(go);
        }
        #endregion

    }
}
