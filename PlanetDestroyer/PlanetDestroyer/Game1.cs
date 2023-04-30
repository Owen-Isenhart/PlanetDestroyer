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

        public Texture2D planetTemplate, planetTexture;
        public Color temp;
        public static Random rnd;

        public int time;

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
            planetTexture = PlanetTextureGeneration();
        }

        public Texture2D PlanetTextureGeneration()
        {
            Texture2D texture = new Texture2D(GraphicsDevice, 400, 400);
            Color[] data = new Color[160000];
            planetTemplate.GetData(data);
            Color darkenedTemp = Darken(temp);
            Color darkenedDarkenedTemp = Darken(darkenedTemp);
            for (int pixel = 0; pixel < data.Count(); pixel++)
            {
                if (data[pixel] == Color.White)
                {
                    if (rnd.Next(0, 200) == 0)
                        data[pixel] = darkenedTemp;
                    else
                        data[pixel] = temp;
                }
                else if (data[pixel] != Color.Transparent && data[pixel] != Color.Black)
                {
                    if (rnd.Next(0, 200) == 0)
                        data[pixel] = darkenedDarkenedTemp;
                    else
                        data[pixel] = darkenedTemp;
                }
            }

            //set the color
            texture.SetData(data);
            return texture;
        }

        public Texture2D UpdatePlanetTexture()
        {
            Color[] data = new Color[160000];
            planetTexture.GetData(data);
            Color[] tempData = new Color[160000];
            planetTemplate.GetData(tempData);
            List<int> alteredIndexes = new List<int>();
            Color darkenedTemp = Darken(temp);
            Color darkenedDarkenedTemp = Darken(darkenedTemp);
            //int pixel = 0;
            for (int pixel = 0; pixel < data.Count(); pixel++)
            {
                if (tempData[pixel] == Color.White)
                {
                    if ((data[pixel + 20] == Color.Transparent || data[pixel + 20] == Color.Black) && rnd.Next(0, 1000) == 0 && data[pixel] == temp)
                        data[pixel] = darkenedTemp;
                    else if (data[pixel + 398] != darkenedTemp && data[pixel + 398] != Color.Transparent && data[pixel + 398] != Color.Black && data[pixel] == darkenedTemp && !alteredIndexes.Contains(pixel))
                    {
                        data[pixel + 398] = darkenedTemp;
                        alteredIndexes.Add(pixel + 398);
                        data[pixel] = temp;
                    }    
                    else if (data[pixel + 398] != Color.Transparent && data[pixel + 398] != Color.Black && data[pixel] == darkenedTemp && !alteredIndexes.Contains(pixel))
                    {
                        data[pixel + 398] = darkenedDarkenedTemp;
                        alteredIndexes.Add(pixel + 398);
                        data[pixel] = temp;
                    }
                    //if (data[pixel + 398])
                    
                }
                else if (tempData[pixel] != Color.Transparent && tempData[pixel] != Color.Black)
                {
                    if (data[pixel + 398] != Color.Transparent && data[pixel] == darkenedDarkenedTemp && !alteredIndexes.Contains(pixel))
                    {
                        if (data[pixel + 398] == Color.Black)
                            data[pixel] = darkenedTemp;
                        else
                        {
                            alteredIndexes.Add(pixel + 398);
                            data[pixel + 398] = darkenedDarkenedTemp;
                            data[pixel] = darkenedTemp;
                        }
                        
                    }
                        
                    
                }
            }

            //for (int pixel = 0; pixel < data.Count(); pixel++)
            //{
            //    if (tempData[pixel] == Color.White)
            //    {
            //        if (rnd.Next(0, 200) == 0)
            //            data[pixel] = darkenedTemp;
            //        else
            //            data[pixel] = temp;
            //    }
            //    else if (tempData[pixel] != Color.Transparent && tempData[pixel] != Color.Black)
            //    {
            //        if (rnd.Next(0, 200) == 0)
            //            data[pixel] = darkenedDarkenedTemp;
            //        else
            //            data[pixel] = darkenedTemp;
            //    }
            //}

            //set the color
            //planetTexture.un
            //planetTexture = null;
            Texture2D t = new Texture2D(GraphicsDevice, 400, 400);
            t.SetData(data);
            return t;
        }
        public Color Darken(Color c)
        {
            return new Color(c.R - 50, c.G - 50, c.B - 50);
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            if (time % 2 == 0)
                planetTexture = UpdatePlanetTexture();
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
            spriteBatch.Draw(planetTexture, new Rectangle(50, 50, 500, 500), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
