﻿/**************************************************************
 * (c) Carsten Baus, Jens Richter 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DragonEngine
{
    public static class EngineSettings
    {
        #region Properties

        public static bool IsDebug = false;

        #region Device
        private static bool mOnAndroid = false;
        private static bool mOnWindows = false;
        private static bool mOnWindowsPhone = false;
        private static bool mOnIOs = false;
        
        public static bool OnAndriod
        {
            get { return mOnAndroid; }
            set
            {
                if (value)
                {
                    mOnWindows = false;
                    mOnWindowsPhone = false;
                    mOnIOs = false;
                    mOnAndroid = true;
                }
            }
        }
        public static bool OnWindows
        {
            get { return mOnWindows; }
            set
            {
                if (value)
                {
                    mOnWindows = true;
                    mOnWindowsPhone = false;
                    mOnIOs = false;
                    mOnAndroid = false;
                }
            }
        }
        public static bool OnwindowsPhone
        {
            get { return mOnWindowsPhone; }
            set
            {
                if (value)
                {
                    mOnWindows = false;
                    mOnWindowsPhone = true;
                    mOnIOs = false;
                    mOnAndroid = false;
                }
            }
        }
        public static bool OnIOS
        {
            get { return mOnIOs; }
            set
            {
                if (value)
                {
                    mOnWindows = false;
                    mOnWindowsPhone = false;
                    mOnIOs = true;
                    mOnAndroid = false;
                }
            }
        }
        #endregion

        public static ContentManager Content;
        public static GraphicsDeviceManager Graphics;
        public static GameTime Time;

        public static Keys Exitkey = Keys.F12;

        #region Resolution

        public static int WindowHeight = 720;
        public static int WindowWidth = 1280;

        public static int DisplayHeight = 360;
        public static int DisplayWidth = 640;

        public static float AspectRatioX;
        public static float AspectRatioY;

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Setzt die Auflösung wie sie gewünscht ist.
        /// </summary>
        public static void SetResolution()
        {
            Graphics.PreferredBackBufferHeight = DisplayHeight;
            Graphics.PreferredBackBufferWidth = DisplayWidth;

            AspectRatioX = WindowWidth / (float)DisplayWidth;
            AspectRatioY = WindowHeight / (float)DisplayHeight;

            Graphics.ApplyChanges();
        }

        public static void SetResolution(int pWidth, int pHeight)
        {
            WindowHeight = pHeight;
            WindowWidth = pWidth;

            SetResolution();
        }
        #endregion
    }
}