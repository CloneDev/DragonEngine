using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DragonEngine.Pool
{
    class ObjectPool<T>
    {
        #region Properties
        protected List<T> mRessources;
        protected List<T> mFreeRessources;
        #endregion

        #region Getter & Setter

        public Action RecycleObject;
        #endregion

        #region Constructor

        public ObjectPool(T value)
        {
            mRessources = new List<T>();
            mFreeRessources = new List<T>();
        }
        #endregion

        #region Virtual Methoden

        //public T GetObject<T>()
        //{
        //    if(mFreeRessources.Count > 0)
        //    {

        //        return mFreeRessources[mFreeRessources.Count-1];
        //    }
        //    return null;
        //}
        #endregion
    }
}
