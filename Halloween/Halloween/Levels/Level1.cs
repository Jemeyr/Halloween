using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FuncWorks.XNA.XTiled;

namespace Halloween.Levels
{
    public class Level1 : World.Level
    {

        public Level1(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            map = Content.Load<Map>(@"Levels\1");
        }

    }
}
