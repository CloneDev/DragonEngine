using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DragonEngine.Manager;
using DragonEngine.SceneManagement;
using DragonEngine.Controls;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace DragonEngine
{
    public class EngineGame : Game
    {
        SpriteBatch spriteBatch;

        public EngineGame()
            : base()
        {
            Content.RootDirectory = "Content";
            EngineSettings.Content = Content;
            EngineSettings.Graphics = new GraphicsDeviceManager(this);

            SpineSettings.Setup();
        }

        protected override void Initialize()
        {
            base.Initialize();
            SceneManager.Instance.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(EngineSettings.Graphics.GraphicsDevice);

            SceneManager.Instance.LoadContent();
            TextureManager.Instance.LoadContent();
            //SpineManager.Instance.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (!this.IsActive) return;
            if (EngineSettings.Time == null) EngineSettings.Time = gameTime;

            if (EngineSettings.OnWindows)
                if (Keyboard.GetState().IsKeyDown(EngineSettings.Exitkey)) Exit();

            if(EngineSettings.OnWindows)
                MouseHelper.Update();
            SceneManager.Instance.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            SceneManager.Instance.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
