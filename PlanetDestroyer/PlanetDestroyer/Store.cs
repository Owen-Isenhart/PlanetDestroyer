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
        public Rectangle border;
        public HashSet<StoreItem> items;
        public ScrollView grid;
        
        public Store()
        {
            border = new Rectangle(0, 0, (Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2 - 1, Game1.screenH);
            items = new HashSet<StoreItem>();
            
            List<Rectangle> t = organizeRects(21);
            List<Texture2D> temp = new List<Texture2D>();
            Texture2D tex = Game1.questionMark;
            for (int i = 0; i < t.Count; i++)
            {
                if (i % 3 == 0)
                {
                    if (tex == Game1.ship)
                    {
                        tex = Game1.questionMark;
                    }

                    else
                    {
                        tex = Game1.ship;
                    }

                }


                temp.Add(tex);
            }
            grid = new ScrollView(border, t, temp, 3);
        }
        public void populateStore()
        {
            List<Rectangle> rects = organizeRects(5);
            items.Add(new StoreItem("Small Ship", 1, rects[0], Game1.ship));
            items.Add(new StoreItem("Small Ship", 1, rects[1], Game1.ship));
            items.Add(new StoreItem("Small Ship", 1, rects[2], Game1.ship));
            items.Add(new StoreItem("Small Ship", 1, rects[3], Game1.ship));
            items.Add(new StoreItem("Small Ship", 1, rects[4], Game1.ship));
        }
        public List<Rectangle> organizeRects(int amnt)
        {
            //List<Rectangle> list = new List<Rectangle>();
            //if (border.Width >= 320)
            //{
            //    int y = border.Y + 25;
            //    //int x = 0;
            //    for (int i = 0, x = 0; i < amnt; i++, x++)
            //    {
            //        if (i % 3 == 0)
            //        {
            //            y += 110 + border.Height / 23;
            //            x = 0;
            //        }

            //        list.Add(new Rectangle(x * 150 + border.Width/8, y, Game1.screenW/22, Game1.screenW/22));
            //    }
            //}
            //return list;
            List<Rectangle> list = new List<Rectangle>();
            if (border.Width >= 320)
            {
                int y = border.Y + (int)(Game1.fonts[2].MeasureString("STORE").Y/1.3);
                //int x = 0;
                for (int i = 0, x = 0; i < amnt; i++, x++)
                {
                    if (i % 3 == 0)
                    {
                        y += border.Width / 6 + 10;
                        x = 0;
                    }

                    list.Add(new Rectangle(border.X + (int)(1.1 * x * border.Width / 6) + (int)(border.Width / 4.25), y, border.Width / 6, border.Width / 6));
                }
            }
            return list;
        }
        public void Update()
        {
            grid.Update();
            for (int i = grid.lastRow * 3 - 9, x = 0; i < grid.lastRow * 3; i++, x++)
            {
                if (grid.hoveringIndex == i)
                {
                    grid.popups[i].shown = true;
                    grid.calculatePopup("left", x);
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
                //if (achievements.ElementAt(i).shown)
                //    achievements.ElementAt(i).calculatePopup("right");
                //achievements.ElementAt(i).Update();
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(Game1.pixel, border, Color.Black);
            spriteBatch.DrawString(Game1.fonts[2], "STORE", new Vector2(border.Width/2 - Game1.fonts[2].MeasureString("STORE").X/2, grid.rects[0].Y - (int)(Game1.healthFont.MeasureString("STORE").Y/1.3)), Color.White);
            grid.Draw(spriteBatch);
        }
    }
}
