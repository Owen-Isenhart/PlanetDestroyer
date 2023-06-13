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
    public class StoreAndPrestige
    {
        public Rectangle storeBorder, prestigebBorder;
        public List<StoreItem> items;
        public List<int> indexes;
        public List<Color> colors;
        public ScrollView grid;
        public int totalDmg;
        public Texture2D[] textures;
        
        public StoreAndPrestige()
        {
            storeBorder = new Rectangle(0, 0, (Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2 - 1, (int)(Game1.screenH / 1.5));
            prestigebBorder = new Rectangle(0, storeBorder.Bottom + 1, (Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2 - 1, Game1.screenH - storeBorder.Bottom);
            items = new List<StoreItem>();
            indexes = new List<int>();
            totalDmg = 0;
            textures = new Texture2D[] { Game1.shipSheet, Game1.ballShip, Game1.spikyShip };

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

                }
                else if (tex == Game1.shipSheet)
                {
                    tex = Game1.ballShip;
                    colors.Add(new Color(255 - (i / 3) * 100, 255 - (i / 3) * 100, 255 ));
                }
                else
                {
                    tex = Game1.shipSheet;
                    colors.Add(new Color(255, 255 - (i / 3) * 100, 255 - (i / 3) * 100));
                }

                
                temp.Add(tex);
            }
            grid = new ScrollView(storeBorder, Game1.shipRects[0], t, temp, colors, 3);
        }
        public Rectangle calculateInitRect(int i)
        {
            return new Rectangle(Game1.playScreen.planet.rect.Center.X-40, Game1.playScreen.planet.rect.Center.Y+(i * 25)-Game1.playScreen.planet.rect.Height/8, Game1.screenW / 25, Game1.screenW / 25);
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
                int y = storeBorder.Y + (int)(Game1.fonts[2].MeasureString("STORE").Y/1.3);
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
            DamagePlanet();
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
                            items.Add(new StoreItem("ship", i, calculateInitRect(i), tex, colors[i]));
                        }
                        //items.Add(new StoreItem("ship", i, calculateInitRect(indexByTexture(tex)), tex));
                        //if (items.Count > 1)
                        //    items[items.Count - 1].angleNext(items[0].angleX, items[0].angleY, items[0].subX, items[0].subY);
                        items = items.OrderByDescending(o => o.index).ToList(); //to get correct overlapping when drawn
                    }
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
            spriteBatch.DrawString(Game1.fonts[2], "STORE", new Vector2(storeBorder.Width/2 - Game1.fonts[2].MeasureString("STORE").X/2, grid.rects[0].Y - (int)(Game1.healthFont.MeasureString("STORE").Y/1.3)), Color.White);
            grid.Draw(spriteBatch);

            foreach (StoreItem item in items)
            {
                item.Update();
                item.Draw(spriteBatch);
            }


            //prestige stuff
            spriteBatch.Draw(Game1.pixel, prestigebBorder, Color.Black);
            spriteBatch.DrawString(Game1.fonts[4], "PRESTIGE", new Vector2(storeBorder.Width / 2 - Game1.fonts[4].MeasureString("PRESTIGE").X / 2, prestigebBorder.Y + 10), Color.White);
        }
    }
}
