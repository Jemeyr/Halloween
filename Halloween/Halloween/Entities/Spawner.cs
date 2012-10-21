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

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
            spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (spawnTimer >= spawnTime)
            {
                spawnTimer = 0;
                G.level.entities.Add(create());
            }
        }

        public abstract Pawn create();
    }
}
