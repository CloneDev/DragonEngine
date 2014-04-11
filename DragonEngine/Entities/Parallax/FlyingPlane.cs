///**************************************************************
// * (c) Jens Richter 2014
// *************************************************************/
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//namespace DragonEngine.Entities
//{
//    public class FlyingPlane<T> : ParallaxPlane<T>
//    {
//        #region Properties

//        public double spawnTimer;

//        protected int mTop;
//        protected int mBottom;
//        protected int mAmount;
//        protected int mChaos;
//        protected int mType;
//        protected float mWind;

//        Random mRandomNumber = new Random();

//        #region Get&Set

//        #endregion

//        #endregion

//        #region Constructor

//        public FlyingPlane()
//            : base()
//        {  
//        }

//        public FlyingPlane(Vector2 pPosition)
//            : base(pPosition)
//        {
//        }

//        #endregion

//        #region Methoden

//        public void Load(Microsoft.Xna.Framework.Content.ContentManager Content, string pTextureName, Camera pCamera)
//        {
//            cloudTexture = Content.Load<Texture2D>("sprites/Level_1/Planes/" + pTextureName);
//            spawnTimer = 0;
//            for (int i = 0; i <= size.X / mWind; i++)
//            {
//                if (spawnTimer > (((100000 - (float)mAmount) * ((100 - (float)mChaos) / 100)) / 30))
//                {
//                    spawnTimer = 0;
//                    SpawnTile(pCamera);
//                }
//                //Wolken updaten
//                for (int t = 0; t < mTiles.Count(); t++)
//                {
//                    T TmpTile = mTiles.ElementAt(t);
//                    TmpTile.Update(mWind); //position.X -= wind;
//                    if (TmpTile.position.X < -TmpTile.cuttexture.Width)
//                    {
//                        mTiles.Remove(TmpTile);
//                    }
//                }
//                spawnTimer++;
//            }
//        }

//        /// <summary>
//        /// Updated die Ebenenverschiebung anhand der übergebenen Kamera, relativ zu Viewport und Viewarea und spawned fliegende Objekte. 
//        /// </summary>
//        /// <param name="pCamera">Zu verwendende Kamera.</param>
//        public override void Update(Camera pCamera)
//        {
//            base.Update(pCamera);

//            //Spawntimer

//            if (EngineSettings.Time.TotalGameTime.TotalMilliseconds > spawnTimer + ((100000 - (float)mAmount) * ((100 - (float)mChaos) / 100)))
//            {
//                spawnTimer = EngineSettings.Time.TotalGameTime.TotalMilliseconds;
//                SpawnTile(pCamera);
//            }

//            //Wolken updaten
//            for (int i = 0; i < mTiles.Count(); i++)
//            {
//                T TmpTile = mTiles.ElementAt(i);
//                TmpTile.Update(mWind); //position.X -= wind;
//                if (TmpTile.position.X < -TmpTile.cuttexture.Width)
//                {
//                    mTiles.Remove(TmpTile);
//                }
//            }
//        }

//        public void SpawnTile(Camera pCamera)
//        {
//            //Entscheiden ob Wolke gespawned werden soll
//            if (mRandomNumber.Next(0, 100) >= mChaos)
//            {
//                //Wolkentyp bestimmen
//                int type = 1;
//                if (mRandomNumber.Next(0, 10) < mType)
//                {
//                    type = 2;
//                }
//                //Wolkenposition bestimmen
//                int spawnPosition = ((int)((mBottom - mTop) * ((float)mRandomNumber.Next(0, 100) / 100))) + mTop;
//                //Wolke erstellen
//                mTiles.Add(new Cloud(type, new Vector2(size.X, spawnPosition), (float)mRandomNumber.Next(mSizeMin, mSizeMax) / 100));
//            }
//        }

//        #endregion

//    }
//}
