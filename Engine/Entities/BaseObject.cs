using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DragonEngine.Entities
{
    public class BaseObject
    {
        #region Properties

        protected List<BaseObject> mAttachedList = new List<BaseObject>();
        public List<Action<BaseObject>> UpdateActionList = new List<Action<BaseObject>>();
        protected List<Action<GameTime>> mUpdateActionGameTime = new List<Action<GameTime>>();
        #endregion

        #region Getter & Setter
        public int AttachedObjectsCount { get { return mAttachedList.Count; } }
        #endregion

        #region Constructor

        public BaseObject()
        {
        }
        #endregion

        #region Virutal Methoden

        /// <summary>
        /// Gibt das angehängte GameObject an der Stelle von pIndex.
        /// </summary>
        /// <param name="pIndex"></param>
        /// <returns></returns>
        public BaseObject GetAttachedGameObject(int pIndex)
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

        public void AddAction(Action<BaseObject> pAction)
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
