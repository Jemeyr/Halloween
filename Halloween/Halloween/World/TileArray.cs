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
                    this.tiles[i, j] = new Tile(j == 10? 1 :2, true, new Vector2(i * texSize, j * texSize));
                }
            }
        }

        public void render(SpriteBatch spriteBatch)
        {
            int xLength = this.tiles.GetLength(0);
            int yLength = this.tiles.GetLength(1);


            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    spriteBatch.Draw(textures[this.tiles[i, j].texID], this.tiles[i, j].position, (j+i)%2 == 0? Color.White: Color.YellowGreen);
                }
            }
        }

        public static int addTexture(Texture2D tex)
        {
            if (TileArray.textures == null)
            {
                textures = new Texture2D[64];
                valid = 0;
            }

            TileArray.textures[valid++] = tex;
            return valid;
        }

    }
}
