using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DragonEngine.Entities
{
    public class FlyingSpritePlane : FlyingPlane<Sprite>
    {
        #region Properties

        #region Get&Set

        #endregion

        #endregion

        #region Constructor

        public FlyingSpritePlane()
            : base()
        {  
        }

        public FlyingSpritePlane(Vector2 pPosition)
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

        #endregion
    }
}
