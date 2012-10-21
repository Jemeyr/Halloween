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

namespace Halloween
{
    public class Level : DrawableGameComponent
    {
        public FuncWorks.XNA.XTiled.Map map;
        public List<Entity> entities = new List<Entity>();
        public Rectangle mapView = new Rectangle();
        public List<Rectangle> rectangles = new List<Rectangle>();

        public ContentManager Content
        {
            get { return Game.Content; }
        }

        public Level(Game game)
            : base(game)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            map.Draw(G.spriteBatch, mapView);
            foreach (Entity entity in entities)
            {
                entity.render(gameTime, G.spriteBatch);
            }

/*            foreach (Rectangle r in this.rectangles)
            {
                spriteBatch.Draw(G.pixelTexture, r, Color.Yellow);
            }*/

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
                    LoadObject(mapObject);
                }
            }
        }

        void LoadObject(MapObject mapObject)
        {
            switch (mapObject.Type.ToLower())
            {
                case "box":
                    rectangles.Add(mapObject.Bounds);
                    break;
                case "start":
                    Player.currentPawn.pos = new Vector2(mapObject.Bounds.X, mapObject.Bounds.Y);
                    G.cam.Follow(Player.currentPawn);
                    break;
            }
        }
    }
}
