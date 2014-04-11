/**************************************************************
 * (c) Jens Richter 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DragonEngine.Entities
{
    public class FlyingPlane<T> : ParallaxPlane<T>
    {
        #region Properties

        public double spawnTimer;

        int mTop;
        int mBottom;
        int mAmount;
        int mChaos;
        int mType;
        public float mWind;
        int mSizeMin;
        int mSizeMax;

        int mTestSpawn;
        int mTestType;
        int mTestPosition;
        int mTestSize;

        Random mRandomNumber = new Random();

        #region Get&Set

        #endregion

        #endregion

        #region Constructor

        public FlyingPlane()
            : base()
        {  
        }

        public FlyingPlane(Vector2 pPosition)
            : base(pPosition)
        {
        }

        #endregion

        #region Methoden

        public void Load(Microsoft.Xna.Framework.Content.ContentManager Content, string pTextureName, Camera pCamera)
        {
            cloudTexture = Content.Load<Texture2D>("sprites/Level_1/Planes/" + pTextureName);
            spawnTimer = 0;
            for (int i = 0; i <= size.X / mWind; i++)
            {
                if (spawnTimer > (((100000 - (float)mAmount) * ((100 - (float)mChaos) / 100)) / 30))
                {
                    spawnTimer = 0;
                    SpawnTile(pCamera);
                }
                //Wolken updaten
                for (int t = 0; t < mTiles.Count(); t++)
                {
                    T TmpTile = mTiles.ElementAt(t);
                    TmpTile.Update(mWind); //position.X -= wind;
                    if (TmpTile.position.X < -TmpTile.cuttexture.Width)
                    {
                        mTiles.Remove(TmpTile);
                    }
                }
                spawnTimer++;
            }
        }

        /// <summary>
        /// Updated die Ebenenverschiebung anhand der übergebenen Kamera, relativ zu Viewport und Viewarea und spawned fliegende Objekte. 
        /// </summary>
        /// <param name="pCamera">Zu verwendende Kamera.</param>
        public override void Update(Camera pCamera)
        {
            base.Update(pCamera);

            //Spawntimer

            if (EngineSettings.Time.TotalGameTime.TotalMilliseconds > spawnTimer + ((100000 - (float)mAmount) * ((100 - (float)mChaos) / 100)))
            {
                spawnTimer = EngineSettings.Time.TotalGameTime.TotalMilliseconds;
                SpawnTile(pCamera);
            }

            //Wolken updaten
            for (int i = 0; i < mTiles.Count(); i++)
            {
                T TmpTile = mTiles.ElementAt(i);
                TmpTile.Update(mWind); //position.X -= wind;
                if (TmpTile.position.X < -TmpTile.cuttexture.Width)
                {
                    mTiles.Remove(TmpTile);
                }
            }
        }

        public override void  Draw(SpriteBatch pSpriteBatch)
        {
            foreach (T TmpTile in mTiles)
            {
                TmpTile.Draw(pSpriteBatch, cloudTexture, position);
                /*public void Cloud.Draw(SpriteBatch spriteBatch, Texture2D cloudTexture, Vector2 parallaxOffset)
                {
                    spriteBatch.Draw(cloudTexture, position + parallaxOffset, cuttexture, Color.White, 0.0f, Vector2.Zero, size, SpriteEffects.None, 1.0f);
                }*/
            }
        }

        public void SpawnTile(Camera pCamera)
        {
            //Entscheiden ob Wolke gespawned werden soll
            mTestSpawn = mRandomNumber.Next(0, 100);
            if (mTestSpawn >= mChaos)
            {
                //Wolkentyp bestimmen
                mTestType = mRandomNumber.Next(0, 10);
                int type = 1;
                if (mTestType < mType)
                {
                    type = 2;
                }
                //Wolkenposition bestimmen
                mTestPosition = mRandomNumber.Next(0, 100);
                int spawnPosition = ((int)((mBottom - mTop) * ((float)mTestPosition / 100))) + mTop;
                //Wolke erstellen
                mTestSize = mRandomNumber.Next(mSizeMin, mSizeMax);
                mTiles.Add(new Cloud(type, new Vector2(size.X, spawnPosition), (float)mTestSize / 100));
            }
        }

        #endregion

    }
}
