#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using DragonEngine.SceneManagement;
using DragonEngine.Manager;
using SnakeMobile.GameContent.Scenes;
using DragonEngine;
#endregion

namespace SnakeMobile
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        SpriteBatch spriteBatch;

        public Game1() : base()
        {
            Content.RootDirectory = "Content";

            EngineSettings.EngineContent = Content;
            EngineSettings.Graphics = new GraphicsDeviceManager(this);
            EngineSettings.SetResolution(1280, 720);

            SceneManager.Instance.AddScene(new StartScene("Start"));
            //SceneManager.Instance.AddScene(new GameScene("Game"));
            //SceneManager.Instance.AddScene(new SplashScreen("Splash"));
            //SceneManager.Instance.AddScene(new HighScoreScreen("Highscore"));
            //SceneManager.Instance.AddScene(new CreditsScene("Credits"));

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            SceneManager.Instance.Initialize();
            //SceneManager.Instance.GetScene("Game").Background = "Background";
            //SceneManager.Instance.GetScene("Splash").Background = "SplashScreen";
            SceneManager.Instance.GetScene("Start").Background = "BackgroundStart";
            SceneManager.Instance.SetStartSceneTo("Start");
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (!this.IsActive) return;
            if (EngineSettings.Time == null) EngineSettings.Time = gameTime;
            SceneManager.Instance.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            SceneManager.Instance.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
