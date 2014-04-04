/**************************************************************
 * (c) Carsten Baus 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DragonEngine.Manager
{
    public abstract class Manager
    {
        #region Properties
        protected Dictionary<String, object> mRessourcen = new Dictionary<string, object>();
        #endregion

        #region Getter & Setter
        #endregion

        #region Constructor
        #endregion

        #region Methoden

        abstract public void Add(String pName, String pPath);
        abstract public void LoadContent();
        abstract public void Unload();
        abstract public T GetElementByString<T>(String pElementName);
        #endregion
    }
}
