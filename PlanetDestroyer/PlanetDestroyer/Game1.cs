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
    /// june 28 - v1 completed
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int screenW, screenH;
        public static KeyboardState kb, oldKB;
        public static MouseState mouse, oldMouse;
        public static Rectangle mouseRect, oldMouseRect;
        public static int scrollWheel, oldScrollWheel, clickDamage;
        public static Dictionary<string, List<Rectangle>> explosionRects;
        public static Dictionary<string, Texture2D> explosionTextures;
        public static List<Rectangle> shipRects, cometSources;
        public static Texture2D planetTemplate, planetTexture, pixel, ship, ballShip, spikyShip, whitePixel, questionMark, checkMark, shipSheet, cash, cometSheet, logo, prestigeDmg, prestigeCost, prestigeMoney, shipUpgrade, ballUpgrade, spikyUpgrade, clickUpgrade, aPlanet, aMoney, aShips;
        public static Color temp;
        public static Random rnd;
        public static GraphicsDevice gd;
        public static PlayScreen playScreen;
        public static Store store;
        public static Prestige prestige;
        public static Upgrades upgrades;
        public static AchievementsScreen achievements;
        public static Money money;
        public static Settings settings;
        public static int time, planetGrit;
        public static double totalMins, currentMins;
        public static SpriteFont healthFont, shopFont;
        public static List<SpriteFont> fonts;
        public static float soundsVolume;
        public static bool activeSettingsModal, activePrestigeModal;
        public static SoundEffect backgroundMusic;
        public static SoundEffectInstance backgroundInstance;
        public static List<SoundEffect> sounds;
        public static List<SoundEffectInstance> soundInstances;
        public static bool cursorSound, clickSound, scrollSound, performanceMode;
        

        public static GameTime gT;

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

        public void updateScreen(int w, int h)
        {
            graphics.PreferredBackBufferWidth = w;
            graphics.PreferredBackBufferHeight = h;
            graphics.ApplyChanges();
            screenW = w;
            screenH = h;
            //set window position
            var form = (System.Windows.Forms.Form)System.Windows.Forms.Control.FromHandle(this.Window.Handle);
            form.Location = new System.Drawing.Point((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - screenW - 18) / 2, (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 80 - screenH) / 2);

            resizeComponents();
        }
        public static void resizeComponents()
        {
            achievements.resizeComponents();
            playScreen.resizeComponents();
            store.resizeComponents();
            prestige.resizeComponents();
            upgrades.resizeComponents();
            achievements.resizeComponents();
            money.resizeComponents();
            settings.resizeComponents();

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
            InactiveSleepTime = new TimeSpan(0); //makes game continue at full speed when out of focus
            mouse = oldMouse = Mouse.GetState();
            kb = oldKB = Keyboard.GetState();
            mouseRect = new Rectangle(mouse.X - 1, mouse.Y - 1, 2, 2);
            oldMouseRect = new Rectangle(mouse.X - 1, mouse.Y - 1, 2, 2);
            screenW = GraphicsDevice.Viewport.Width;
            screenH = GraphicsDevice.Viewport.Height;
            scrollWheel = oldScrollWheel = mouse.ScrollWheelValue;
            var form = (System.Windows.Forms.Form)System.Windows.Forms.Control.FromHandle(this.Window.Handle);
            form.Location = new System.Drawing.Point((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - screenW - 18) / 2, (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 80 - screenH) / 2);

            rnd = new Random();
            temp = new Color(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            time = 0;
            gd = GraphicsDevice;
            shipRects = new List<Rectangle>();
            int y = 0;
            
            for (int i = 0, x = 0; i < 5; i ++, x++)
            {
                shipRects.Add(new Rectangle(x * 450, y, 450, 450));
                if (x == 1)
                {
                    x = -1;
                    y += 450;
                }
            }
            cursorSound = clickSound = scrollSound = true;
            performanceMode = false;
            cometSources = new List<Rectangle> { new Rectangle(0, 0, 100, 100), new Rectangle(100, 0, 100, 100), new Rectangle(0, 100, 100, 100), new Rectangle(100, 100, 100, 100), new Rectangle(0, 200, 100, 100) };
            explosionRects = new Dictionary<string, List<Rectangle>>();
            explosionTextures = new Dictionary<string, Texture2D>();
            explosionRects["small"] = rectsBySheet(1, 8, 48, 48, 8);
            explosionRects["large"] = rectsBySheet(1, 12, 128, 128, 12);
            planetGrit = rnd.Next(5, 100);
            activeSettingsModal = false;
            activePrestigeModal = false;
            gT = new GameTime();
            clickDamage = 1;
            totalMins = currentMins = 0;
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
        public static List<Rectangle> rectsBySheet(int rows, int cols, int width, int height, int items)
        {
            List<Rectangle> result = new List<Rectangle>();
            int total = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    total++;
                    if (total == items)
                    {
                        return result;
                    }
                    result.Add(new Rectangle(c * width, r * height, width, height));
                }
            }
            return null; //something impossibly wrong has happened
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            backgroundMusic = Content.Load<SoundEffect>("Dystopic-Mayhem");
            backgroundInstance = backgroundMusic.CreateInstance();
            backgroundInstance.IsLooped = true;
            backgroundInstance.Volume = .1f;
            backgroundInstance.Play();

            sounds = new List<SoundEffect> { Content.Load<SoundEffect>("Menu-Selection-Change-G-www.fesliyanstudios.com"), Content.Load<SoundEffect>("menu-button-press-h-wwwfesliyanstudioscom_kXBDSQWh"), Content.Load<SoundEffect>("Menu-Scroll-A-www.fesliyanstudios.com") };
            //soundInstances = new List<SoundEffectInstance> { sounds[0].CreateInstance(), sounds[1].CreateInstance(), sounds[2].CreateInstance() };
            //for (int i = 0; i < 3; i++)
            //{
            //    soundInstances[i].Volume = .1f;
            //}
            
            //MediaPlayer.IsRepeating = true;

            planetTemplate = Content.Load<Texture2D>("upscaledBlankPlanet");
            fonts = new List<SpriteFont> { Content.Load<SpriteFont>("font1"), Content.Load<SpriteFont>("font2"), Content.Load<SpriteFont>("font3"), Content.Load<SpriteFont>("font4"), Content.Load<SpriteFont>("font5"), Content.Load<SpriteFont>("font6"), Content.Load<SpriteFont>("font7"), Content.Load<SpriteFont>("font8"), Content.Load<SpriteFont>("font9"), Content.Load<SpriteFont>("font10"), Content.Load<SpriteFont>("font11") };
            
            

            //planet = new Planet(1);
            playScreen = new PlayScreen();
            
            planetTexture = playScreen.planet.PlanetTextureGeneration();
            pixel = Content.Load<Texture2D>("pixel");
            whitePixel = Content.Load<Texture2D>("whitePixel");
            explosionTextures["small"] = Content.Load<Texture2D>("explosion-6");
            explosionTextures["large"] = Content.Load<Texture2D>("explosion-4");
            ballShip = Content.Load<Texture2D>("ballShip (1)");
            spikyShip = Content.Load<Texture2D>("spikyShip (1)");
            shipSheet = Content.Load<Texture2D>("New Piskel (2)");
            cash = Content.Load<Texture2D>("Cash");
            cometSheet = Content.Load<Texture2D>("CometSheet");
            logo = Content.Load<Texture2D>("pDestroyerLogo");
            prestigeCost = Content.Load<Texture2D>("costPrestige");
            prestigeDmg = Content.Load<Texture2D>("dmgPrestige");
            prestigeMoney = Content.Load<Texture2D>("moneyPrestige");
            ballUpgrade = Content.Load<Texture2D>("ballUpgrade (1)");
            shipUpgrade = Content.Load<Texture2D>("shipUpgrade");
            spikyUpgrade = Content.Load<Texture2D>("spikyUpgrade");
            clickUpgrade = Content.Load<Texture2D>("clickUpgrade");
            aPlanet = Content.Load<Texture2D>("achievements (2)");
            aMoney = Content.Load<Texture2D>("achievements (3)");
            aShips = Content.Load<Texture2D>("achievements (4)");
            prestige = new Prestige();
            settings = new Settings();
            upgrades = new Upgrades();
            achievements = new AchievementsScreen();
            store = new Store();
            money = new Money();

            Load();
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            totalMins += currentMins;
            Save();
            base.OnExiting(sender, args);
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public static SpriteFont getFont(int size) //0 for planet, 1 for store and upgrades, 2 for prestige + achievements, 5 for settings, 6 for popups
        {
            //god why does xna not just have a way to programatically generate and change fonts
            int result = 0;
            if (settings != null)
                result = settings.popups[1].dropdowns[0].selectedIndex;
            //for (int i = 1900, x = 0; i > screenW; i -= 200, x++)
            //{
            //    result = x;
            //}
            if (result + size >= fonts.Count) return fonts[fonts.Count - 1];
            return fonts[result + size];

        }

        public static void PrestigeGame()
        {
            //money prestige
            money.Prestige();

            //planet + playscreen prestige
            playScreen.Prestige();

            //store prestige
            store.Prestige();

            //upgrades prestige
            upgrades.Prestige();
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
            oldMouseRect.X = oldMouse.X - 1; oldMouseRect.Y = oldMouse.Y - 1;
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            playScreen.Update();
            if (!activeSettingsModal && !activePrestigeModal)
            {
                store.Update();
                upgrades.Update();
                achievements.Update();
                money.Update();
                prestige.Update();
                settings.Update();
            }
            else if (!activeSettingsModal && activePrestigeModal)
            {
                prestige.Update();
            }
            else if (activeSettingsModal && !activePrestigeModal)
            {
                settings.Update();
            }
            store.DamagePlanet();
            if (settings.resize)
                updateScreen(settings.w, settings.h);
            if (time % 2 == 0 && !performanceMode)
                planetTexture = playScreen.planet.UpdatePlanetTexture();

            gT = gameTime;
            time++;

            backgroundInstance.Volume = (float)settings.popups[0].sliders[0].sliderValue / 150;
            soundsVolume = (float)settings.popups[0].sliders[1].sliderValue / 200;

            if (settings.popups[0].buttonStates[0])
                cursorSound = true;
            else
                cursorSound = false;

            if (settings.popups[0].buttonStates[1])
                clickSound = true;
            else
                clickSound = false;

            if (settings.popups[0].buttonStates[2])
                scrollSound = true;
            else
                scrollSound = false;

            if (settings.popups[1].buttonStates[0])
                performanceMode = true;
            else
                performanceMode = false;

            if (clickSound && mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
                sounds[1].Play(volume: soundsVolume / 3f, pitch: .05f, pan: 0f);

            currentMins = gT.TotalGameTime.TotalMinutes;

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
            store.Draw(spriteBatch);
            
            upgrades.Draw(spriteBatch);
            achievements.Draw(spriteBatch);
            money.Draw(spriteBatch);
            prestige.Draw(spriteBatch);
            settings.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void Save() //i dont want to use a serializer or something like that so i'm stuck writing like 50 for loops and manually writing to a file
        {

            //Game1
            string str = screenW + "+" + screenH + "+" + clickDamage + "_";


            //achievements
            for (int i = 0; i < achievements.completed.Count; i++)
            {
                str += achievements.completed[i] + "+" + achievements.grid.popups[i].text + "+";
            }
            str += "_";
            
            //money
            str += money.runAmount + "+" + money.lifeAmount + "+" + money.comets + "+" + money.deltaC + "+" + Money.lifetimeMoney + "+" + Money.lifetimeSpent + "_";
            
            //planet
            str += playScreen.planet.index + "+" + Planet.totalDestroyed + "+" + Planet.totalClicks + "_";
            
            //prestige
            str += Prestige.totalPrestiges + "+" + prestige.dmgIncrease + "+" + prestige.moneyIncrease + "+" + prestige.costDecrease + "+" + prestige.dmgPercent + "+" +  prestige.moneyPercent + "+" + prestige.costPercent + "_";
            
            //settings
            str += settings.popups[0].sliders[0].sliderValue + "+" + settings.popups[0].sliders[1].sliderValue + "+"; //slider values
            for (int i = 0; i < settings.popups.Count; i ++)
            {
                for (int j = 0; j < settings.popups[i].buttonStates.Count; j++)
                {
                    str += settings.popups[i].buttonStates[j] + "+"; //all button states
                }
            }
            str += settings.popups[1].dropdowns[0].selectedIndex + "_"; //dropdown

            //store
            str += store.items.Count + "+" + store.items[0].Count + "+";
            for (int i = store.items[0].Count - 1; i > -1; i--)
            {
                str += store.items[0][i].index + "+";
                //for (int j = 0; j < store.items[i].Count; j++)
                //{
                //    //str += store.items[i][j].dps + "+" + store.items[i][j].index + "+" + store.items[i][j].dist + "+" + store.items[i][j].angleX + "+" + store.items[i][j].angleY + "+" + store.items[i][j].subX + "+" + store.items[i][j].subY + "+" + store.items[i][j].rect.X + "+" + store.items[i][j].rect.Y + "+" + store.items[i][j].rect.Width + "+" + store.items[i][j].rect.Height + "+" + store.items[i][j].frameIndex + "+" + store.items[i][j].color.R + "+" + store.items[i][j].color.G + "+" + store.items[i][j].color.B + "+";
                //}
            }
            for (int i = 0; i < store.nonalteredDamages.Count; i++)
            {
                str += store.shipDamages[i] + "+" + store.nonalteredDamages[i] + "+";
            }
            for (int i = 0; i < store.grid.popups.Count; i++)
            {
                str += store.grid.popups[i].text + "+" + store.prices[i] + "+" + store.unlocked[i] + "+";
            }
            str += store.totalDmg + "+" + Store.totalShips + "_";

            //upgrades
            str += Upgrades.totalUpgrades + "+";
            for (int i = 0; i < upgrades.prices.Count; i++)
            {
                str += upgrades.prices[i] + "+" + upgrades.ogPrices[i] + "+";
            }
            for (int i = 0; i < upgrades.increases.Count; i++)
            {
                str += upgrades.increases[i] + "+";
            }
            str += "_";

            //playtime
            str += totalMins;

            string path = System.Windows.Forms.Application.StartupPath + "\\" + Content.RootDirectory + "\\saveFile.txt";
            SaveAndLoad.Save(str, path);
        }
        public void Load()
        {
            string path = System.Windows.Forms.Application.StartupPath + "\\" + Content.RootDirectory + "\\saveFile.txt";
            string str = SaveAndLoad.Load(path);
            List<string> strings = str.Split('_').ToList();

            if (strings.Count < 8)
                return;

            List<string> g1 = strings[0].Split('+').ToList();
            Int32.TryParse(g1[0], out screenW); Int32.TryParse(g1[1], out screenH); Int32.TryParse(g1[2], out clickDamage);
            

            List<string> ach = strings[1].Split('+').ToList();
            int achIndex = 0;
            for (int i = 0; i < achievements.completed.Count; i ++) //no idea if this will work
            {
                bool temp;
                bool worked = bool.TryParse(ach[achIndex], out temp);
                if (worked)
                    achievements.completed[i] = temp;
                achIndex++;
                achievements.grid.popups[i].text = ach[achIndex];
                achIndex++;
                
            }

            List<string> mny = strings[2].Split('+').ToList();
            long.TryParse(mny[0], out money.runAmount); long.TryParse(mny[1], out money.lifeAmount); Int32.TryParse(mny[2], out money.comets); Int32.TryParse(mny[3], out money.deltaC); long.TryParse(mny[4], out Money.lifetimeMoney); long.TryParse(mny[5], out Money.lifetimeSpent);

            List<string> plan = strings[3].Split('+').ToList();
            Int32.TryParse(plan[0], out playScreen.planet.index); 
            playScreen.planet = new Planet(playScreen.planet.index);
            Int32.TryParse(plan[1], out Planet.totalDestroyed); Int32.TryParse(plan[2], out Planet.totalClicks);

            List<string> prest = strings[4].Split('+').ToList();
            Int32.TryParse(prest[0], out Prestige.totalPrestiges); double.TryParse(prest[1], out prestige.dmgIncrease); double.TryParse(prest[2], out prestige.moneyIncrease); double.TryParse(prest[3], out prestige.costDecrease); double.TryParse(prest[4], out prestige.dmgPercent); double.TryParse(prest[5], out prestige.moneyPercent); double.TryParse(prest[6], out prestige.costPercent);

            List<string> sett = strings[5].Split('+').ToList();
            Int32.TryParse(sett[0], out settings.popups[0].sliders[0].sliderValue); Int32.TryParse(sett[1], out settings.popups[0].sliders[1].sliderValue);
            settings.popups[0].sliders[0].setByPercent(settings.popups[0].sliders[0].sliderValue); settings.popups[0].sliders[1].setByPercent(settings.popups[0].sliders[1].sliderValue);
            int sindex = 2;
            for (int i = 0; i < settings.popups.Count; i ++)
            {
                for (int j = 0; j < settings.popups[i].buttonStates.Count; j++)
                {
                    bool temp;
                    bool worked = bool.TryParse(sett[sindex], out temp);
                    if (worked)
                        settings.popups[i].buttonStates[j] = temp;
                    sindex++;
                }
            }
            Int32.TryParse(sett[sindex], out settings.popups[1].dropdowns[0].selectedIndex);

            List<string> sto = strings[6].Split('+').ToList();

            
            int row;
            int col;
            bool allowed = Int32.TryParse(sto[0], out row); Int32.TryParse(sto[1], out col);
            int stoIndex = 2;
            if (allowed)
            {
                for (int e = stoIndex; e < col + 2; e++)
                {
                    stoIndex++;
                    int i;
                    Int32.TryParse(sto[e], out i);
                    int temp = i / 3;

                    Texture2D tex = store.textures[i % 3];
                    int index = store.indexByTexture(tex, temp);
                    

                    if (index != -1)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            StoreItem item = store.items[j][index].getClone(); //might need to switch
                            store.items[j].Insert(index + 1, item);
                            store.items[j][index + 1].rect = store.items[j][index].positionNext();
                            store.items[j][index + 1].angleNext();

                        }

                    }
                    else
                    {
                        if (store.items[0].Count == 0)
                        {
                            for (int j = 0; j < 5; j++)
                                store.items[j].Add(new StoreItem("ship", i, store.widths[j], store.calculateInitRect(i, store.widths[j], store.heights[j]), tex, store.colors[i]));
                        }

                        else
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                StoreItem item = store.items[j][0].getClone();

                                item.texture = tex;
                                item.rect.Y += store.heights[j] / 41;
                                item.index = i;
                                item.color = store.colors[i];
                                item.dps = (int)Math.Pow(i + 1, 2);
                                store.items[j].Add(item);
                            }

                        }
                        

                    }
                    

                    for (int j = 0; j < 5; j++)
                        store.items[j] = store.items[j].OrderByDescending(o => o.index).ToList(); //to get correct overlapping when drawn
                }
                
                for (int i = 0; i < store.nonalteredDamages.Count; i++) //dk if this works
                {
                    ulong dmg;
                    ulong.TryParse(sto[stoIndex], out dmg);
                    store.shipDamages[i] = dmg;
                    stoIndex++;
                    ulong.TryParse(sto[stoIndex], out dmg);
                    store.nonalteredDamages[i] = dmg;
                    stoIndex++;
                }
                for (int i = 0; i < store.grid.popups.Count; i++)
                {
                    store.grid.popups[i].text = sto[stoIndex];
                    stoIndex++;
                    int temp;
                    Int32.TryParse(sto[stoIndex], out temp);
                    store.prices[i] = temp;
                    stoIndex++;
                    bool l;
                    bool.TryParse(sto[stoIndex], out l);
                    store.unlocked[i] = l;
                    stoIndex++;
                }
                ulong.TryParse(sto[stoIndex], out store.totalDmg);
                Int32.TryParse(sto[stoIndex + 1], out Store.totalShips);
            }

            List<string> upg = strings[7].Split('+').ToList();
            bool wo = Int32.TryParse(upg[0], out Upgrades.totalUpgrades);
            if (wo)
            {
                int upIndex = 1;
                for (int i = 0; i < upgrades.prices.Count; i++)
                {
                    int temp;
                    Int32.TryParse(upg[upIndex], out temp);
                    upgrades.prices[i] = temp;
                    upIndex++;
                    Int32.TryParse(upg[upIndex], out temp);
                    upgrades.ogPrices[i] = temp;
                    upIndex++;
                        
                }
                for (int i = 0; i < upgrades.increases.Count; i++)
                {
                    double temp;
                    double.TryParse(upg[upIndex], out temp);
                    upgrades.increases[i] = temp;
                    upIndex++;
                }
            }

            double.TryParse(strings[8], out totalMins);

            updateScreen(screenW, screenH);
        }
    }
}
