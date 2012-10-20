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
using System.IO;
using FuncWorks.XNA.XTiled;
using Halloween.Input;
using Halloween.Entities;

namespace Halloween
{
    public class G : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static ContentManager content;
        public static GraphicsDevice graphicsDevice;
        public static SpriteFont spriteFont;
        public static Player player;

        public static Dictionary<string, Animation> animations = new Dictionary<string, Animation>();

        public static Texture2D pixelTexture;

        internal static readonly TimeSpan CachedSecond = new TimeSpan(0, 0, 0, 1);

        public static AudioManager audio;
        public static InputManager input;

        public static Cam2d cam;
        public static Level level;

        Texture2D test;
        float rot;

        public G()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            content = Content;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            graphicsDevice = GraphicsDevice;
            spriteFont = Content.Load<SpriteFont>("Font");
            pixelTexture = new Texture2D(graphicsDevice, 1, 1);
            pixelTexture.SetData<Color>(new[] { Color.White });

            audio = new AudioManager(this, @"Content\Audio\audio_settings.xgs", @"Content\Audio\Sound Bank.xsb", @"Content\Audio\Wave Bank.xwb", @"Content\Audio\Streaming Bank.xwb");
            Components.Add(audio);

            input = new InputManager();
            Components.Add(input);

            spriteBatch = new SpriteBatch(GraphicsDevice);
            cam = new Cam2d(GraphicsDevice.Viewport);

            animations.Add("zombie", new Animation(Content.Load<Texture2D>("zombieWalk"), 0.5f, true));

            level = new Level(this, spriteBatch);
            level.LoadMap(@"Levels\1");
            level.mapView = graphicsDevice.Viewport.Bounds;

            test = Content.Load<Texture2D>("works");
            rot = 0f;
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (input.Keyboard[Keys.Escape].IsPressed)
                this.Exit();

            if (input.Keyboard[Keys.Space].IsPressed)
                audio.Play("SFX_Laser");

            if (input.Players[PlayerIndex.One].Gamepad[GamepadButtons.A].IsPressed)
                audio.Play("SFX_Laser");

            if (input.Players[PlayerIndex.Two].Gamepad[GamepadButtons.B].IsPressed)
                audio.Play("SFX_Laser");

            level.Update(gameTime);

            if (player != null)
                player.currentPawn.update(gameTime);

            rot += 0.05f;
            //cam.Zoom *= .995f;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, cam.GetCameraTransformation());

            level.Draw(gameTime);

            if (player != null)
                player.currentPawn.render(gameTime, spriteBatch);

            spriteBatch.Draw(test, new Vector2(360f,240f), null, Color.White, rot, new Vector2(400f, 250f), .25f, SpriteEffects.None, 1);
            spriteBatch.DrawString(spriteFont, cam.Position.ToString(), Vector2.Zero, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
