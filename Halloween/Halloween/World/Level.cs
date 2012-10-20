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
using Halloween.Entities;
using FuncWorks.XNA.XTiled;

namespace Halloween.World
{
    public class Level : DrawableGameComponent
    {
        public FuncWorks.XNA.XTiled.Map map;
        public SpriteBatch spriteBatch;
        public List<Entity> entities = new List<Entity>();
        public Rectangle mapView = new Rectangle();
        public List<Rectangle> rectangles = new List<Rectangle>();

        public ContentManager Content
        {
            get { return Game.Content; }
        }

        public Level(Game game, SpriteBatch sb)
            : base(game)
        {
            spriteBatch = sb;
        }

        public override void Draw(GameTime gameTime)
        {
            map.Draw(spriteBatch, mapView);
            foreach (Entity entity in entities)
            {
                entity.render(gameTime, spriteBatch);
            }
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (Entity entity in entities)
            {
                entity.update(gameTime);
            }
        }

        public void LoadMap(string mapName)
        {
            map = Content.Load<Map>(@"Levels\1");
            foreach (ObjectLayer objectLayer in map.ObjectLayers)
            {
                foreach (MapObject mapObject in objectLayer.MapObjects)
                {
                    switch (mapObject.Type.ToLower())
                    {
                        case "box":
                            rectangles.Add(mapObject.Bounds);
                            break;
                    }
                }
            }
        }
    }
}
