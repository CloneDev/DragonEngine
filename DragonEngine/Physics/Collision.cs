/**************************************************************
 * (c) Jens Richter 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DragonEngine;
using Microsoft.Xna.Framework;

namespace DragonEngine.Physics
{
    public class Collision
    {

        public static Vector2 CollisionCheckedVector(Rectangle pBody ,int pDeltaX, int pDeltaY, List<Rectangle> pBodiesToCheck)
        {
            Rectangle TmpCollisionBody = pBody;
            Vector2 TmpMove = Vector2.Zero;
            int TmpStep;
            bool TmpCollided;
            //Größere Koordinate als Iteration nehmen
            if (Math.Abs(pDeltaX) > Math.Abs(pDeltaY))
            {
                TmpStep = Math.Abs(pDeltaX);
            }
            else
            {
                TmpStep = Math.Abs(pDeltaY);
            }
            //Iteration
            for (int i = 1; i <= TmpStep; i++)
            {
                TmpCollided = false;
                //Box für nächsten Iterationsschritt berechnen
                TmpCollisionBody.X = pBody.X + ((pDeltaX / TmpStep) * i);
                TmpCollisionBody.Y = pBody.Y + ((pDeltaY / TmpStep) * i);
                //Gehe die Blöcke der Liste durch
                foreach (Rectangle pBodyToCheck in pBodiesToCheck)
                {
                    //Wenn Kollision vorliegt: Keinen weiteren Block abfragen
                    if (TmpCollisionBody.Intersects(pBodyToCheck))
                    {
                        TmpCollided = true;
                        break;
                    }
                }
                if (TmpCollided == true) //Bei Kollision: Kollisionsabfrage mit letztem kollisionsfreien Zustand beenden
                {
                    break;
                }
                else //Kollisionsfreien Fortschritt speichern
                {
                    TmpMove.X = TmpCollisionBody.X - pBody.X;
                    TmpMove.Y = TmpCollisionBody.Y - pBody.Y;
                }
            }
            return TmpMove;
        }
    }
}
