using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DragonEngine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace DragonEngine.Partikel
{
    class Emitter : GameObject
    {
        #region Proberties
        private List<Modifer> mEmitterModifer;
        private List<Particel> mEmitterParticel;

        
        #endregion

        #region Getter & Setter
        #endregion

        #region Constructor
        #endregion

       
       
        

        #region Methods

        public void addModifier(Modifer pModifer)
        {
            if(pModifer.Type == ModifierType.EmitterModifer) this.mEmitterModifer.Add(pModifer);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
