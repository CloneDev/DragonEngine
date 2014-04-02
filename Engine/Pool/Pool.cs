using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DragonEngine.Pool
{
    abstract class Pool<T>
    {

        #region Properties
        protected static List<T> mFreeRessources;
        protected static List<T> mUsedRessources;
        #endregion

        #region Getter & Setter
        #endregion

        #region Constructor

        public Pool()
        {
            Initialize();
        }

        #endregion

        #region Methoden

        public static void Initialize()
        {
            mFreeRessources = new List<T>();
            mUsedRessources = new List<T>();
        }

        public T GetObject()
        {
            if (mFreeRessources.Count != 0)
            {
                T TmpObject = mFreeRessources[0];
                mUsedRessources.Add(TmpObject);
                mFreeRessources.RemoveAt(0);
                return TmpObject;
            }
            else
            {
                T TmpObject = CreateInstance();
                mUsedRessources.Add(TmpObject);
                return TmpObject;
            }
        }

        public void ReleaseObject(T pObject)
        {
            CleanUpInstance(pObject);

            lock (mFreeRessources)
            {
                mFreeRessources.Add(pObject);
                mUsedRessources.Remove(pObject);
            }
        }

        #region Abstract

        protected abstract T CreateInstance();
        protected abstract void CleanUpInstance(T pObject);
        
        #endregion
        
        #endregion
    }
}
