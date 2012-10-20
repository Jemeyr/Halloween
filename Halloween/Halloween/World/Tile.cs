using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Halloween.World
{
    class Tile
    {
        public int texID;
        public bool passable;
        public Vector2 position;
        static private readonly bool[] defaultPassability = { true, false, true }; //HACK update this as needed


        public Tile(Vector2 pos) : this(defaultPassability[0], pos) { }

        public Tile(int texId, Vector2 pos) : this(texId, defaultPassability[texId], pos) { }

        public Tile(bool pass, Vector2 pos) : this(0, pass, pos) { }

        public Tile(int texID, bool pass, Vector2 pos)
        {
            this.texID = texID;
            if (texID >= TileArray.valid)
            {
                this.texID = 0;
                Console.Out.WriteLine("HEY! You should initialize your textures before the tilearray");
            }
            this.passable = pass;
            this.position = pos;
        }

        

    }
}
