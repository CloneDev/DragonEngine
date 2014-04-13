using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DragonEngine.Particle;
using DragonEngine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace DragonEngine.Particle
{
    class Emitter : GameObject
    {
        #region Properties
        private List<Modifier> mEmitterModifer;
        private List<Particle> mEmitterParticel;

        
        #endregion

        #region Getter & Setter
        #endregion

        #region Constructor
        #endregion

       
       
        

        #region Methoden

        public void addModifier(Modifier pModifier)
        {
            if(pModifier.Type == ModifierType.EmitterModifier) this.mEmitterModifer.Add(pModifier);
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch pSpriteBatch)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
