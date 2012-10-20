using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Halloween.Entities
{
    class Human : Pawn
    {
        public bool isArmed;
        
        public Human() {
            health = 2;
            friendly = false;
            isArmed = false;
        }

        public override void update(GameTime gameTime) {
            if(isArmed){
                //TODO: attack AI
            }else{
                //TODO: run away AI
            }
    }
}
