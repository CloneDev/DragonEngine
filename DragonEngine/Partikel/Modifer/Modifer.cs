using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DragonEngine.Partikel
{
    public enum ModifierType
    {

        PartikelModifier,
        EmitterModifer
    }



    public abstract class Modifer
    {

        #region Properties
        private ModifierType mType;
        #endregion

        #region Getter & Setter
        public ModifierType Type { get { return mType; } }
        #endregion

        #region Constructor
        #endregion

        #region VirtuallMethods
        #endregion

    }
}
