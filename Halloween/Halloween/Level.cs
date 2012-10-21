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
            for (var x = 0; x < entities.Count; x++)
                entities[x].render(gameTime, G.spriteBatch);

/*            foreach (Rectangle r in this.rectangles)
            {
                spriteBatch.Draw(G.pixelTexture, r, Color.Yellow);
            }*/

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            for (var x = 0; x < entities.Count; x++)
                entities[x].update(gameTime);
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
            Vector2 objectPos = new Vector2(mapObject.Bounds.X, mapObject.Bounds.Y);
            switch (mapObject.Type.ToLower())
            {
                case "box":
                    rectangles.Add(mapObject.Bounds);
                    break;
                case "start":
                    Player.currentPawn.pos = objectPos;
                    G.cam.Follow(Player.currentPawn);
                    break;
                case "spawner":
                    var pawnType = string.Empty;
                    if (mapObject.Properties.ContainsKey("pawntype"))
                    {
                        pawnType = mapObject.Properties["pawntype"].Value;
                    }
                    var spawntime = 0f;
                    if (mapObject.Properties.ContainsKey("spawntime"))
                    {
                        spawntime = mapObject.Properties["spawntime"].AsSingle.Value;
                    }
                    switch (pawnType)
                    {
                        case "kid":
                            entities.Add(new KidSpawner() { pos = objectPos, spawnTime = spawntime });
                            break;
                    }
                    break;
            }
        }
    }
}
