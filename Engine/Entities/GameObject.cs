using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DragonEngine.Entities
{
    public class GameObject
    {
        #region Properties

        protected Vector2 mPosition;

        protected List<GameObject> mAttachedList = new List<GameObject>();
        public List<Action<GameObject>> UpdateActionList = new List<Action<GameObject>>();
        protected List<Action<GameTime>> mUpdateActionGameTime = new List<Action<GameTime>>();

        protected Color mDebugColor = Color.Honeydew;
        #endregion

        #region Getter & Setter
        public int AttachedObjectsCount { get { return mAttachedList.Count; } }
        public Vector2 Position { set { mPosition = value; } get { return mPosition; } }
        public int PositionX { set { mPosition.X = value; } get { return (int)mPosition.X; } }
        public int PositionY { set { mPosition.Y = value; } get { return (int)mPosition.Y; } }
        #endregion

        #region Constructor

        public GameObject()
        {
        }

        public GameObject(Vector2 pPosition)
        {
            mPosition = pPosition;
        }
        #endregion

        #region Virutal Methoden

        /// <summary>
        /// Gibt das angehängte GameObject an der Stelle von pIndex.
        /// </summary>
        /// <param name="pIndex"></param>
        /// <returns></returns>
        public GameObject GetAttachedGameObject(int pIndex)
        {
            return (pIndex < mAttachedList.Count) ? mAttachedList[pIndex] : null;
        }

        public virtual void Update(GameTime gameTime) 
        {
            for (int i = 0; i < UpdateActionList.Count; i++)
                UpdateActionList[i](this);

            for (int i = 0; i < mUpdateActionGameTime.Count; i++)
                mUpdateActionGameTime[i](gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch) { }

        public void AddAction(Action<GameObject> pAction)
        {
            UpdateActionList.Add(pAction);
        }

        public Action<GameTime> GetActionGameTime(int Id)
        {
            return mUpdateActionGameTime[Id];
        }
        #endregion
    }
}
