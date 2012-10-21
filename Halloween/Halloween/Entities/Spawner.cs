using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Halloween.Entities
{
    public abstract class Spawner : Entity
    {
        public float spawnTime = 5;
        float spawnTimer;
        public int max = 5;
        int added;

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
            spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (spawnTimer >= spawnTime)
            {
                spawnTimer = 0;
                if (added < max)
                {
                    G.level.pawns.Add(create());
                    added += 1;
                }
            }
            //if (G.input.Keyboard[Microsoft.Xna.Framework.Inp])
        }

        public abstract Pawn create();
    }
}
