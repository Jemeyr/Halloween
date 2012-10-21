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
        public List<Pawn> pawns = new List<Pawn>();
        public List<Spawner> spawners = new List<Spawner>();
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
            for (var x = 0; x < pawns.Count; x++)
                pawns[x].render(gameTime, G.spriteBatch);

            //foreach (Rectangle r in this.rectangles)
            //{
            //    G.spriteBatch.Draw(G.pixelTexture, new Rectangle(r.X, r.Y + 5, r.Width, r.Height), Color.Yellow);
            //}
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            mapView = new Rectangle(0, 0, map.Width * map.TileWidth, map.Height * map.TileHeight);
            for (var x = 0; x < spawners.Count; x++)
                spawners[x].update(gameTime);
            for (var x = 0; x < pawns.Count; x++)
                pawns[x].update(gameTime);

            for (var x = 0; x < pawns.Count; x++)
            {
                for (var y = 0; y < Player.horde.Count; y++)
                {
                    if (pawns[x].positionCollisionBox.Intersects(Player.horde[y].positionCollisionBox))
                    {
                        pawns[x].onHit(Player.horde[y]);
                        Player.horde[y].onHit(pawns[x]);
                    }
                }
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
                    int maxAmount = 0;
                    if (mapObject.Properties.ContainsKey("max"))
                    {
                        maxAmount = mapObject.Properties["max"].AsInt32.Value;
                    }
                    switch (pawnType)
                    {
                        case "kid":
                            spawners.Add(new KidSpawner() { pos = objectPos, spawnTime = spawntime, max = maxAmount});
                            break;
                    }
                    break;
            }
        }
    }
}
