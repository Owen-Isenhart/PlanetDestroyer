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
        public static int scrollWheel, oldScrollWheel;

        //public static List<Rectangle> planetRects;
        //public static List<Texture2D> planetTextures;

        public static Dictionary<string, List<Rectangle>> explosionRects;
        public static List<Rectangle> shipRects, cometSources;

        public static Texture2D planetTemplate, planetTexture, pixel, ship, ballShip, spikyShip, whitePixel, questionMark, checkMark, shipSheet, cash, cometSheet, logo;
        public static Color temp;
        public static Random rnd;
        public static GraphicsDevice gd;

        //public Planet planet;
        public static PlayScreen playScreen;
        public StoreAndPrestige storeAndPrestige;
        public Upgrades upgrades;
        public AchievementsScreen achievements;
        public Money money;
        public Settings settings;

        public static int time, planetGrit;

        public static SpriteFont healthFont, shopFont;
        public static List<SpriteFont> fonts;

        public static Texture2D explosionsSheet;

        public static bool activeModal;

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
            scrollWheel = oldScrollWheel = mouse.ScrollWheelValue;
            var form = (System.Windows.Forms.Form)System.Windows.Forms.Control.FromHandle(this.Window.Handle);
            form.Location = new System.Drawing.Point(-7, 0);
            rnd = new Random();
            temp = new Color(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            time = 0;
            gd = GraphicsDevice;
            shipRects = new List<Rectangle>();
            int y = 0;
            //for (int i = 0, x= 0; i < 7; i ++, x++)
            //{
            //    if (i != 0 && i != 6)
            //        shipRects.Add(new Rectangle(x * 450, y, 450, 450));
            //    if (x == 2)
            //    {
            //        x = -1;
            //        y += 450;
            //    }
            //}
            //y = 0;
            for (int i = 0, x = 0; i < 5; i ++, x++)
            {
                shipRects.Add(new Rectangle(x * 450, y, 450, 450));
                if (x == 1)
                {
                    x = -1;
                    y += 450;
                }
            }
            cometSources = new List<Rectangle> { new Rectangle(0, 0, 100, 100), new Rectangle(100, 0, 100, 100), new Rectangle(0, 100, 100, 100), new Rectangle(100, 100, 100, 100), new Rectangle(0, 200, 100, 100) };
            explosionRects = new Dictionary<string, List<Rectangle>>();
            explosionRects["small"] = loadExplosions("small") ;
            explosionRects["large"] = loadExplosions("large");
            planetGrit = rnd.Next(5, 100);
            activeModal = false;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 
        public List<Rectangle> loadExplosions(string type)
        {
            List<Rectangle> temp = new List<Rectangle>();
            if (type.Equals("small"))
            {
                temp.Add(new Rectangle(195, 285, 105, 100));
                temp.Add(new Rectangle(295, 285, 115, 100));
                temp.Add(new Rectangle(425, 285, 175, 100));
                temp.Add(new Rectangle(600, 285, 120, 100));
                temp.Add(new Rectangle(734, 285, 136, 100));
                temp.Add(new Rectangle(880, 285, 150, 100));
                temp.Add(new Rectangle(1050, 285, 140, 100));
                temp.Add(new Rectangle(1200, 285, 130, 100));
                temp.Add(new Rectangle(1340, 285, 150, 100));
            }
            else if (type.Equals("large"))
            {
                temp.Add(new Rectangle(40, 450, 130, 230));
                temp.Add(new Rectangle(180, 450, 170, 230));
                temp.Add(new Rectangle(380, 450, 260, 230));
                temp.Add(new Rectangle(650, 450, 230, 230));
                temp.Add(new Rectangle(900, 450, 200, 230));
                temp.Add(new Rectangle(1110, 450, 200, 230));
                temp.Add(new Rectangle(1330, 450, 220, 230));
                temp.Add(new Rectangle(1560, 450, 230, 230));
                temp.Add(new Rectangle(1330, 665, 210, 230));

            }
            else if (type.Equals("sideways"))
            {

            }
            return temp;
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            planetTemplate = Content.Load<Texture2D>("upscaledBlankPlanet");
            fonts = new List<SpriteFont> { Content.Load<SpriteFont>("planetFont1"), Content.Load<SpriteFont>("planetFont2"), Content.Load<SpriteFont>("planetFont3"), Content.Load<SpriteFont>("planetFont4"), Content.Load<SpriteFont>("planetFont5"), Content.Load<SpriteFont>("shopFont1") };
            
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

            //planet = new Planet(1);
            playScreen = new PlayScreen();
            
            planetTexture = playScreen.planet.PlanetTextureGeneration();
            pixel = Content.Load<Texture2D>("pixel");
            whitePixel = Content.Load<Texture2D>("whitePixel");
            explosionsSheet = Content.Load<Texture2D>("upscaledExplosions");
            ballShip = Content.Load<Texture2D>("ballShip (1)");
            spikyShip = Content.Load<Texture2D>("spikyShip (1)");
            questionMark = Content.Load<Texture2D>("questionMark");
            checkMark = Content.Load<Texture2D>("checkMark");
            shopFont = Content.Load<SpriteFont>("shopFont1");
            shipSheet = Content.Load<Texture2D>("New Piskel (2)");
            cash = Content.Load<Texture2D>("Cash");
            cometSheet = Content.Load<Texture2D>("CometSheet");
            logo = Content.Load<Texture2D>("pDestroyerLogo");
            upgrades = new Upgrades();
            achievements = new AchievementsScreen();
            storeAndPrestige = new StoreAndPrestige();
            settings = new Settings();
            money = new Money();
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
            oldScrollWheel = scrollWheel;
            scrollWheel = mouse.ScrollWheelValue;
            mouseRect.X = mouse.X - 1; mouseRect.Y = mouse.Y - 1;
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            playScreen.Update();
            storeAndPrestige.Update();
            upgrades.Update();
            achievements.Update();
            money.Update();
            settings.Update();
            if (time % 2 == 0)
                planetTexture = playScreen.planet.UpdatePlanetTexture();
            //if (kb.IsKeyDown(Keys.Space) && oldKB.IsKeyUp(Keys.Space))
            //{
            //    planetGrit = rnd.Next(5, 100);
            //    temp = new Color(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
                
            //    planetTexture = planet.PlanetTextureGeneration();
            //}
                
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
            playScreen.Draw(spriteBatch);
            storeAndPrestige.Draw(spriteBatch);
            upgrades.Draw(spriteBatch);
            achievements.Draw(spriteBatch);
            money.Draw(spriteBatch);
            settings.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
