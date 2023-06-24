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
        public List<List<StoreItem>> items;
        public List<int> indexes;
        public List<Color> colors;
        public ScrollView grid;
        public int totalDmg;
        public Texture2D[] textures;
        public List<Texture2D> texs;
        public SpriteFont font;
        public Vector2 titlePos;
        public List<int> widths, heights, prices;
        //public List<int> heights;
        public List<bool> unlocked; //have to do this otherwise the ships do not work correctly lmao
        public List<string> priceOrLocked;
        public static int totalShips;
        //prestige stuff


        public Store()
        {
            storeBorder = new Rectangle(0, 0, (Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2 - 1, (int)(Game1.screenH / 1.5));
            items = new List<List<StoreItem>>();

            for (int j = 0; j < 5; j++)
            {
                items.Add(new List<StoreItem>());
            }
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
            unlocked = new List<bool>();
            priceOrLocked = new List<string>();
            prices = new List<int>();
            for (int i = 0; i < t.Count; i++)
            {
                prices.Add((int)Math.Pow((i + 1) * 2, 4));
                priceOrLocked.Add("LOCKED");

                if (tex == Game1.ballShip)
                {
                    tex = Game1.spikyShip;
                    colors.Add(new Color(255 - (i / 3) * 100, 255 , 255 - (i / 3) * 100));
                    int index = 1 + (i / 3);
                    sTemp.Add("Laser Ship " + index + ": " + (int)Math.Pow(i + 1, 2) + " dps\n\n" + priceOrLocked[i]);
                }
                else if (tex == Game1.shipSheet)
                {
                    tex = Game1.ballShip;
                    colors.Add(new Color(255 - (i / 3) * 100, 255 - (i / 3) * 100, 255 ));
                    int index = 1 + (i / 3);
                    sTemp.Add("Gunner Ship " + index + ": " + (int)Math.Pow(i + 1, 2) + " dps\n\n" + priceOrLocked[i]);
                }
                else
                {
                    tex = Game1.shipSheet;
                    colors.Add(new Color(255, 255 - (i / 3) * 100, 255 - (i / 3) * 100));
                    int index = 1 + (i / 3);
                    sTemp.Add("Missile Ship " + index + ": " + (int)Math.Pow(i + 1, 2) + " dps\n\n" + priceOrLocked[i]);
                }
                
                unlocked.Add(false);
                
                temp.Add(tex);
            }
            unlocked[0] = true; //FINISH DOING THIS, USE IT TO SHADE THE GRID AND CHECK IN UPDATE
            
            //priceOrLocked[0] = "$" + prices[0]; //NEED TO ADD SOMETHING TO SCROLLVIEW CLASS THAT LETS ME CHANGE THE POPUP TEXT, OR I COULD PARSE FOR "\N" AND ALTER THAT PART
            texs = temp;
            grid = new ScrollView(storeBorder, Game1.shipRects[0], t, temp, colors, sTemp, 3);
            string[] text = grid.popups[0].text.Split('\n');
            grid.popups[0].text = text[0] + "\n\n$" + prices[0];
            totalShips = 0;
            widths = new List<int> { Game1.screenW, 1600, 1366, 1280, 1024 };
            heights = new List<int> { Game1.screenH, 900, 768, 720, 576 };
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
            List<int> indexes = new List<int>();
            for (int i = 0; i < items.Count; i++)
            {
                if (indexByTexture(textures[i%3], i) != -1)
                {
                    indexes.Add(i);
                }
            }

            //OWEN, THIS IS YOU FROM THE PAST. THE EASIEST WAY TO DO THIS, EVEN THOUGH ITS DOGSHIT STUPID,
            //IS TO CREATE COPYS OF THE ITEMS ARRAY FOR EACH RESOLUTION AND JUST ADD TO THEM LIKE THE REGULAR ONE

            
        }
        public Rectangle calculateInitRect(int i, int w, int h)
        {
            //return new Rectangle(Game1.playScreen.planet.rect.Center.X - 40, Game1.playScreen.planet.rect.Center.Y + (i * 25) - Game1.playScreen.planet.rect.Height / 8, w / 25, h / 25);
            return new Rectangle(w / 2 - 40, (int)(h / 1.7) + (i * (h / 30)) - Game1.playScreen.planet.rect.Height / 8, w / 28, w / 28);

        }

        public int indexByTexture(Texture2D t, int temp)
        {
            for (int j = 0; j < 5; j++)
            {
                for (int i = items[j].Count - 1; i > -1; i--)
                {
                    if (items[j][i].texture == t && items[j][i].index / 3 == temp)
                    {
                        return i;
                    }
                }
            }
            
            return -1;
        }
        //public int firstIndexByTexture(Texture2D t, int temp)
        //{
        //    for (int i = 0; i < items.Count; i++)
        //    {
        //        if (items[i].texture == t && items[i].index / 3 == temp)
        //        {
        //            return i;
        //        }
        //    }
        //    return -1;
        //}
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
            foreach (StoreItem item in items[0])
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

                    if (Game1.mouse.LeftButton == ButtonState.Pressed && Game1.oldMouse.LeftButton == ButtonState.Released && unlocked[i] && Game1.money.runAmount >= prices[i])
                    {
                        Game1.money.runAmount -= prices[i];

                        int temp = i / 3;
                        Texture2D tex = textures[i % 3];
                        int index = indexByTexture(tex, temp);
                        if (i != unlocked.Count - 1 && unlocked[i + 1] == false)
                            unlocked[i + 1] = true;
                        

                        if (index != -1)
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                StoreItem item = items[j][index].getClone(); //might need to switch
                                items[j].Insert(index + 1, item);
                                items[j][index + 1].rect = items[j][index].positionNext();
                                items[j][index + 1].angleNext();

                            }
                            
                        }
                        else
                        {
                            if (items[0].Count == 0)
                            {
                                for (int j = 0; j < 5; j ++)
                                    items[j].Add(new StoreItem("ship", i, widths[j], calculateInitRect(i, widths[j], heights[j]), tex, colors[i]));
                            }
                                
                            else
                            {
                                for (int j = 0; j < 5; j++)
                                {
                                    StoreItem item = items[j][0].getClone();

                                    item.texture = tex;
                                    item.rect.Y += heights[j] / 45;
                                    item.index = i;
                                    item.color = colors[i];
                                    item.dps = (int)Math.Pow(i + 1, 2);
                                    items[j].Add(item);
                                }
                                
                            }
                            if (i != 11)
                            {
                                string[] t = grid.popups[i + 1].text.Split('\n');
                                t[2] = "$" + prices[i + 1];
                                grid.popups[i + 1].text = t[0] + "\n\n" + t[2];
                            }
                            
                        }

                        string[] text = grid.popups[i].text.Split('\n');
                        prices[i] += (int)Math.Pow((i + 1) * 2, 3);
                        text[2] = "$" + prices[i];
                        grid.popups[i].text = text[0] + "\n\n" + text[2];
                        

                        for (int j = 0; j < 5; j ++)
                            items[j] = items[j].OrderByDescending(o => o.index).ToList(); //to get correct overlapping when drawn
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
            int i = 0;
            foreach (StoreItem item in items[Game1.settings.popups[1].dropdowns[0].selectedIndex])
            {
                item.Update();
                item.Draw(spriteBatch);
                spriteBatch.DrawString(Game1.fonts[7], i + "", new Vector2(item.rect.X, item.rect.Y), Color.White);
                i++;
            }


            
        }
    }
}
