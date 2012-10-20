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
    class TileArray
    {
        public Tile[,] tiles;
        public static int valid;
        private static Texture2D[] textures;
        private const int texSize = 32;

        public TileArray() : this(64,64)
        {}

        public TileArray(int xSize, int ySize)
        {
            if (textures == null)
            {
                textures = new Texture2D[64];
                valid = 0;
            }

            this.tiles = new Tile[xSize,ySize];
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    this.tiles[i, j] = new Tile(true, new Vector2(xSize * texSize, ySize * texSize));
                }
            }
        }

        public void render(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < this.tiles.GetLength(0); i++)
            {
                for (int j = 0; j < this.tiles.GetLength(1); i++)
                {
                    spriteBatch.Draw(textures[this.tiles[i, j].texID], this.tiles[i, j].position, Color.White);
                }
            }
        }

        public static int addTexture(Texture2D tex)
        {
            TileArray.textures[valid++] = tex;
            return valid;
        }

    }
}
