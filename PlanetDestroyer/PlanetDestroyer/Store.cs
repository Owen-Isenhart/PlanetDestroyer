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
    public class Store
    {
        public Rectangle storeBorder;
        public List<StoreItem> items;
        public List<int> indexes;
        public List<Color> colors;
        public ScrollView grid;
        public int totalDmg;
        public Texture2D[] textures;
        public SpriteFont font;
        public Vector2 titlePos;
        public static int totalShips;
        //prestige stuff


        public Store()
        {
            storeBorder = new Rectangle(0, 0, (Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2 - 1, (int)(Game1.screenH / 1.5));
            items = new List<StoreItem>();
            indexes = new List<int>();
            totalDmg = 0;
            font = Game1.getFont(1);
            textures = new Texture2D[] { Game1.shipSheet, Game1.ballShip, Game1.spikyShip };
            int diff = (int)(storeBorder.Width - font.MeasureString("STORE").X);
            titlePos = new Vector2(storeBorder.X + diff / 2, storeBorder.Y + font.MeasureString("STORE").Y / 2);
            List<string> sTemp = new List<string>();
            List<Rectangle> t = organizeRects(12);
            List<Texture2D> temp = new List<Texture2D>();
            colors = new List<Color>();
            Texture2D tex = Game1.spikyShip;
            for (int i = 0; i < t.Count; i++)
            {
                if (tex == Game1.ballShip)
                {
                    tex = Game1.spikyShip;
                    colors.Add(new Color(255 - (i / 3) * 100, 255 , 255 - (i / 3) * 100));
                    int index = 1 + (i / 3);
                    sTemp.Add("Laser Ship " + index + ": " + (int)Math.Pow(i + 1, 2) + " dps");
                }
                else if (tex == Game1.shipSheet)
                {
                    tex = Game1.ballShip;
                    colors.Add(new Color(255 - (i / 3) * 100, 255 - (i / 3) * 100, 255 ));
                    int index = 1 + (i / 3);
                    sTemp.Add("Gunner Ship " + index + ": " + (int)Math.Pow(i + 1, 2) + " dps");
                }
                else
                {
                    tex = Game1.shipSheet;
                    colors.Add(new Color(255, 255 - (i / 3) * 100, 255 - (i / 3) * 100));
                    int index = 1 + (i / 3);
                    sTemp.Add("Missile Ship " + index + ": " + (int)Math.Pow(i + 1, 2) + " dps");
                }

                
                temp.Add(tex);
            }
            grid = new ScrollView(storeBorder, Game1.shipRects[0], t, temp, colors, sTemp, 3);
            totalShips = 0;
        }
        public void resizeComponents()
        {
            storeBorder = new Rectangle(0, 0, (Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2 - 1, (int)(Game1.screenH / 1.5));
            font = Game1.getFont(1);
            textures = new Texture2D[] { Game1.shipSheet, Game1.ballShip, Game1.spikyShip };
            int diff = (int)(storeBorder.Width - font.MeasureString("STORE").X);
            titlePos = new Vector2(storeBorder.X + diff / 2, storeBorder.Y + font.MeasureString("STORE").Y / 2);
            List<string> sTemp = new List<string>();
            List<Rectangle> t = organizeRects(12);
            List<Texture2D> temp = new List<Texture2D>();
            colors = new List<Color>();
            Texture2D tex = Game1.spikyShip;
            for (int i = 0; i < t.Count; i++)
            {
                if (tex == Game1.ballShip)
                {
                    tex = Game1.spikyShip;
                    colors.Add(new Color(255 - (i / 3) * 100, 255, 255 - (i / 3) * 100));
                    int index = 1 + (i / 3);
                    sTemp.Add("Laser Ship " + index + ": " + (int)Math.Pow(i + 1, 2) + " dps");
                }
                else if (tex == Game1.shipSheet)
                {
                    tex = Game1.ballShip;
                    colors.Add(new Color(255 - (i / 3) * 100, 255 - (i / 3) * 100, 255));
                    int index = 1 + (i / 3);
                    sTemp.Add("Gunner Ship " + index + ": " + (int)Math.Pow(i + 1, 2) + " dps");
                }
                else
                {
                    tex = Game1.shipSheet;
                    colors.Add(new Color(255, 255 - (i / 3) * 100, 255 - (i / 3) * 100));
                    int index = 1 + (i / 3);
                    sTemp.Add("Missile Ship " + index + ": " + (int)Math.Pow(i + 1, 2) + " dps");
                }


                temp.Add(tex);
            }
            grid = new ScrollView(storeBorder, Game1.shipRects[0], t, temp, colors, sTemp, 3);
        }
        public Rectangle calculateInitRect(int i)
        {
            return new Rectangle(Game1.playScreen.planet.rect.Center.X - 40, Game1.playScreen.planet.rect.Center.Y + (i * 25) - Game1.playScreen.planet.rect.Height / 8, Game1.screenW / 25, Game1.screenW / 25);
            
        }
        
        public int indexByTexture(Texture2D t, int temp)
        {
            for (int i = items.Count-1; i > -1; i--)
            {
                if (items[i].texture == t && items[i].index/3 == temp)
                {
                    return i;
                }
            }
            return -1;
        }
        public void populateStore()
        {
            List<Rectangle> rects = organizeRects(5);
            
        }
        public List<Rectangle> organizeRects(int amnt)
        {
            
            List<Rectangle> list = new List<Rectangle>();
            if (storeBorder.Width >= 320)
            {
                int y = storeBorder.Y + (int)(font.MeasureString("STORE").Y);
                for (int i = 0, x = 0; i < amnt; i++, x++)
                {
                    if (i % 3 == 0)
                    {
                        y += storeBorder.Width / 6 + 10;
                        x = 0;
                    }

                    list.Add(new Rectangle(storeBorder.X + (int)(1.1 * x * storeBorder.Width / 6) + (int)(storeBorder.Width / 4.25), y, storeBorder.Width / 6, storeBorder.Width / 6));
                }
            }
            return list;
        }
        public void DamagePlanet()
        {
            totalDmg = 0;
            foreach (StoreItem item in items)
            {
                totalDmg += item.dps;
            }
            Game1.playScreen.planet.Health -= (double)totalDmg/60;
        }

        public void Update()
        {
            grid.Update();
            //DamagePlanet();
            for (int i = grid.lastRow * 3 - 9, x = 0; i < grid.lastRow * 3; i++, x++)
            {
                if (grid.hoveringIndex == i)
                {
                    grid.popups[i].shown = true;
                    grid.calculatePopup("left", x);

                    if (Game1.mouse.LeftButton == ButtonState.Pressed && Game1.oldMouse.LeftButton == ButtonState.Released)
                    {
                        int temp = i / 3;
                        Texture2D tex = textures[i % 3];
                        int index = indexByTexture(tex, temp);
                        if (index != -1)
                        {
                            StoreItem item = items[index].getClone();
                            items.Insert(index + 1, item);
                            items[index+1].rect = items[index].positionNext();
                            items[index+1].angleNext();
                        }
                        else
                        {
                            if (items.Count == 0)
                                items.Add(new StoreItem("ship", i, calculateInitRect(i), tex, colors[i]));
                            else
                            {
                                StoreItem item = items[0].getClone();

                                item.texture = tex;
                                item.rect.Y += Game1.screenW / 75;
                                item.index = i;
                                item.color = colors[i];
                                item.dps = (int)Math.Pow(i + 1, 2);
                                items.Add(item);
                            }
                        }
                        //items.Add(new StoreItem("ship", i, calculateInitRect(indexByTexture(tex)), tex));
                        //if (items.Count > 1)
                        //    items[items.Count - 1].angleNext(items[0].angleX, items[0].angleY, items[0].subX, items[0].subY);
                        items = items.OrderByDescending(o => o.index).ToList(); //to get correct overlapping when drawn
                    }

                    //prestige stuff

                }
                else
                {
                    grid.popups[i].shown = false;
                }
                if (i % 3 == 0) //planet
                {

                }
                else if (i % 3 == 1) //money
                {

                }
                else //shop
                {

                }

            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(Game1.pixel, storeBorder, Color.Black);
            spriteBatch.DrawString(font, "STORE", titlePos, Color.White);
            grid.Draw(spriteBatch);
            foreach (StoreItem item in items)
            {
                item.Update();
                item.Draw(spriteBatch);
                //spriteBatch.DrawString(Game1.fonts[7], i + "", new Vector2(item.rect.X, item.rect.Y), Color.White);
                //i++;
            }


            
        }
    }
}
