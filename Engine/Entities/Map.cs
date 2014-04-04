/**************************************************************
 * (c) Jens Richter 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DragonEngine;
using DragonEngine.Entities;

namespace DragonEngine.Entities
{
    class Map : GameObject
    {
        public enum MapLayout { isometric, profile, topdown }

        #region Properties

        protected MapLayout mLayout;

        #region Get & Set

        public MapLayout Layout { get { return mLayout; } }

        #endregion

        #endregion

        #region Constructor

        Map(MapLayout pLayout) : base()
        {
            mLayout = pLayout;
        }

        #endregion

        #region Methoden



        #endregion

    }
}
