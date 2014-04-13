﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DragonEngine.Pools;

namespace DragonEngine.Entities
{
    public class ParallaxSpinePlane : ParallaxPlane<SpineObject>
    {

        #region Methods

        /// <summary>
        /// Füllt die ParallaxPlane mit Sprites anhand einer TileMap(RGBMap).
        /// </summary>
        /// <param name="pRGBMap">TileMap für die Ebene</param>
        /// <param name="pInterpreter">Zuweisung: string RGB -> string TextureName</param>
        /// <param name="pTileSize">Maßstab der TileMap</param>
        public void Generate(string[,] pRGBMap, Dictionary<String, SpinePool> pInterpreter, int pTileSize)
        {
            mSize = new Vector2(pRGBMap.GetLength(0) * pTileSize, pRGBMap.GetLength(1) * pTileSize);
            for (int x = 0; x < pRGBMap.GetLength(0); x++)
            {
                for (int y = 0; y < pRGBMap.GetLength(1); y++)
                {
                    if (pInterpreter.ContainsKey(pRGBMap[x,y])) //Zuweisung für diesen Farbwert vorhanden?
                        mTiles.Add(pInterpreter[pRGBMap[x,y]].GetObject(); //SpineObject von zugewiesenem Pool holen und zur ParallaxPlane hinzufügen
                }
            }
        }

        public void Draw(Camera pCamera)
        {
            foreach (SpineObject TmpSpineObject in mTiles)
                TmpSpineObject.Draw(pCamera);
        }

        #endregion
    }
}
