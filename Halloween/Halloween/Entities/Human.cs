using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace Halloween.Entities
{
    enum Type { kid, military, police }

    class Human : Pawn
    {
        public Type type;
        public bool armed;

        public Human(Type type) 
        {
            health = 2;
            this.type = type;
            if (this.type != 0) 
            {
                armed = true;
            }
        }

        public override void update(GameTime gameTime)
        {
            if (armed)
            {
                //TODO: attack AI
            }
            else 
            {
                //TODO: run away AI
            }
        }
    }
}
