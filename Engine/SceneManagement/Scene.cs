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

        protected Color mClearColor = Color.LawnGreen;

        // String1 für AssetName, String2 für AssetPfad
        protected Dictionary<String, String> mTexture2DStringList = new Dictionary<string, string>();
        protected List<GameObject> mUpdateGameObjects = new List<GameObject>();
        protected List<Action<GameTime>> mUpdateAction = new List<Action<GameTime>>();
        protected List<Action> mDrawAction = new List<Action>();
        #endregion

        #region Getter & Setter
        public String Name { get { return this.mName; } }
        public Dictionary<String, String> AssetList { get { return mTexture2DStringList; } }
        public List<GameObject> UpdateGameObjects { get { return mUpdateGameObjects; } }
        public String Background { set { mBackgroundName = value; mClearColor = Color.White; } }
        #endregion

        #region Constructor

        public Scene()
        {
            //FillTexture2DList();
        }
        public Scene(String pSceneName)
        {
            this.mName = pSceneName;
            //FillTexture2DList();
        }
        #endregion

        #region Virtual Methods

        public virtual void Initialize()
        {
            mRenderTarget = new RenderTarget2D(EngineSettings.Graphics.GraphicsDevice, EngineSettings.WindowWidth, EngineSettings.WindowHeight);
            mSpriteBatch = new SpriteBatch(EngineSettings.Graphics.GraphicsDevice);
        }

        public virtual void LoadContent()
        {
            //TextureManager.Instance.LoadStringDictionary(mTexture2DStringList);
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

            mSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
            mSpriteBatch.Draw(TextureManager.Instance.GetElementByString<Texture2D>(mBackgroundName), new Rectangle(0, 0, EngineSettings.WindowWidth, EngineSettings.WindowHeight), mClearColor);
            mSpriteBatch.End();

            for(int i = 0; i < mDrawAction.Count; i++)
                mDrawAction[i]();

            mSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
            foreach (GameObject go in mUpdateGameObjects)
                go.Draw(mSpriteBatch);
            mSpriteBatch.End();

            EngineSettings.Graphics.GraphicsDevice.SetRenderTarget(null);

            mSpriteBatch.Begin();
            mSpriteBatch.Draw(mRenderTarget, new Rectangle(0, 0, EngineSettings.DisplayWidth, EngineSettings.DisplayHeight), Color.White);
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
