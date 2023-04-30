using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlanetDestroyer
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int screenW, screenH;
        public static KeyboardState kb, oldKB;
        public static MouseState mouse, oldMouse;
        public static Rectangle mouseRect;

        public static List<Rectangle> planetRects;
        public static List<Texture2D> planetTextures;

        public static List<Rectangle> explosionRects;
        public static List<Texture2D> explosionTextures;

        public static Texture2D planetTemplate, planetTexture;
        public static Color temp;
        public static Random rnd;
        public static GraphicsDevice gd;

        public Planet planet;

        public static int time, planetGrit;

        public static SpriteFont healthFont;
        public List<SpriteFont> fonts;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width-5;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height-80;
            Window.AllowUserResizing = true;

            this.IsMouseVisible = true;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            mouse = oldMouse = Mouse.GetState();
            kb = oldKB = Keyboard.GetState();
            mouseRect = new Rectangle(mouse.X - 1, mouse.Y - 1, 2, 2);
            screenW = GraphicsDevice.Viewport.Width;
            screenH = GraphicsDevice.Viewport.Height;
            var form = (System.Windows.Forms.Form)System.Windows.Forms.Control.FromHandle(this.Window.Handle);
            form.Location = new System.Drawing.Point(-7, 0);
            rnd = new Random();
            temp = new Color(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            time = 0;
            gd = GraphicsDevice;
            
            planetGrit = rnd.Next(5, 100);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            planetTemplate = Content.Load<Texture2D>("upscaledBlankPlanet");
            fonts = new List<SpriteFont> { Content.Load<SpriteFont>("planetFont1"), Content.Load<SpriteFont>("planetFont2"), Content.Load<SpriteFont>("planetFont3"), Content.Load<SpriteFont>("planetFont4"), Content.Load<SpriteFont>("planetFont5") };
            
            if (screenW >= 1080)
                healthFont = fonts[0];
            else if (screenW >= 980)
                healthFont = fonts[1];
            else if (screenW >= 880)
                healthFont = fonts[2];
            else if (screenW >= 780)
                healthFont = fonts[3];
            else if (screenW >= 680)
                healthFont = fonts[4];

            planet = new Planet(1);
            planetTexture = planet.PlanetTextureGeneration();

            
        }

        
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            oldKB = kb;
            kb = Keyboard.GetState();
            oldMouse = mouse;
            mouse = Mouse.GetState();
            mouseRect.X = mouse.X - 1; mouseRect.Y = mouse.Y - 1;
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            planet.Update();



            if (time % 2 == 0)
                planetTexture = planet.UpdatePlanetTexture();
            if (kb.IsKeyDown(Keys.Space) && oldKB.IsKeyUp(Keys.Space))
            {
                planetGrit = rnd.Next(5, 100);
                temp = new Color(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
                planetTexture = planet.PlanetTextureGeneration();
            }
                
            time++;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            planet.Draw(spriteBatch);
            spriteBatch.Draw(planetTexture, mouseRect, Color.Black);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
