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
using Halloween.Graphics;
using Halloween.World;
using Halloween.Audio;

namespace Halloween
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public AudioManager audio;

        public Cam2d cam;
        Level currentLevel;

        Texture2D pawnText;

        Texture2D test;
        float rot;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            audio = new AudioManager(this, @"Content\Audio\audio_settings.xgs", @"Content\Audio\Sound Bank.xsb", @"Content\Audio\Wave Bank.xwb", @"Content\Audio\Streaming Bank.xwb");
            Components.Add(audio);

            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.cam = new Cam2d(GraphicsDevice.Viewport);

            TileArray.addTexture(Content.Load<Texture2D>("Tiles/defaultTile"));

  //          pawnText = Content.Load<Texture2D>("");

            for (int i = 1; i < 3; i++) // HACK update this as we add tiles.
            {
                TileArray.addTexture(Content.Load<Texture2D>("Tiles/tile" + i));
            }

            this.currentLevel = new Level();


            //test stuff
            test = Content.Load<Texture2D>("works");
            rot = 0f;
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                audio.Play("BGM_Ice");

            rot += 0.05f;
            //cam.Zoom *= .995f;

            currentLevel.update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, cam.GetCameraTransformation());



            currentLevel.render(gameTime, spriteBatch);


            spriteBatch.Draw(test, new Vector2(360f,240f), null, Color.White, rot, new Vector2(400f, 250f), .25f, SpriteEffects.None, 1);
            
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
