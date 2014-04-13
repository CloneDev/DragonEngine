/**************************************************************
 * (c) Carsten Baus, Jens Richter 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DragonEngine
{
    public static class EngineSettings
    {
        #region Properties
        
        public static bool IsDebug = false;
        public static bool OnAndriod = false;

        public static ContentManager Content;
        public static GraphicsDeviceManager Graphics;
        public static GameTime Time;

        #region Resolution

        private static Vector2 ScreenResolution = Vector2.Zero;
        private static Vector2 VirtualResolution = new Vector2(1280, 720);

        public static int VirtualResX { get { return (int)VirtualResolution.X; } }
        public static int VirtualResY { get { return (int)VirtualResolution.Y; } }

        public static int ScreenResX { get { return (int)ScreenResolution.X; } }
        public static int ScreenResY { get { return (int)ScreenResolution.Y; } }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Setzt die Windowauflösung auf Bildschirmauflösung und Fullscreen = true.
        /// </summary>
        public static void FitToFullScreen()
        {
            GetScreenResolution();
            SetWindowToFullScreenPC();
        }

        /// <summary>
        /// Läd die Bildschirmauflösung in Vector2 ScreenResolution.
        /// </summary>
        public static void GetScreenResolution()
        {
            ScreenResolution.X = (float)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            ScreenResolution.Y = (float)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        }

        /// <summary>
        /// Setzt die Windowauflösung auf Vector2 ScreenResolution und Fullscreen = true.
        /// </summary>
        public static void SetWindowToFullScreen()
        {
            Graphics.PreferredBackBufferWidth = (int)ScreenResolution.X;
            Graphics.PreferredBackBufferHeight = (int)ScreenResolution.Y;
            Graphics.IsFullScreen = true;
            Graphics.ApplyChanges();
        }

        /// <summary>
        /// Setzt die Windowauflösung auf VirtualResolution.
        /// </summary>
        public static void SetWindowToFullScreenPC()
        {
            ScreenResolution = VirtualResolution;
            ScreenResolution *= 0.75f;
            Graphics.PreferredBackBufferWidth = (int)ScreenResolution.X;
            Graphics.PreferredBackBufferHeight = (int)ScreenResolution.Y;
            Graphics.ApplyChanges();
        }
        
        #endregion
    }
}
