using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DragonEngine.Particle
{
    public enum ModifierType
    {

        PartikelModifier,
        EmitterModifier
    }



    public abstract class Modifier
    {

        #region Properties
        private ModifierType mType;
        #endregion

        #region Getter & Setter
        public ModifierType Type { get { return mType; } }
        #endregion

        #region Constructor
        #endregion

        #region Virtual Methoden
        #endregion

    }
}
