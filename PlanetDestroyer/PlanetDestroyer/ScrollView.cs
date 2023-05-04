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
    public class ScrollView
    {
        public Rectangle bigBorder;
        public Rectangle border, scrollbarRect;
        public Dictionary<int, Rectangle> rects;
        public Dictionary<int, Texture2D> textures;
        public Dictionary<int, Color> colors;
        public Dictionary<int, Popup> popups;
        public bool hoveringBorder, clickingBar;
        public int hoveringIndex;
        public int lastRow;
        public int cols;

        public ScrollView(Rectangle b, List<Rectangle> r, List<Texture2D> t, int c)
        {
            cols = c;
            bigBorder = b;
            rects = new Dictionary<int, Rectangle>();
            textures = new Dictionary<int, Texture2D>();
            colors = new Dictionary<int, Color>();
            popups = new Dictionary<int, Popup>();
            for (int i = 0; i < r.Count; i++)
            {
                rects.Add(i, r[i]);
                textures.Add(i, t[i]);
                colors.Add(i, Color.White * .1f);
                popups.Add(i, new Popup());
                popups[i].text = (i+1) + "";
            }
            hoveringIndex = -1;
            hoveringBorder = false;
            clickingBar = false;
            border = new Rectangle(rects[0].X, rects[0].Y, rects[cols-1].X + rects[0].Width - rects[0].X, rects[cols * 3 - 1].Y + rects[0].Height - rects[0].Y);
            int full = rects[cols*3 - 1].Y + rects[cols * 3 - 1].Height - rects[0].Y;
            int columns = rects.Count / cols;
            int cPerF = full / columns;
            scrollbarRect = new Rectangle(border.X + border.Width, border.Y, 8, 3*cPerF)  ;
            lastRow = 3;
        }
        public void calculatePopup(string direction, int index)
        {
            if (direction.Equals("left"))
            {
                
                popups[index].popupRect.X = rects[index].X + rects[index].Width + scrollbarRect.Width + 10;
                popups[index].popupRect.Y = Game1.mouse.Y - popups[index].popupRect.Height / 2;
            }
            else if (direction.Equals("right"))
            {
                popups[index].popupRect.X = rects[index].X - popups[index].popupRect.Width - 10;
                popups[index].popupRect.Y = Game1.mouse.Y - popups[index].popupRect.Height / 2;
            }
            //else if (direction.Equals("up"))
            //{
            //    popupRect.X = rect.X - popupRect.Width - 10;
            //    popupRect.Y = Game1.mouse.Y - popupRect.Height / 2;
            //}
            //else if (direction.Equals("down"))
            //{
            //    popupRect.X = rect.X - popupRect.Width - 10;
            //    popupRect.Y = Game1.mouse.Y - popupRect.Height / 2;
            //}
        }
        public void Update()
        {
            if (Game1.mouseRect.Intersects(border) || Game1.mouseRect.Intersects(scrollbarRect))
                hoveringBorder = true;
            else
                hoveringBorder = false;

            if (hoveringBorder || clickingBar)
            {
                if (Game1.mouseRect.Intersects(scrollbarRect) && !clickingBar && Game1.mouse.LeftButton == ButtonState.Pressed && Game1.oldMouse.LeftButton == ButtonState.Released)
                {
                    clickingBar = true;
                }
                else if (clickingBar && Game1.mouse.LeftButton == ButtonState.Released)
                {
                    clickingBar = false;
                }
                    

                if (clickingBar)
                {
                    int diff = Game1.mouse.Y - Game1.oldMouse.Y;
                    if (scrollbarRect.Y + diff >= border.Y && scrollbarRect.Y + scrollbarRect.Height + diff <= border.Y + border.Height)
                    {
                        scrollbarRect.Y += diff;
                    }
                }

                for (int i = lastRow * cols - cols*3, x = 0; i < lastRow*cols; i++, x++)
                {
                    if (Game1.mouseRect.Intersects(rects[x]))
                    {
                        colors[i] = Color.White * .3f;
                        hoveringIndex = i;
                        if (Game1.mouse.LeftButton == ButtonState.Pressed && Game1.oldMouse.LeftButton == ButtonState.Released)
                        {
                            //clicked
                        }
                    }
                    else
                    {
                        colors[i] = Color.White * .1f;
                    }
                }
            }
            else
            {
                if (hoveringIndex > 0)
                    colors[hoveringIndex] = Color.White * .1f;
                hoveringIndex = -1;
                
            }
            lastRow = finalShownIndex();
        }
        public int finalShownIndex()
        {
            
            int center = scrollbarRect.Center.Y;
            int full = rects[cols * 3 - 1].Y + rects[cols * 3 - 1].Height - rects[0].Y;
            int columns = rects.Count / cols;
            double cPerF = (double)full / (double)columns / 2;
            double[] sections = new double[columns*2];
            for (int i = 0; i < sections.Length; i++)
            {
                sections[i] = cPerF + border.Y;
                cPerF +=(double)full / (double)columns / 2;
            }
            int closestIndex = 0;
            int min = Int32.MaxValue;
            for (int i = 0; i < sections.Length; i++)
            {
                if (Math.Abs(center - sections[i]) < min)
                {
                    min = (int)Math.Abs(center - sections[i]);
                    closestIndex = i / 2 + 2;
                    
                }
                
            }
            Console.WriteLine(closestIndex);
            if (closestIndex > 3)
            {
                updateRects(closestIndex);
                return closestIndex;
            }

            else
            {
                updateRects(3);
                return 3;
            }
                
        }
        public void updateRects(int index)
        {
            //if (index != 3)
            //    Console.WriteLine();
            //index *= 5;
            //for (int i = index - 15, x = 0; i < index; i++, x++)
            //{
            //    rects[i] = rects[x];
               
            //}
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //int index = finalShownIndex() * 5;
            for (int i = lastRow * cols - cols*3, x = 0; i < lastRow * cols; i++, x++)
            {
                //if (rects)
                spriteBatch.Draw(Game1.whitePixel, rects[x], colors[i]);
                spriteBatch.Draw(textures[i], rects[x], Color.White);
                
            }

            for (int i = lastRow * cols - cols*3, x = 0; i < lastRow * cols; i++, x++)
            {
                if (popups[i].shown)
                {
                    spriteBatch.Draw(Game1.whitePixel, popups[x].popupRect, Color.White * .7f);
                    spriteBatch.DrawString(Game1.shopFont, popups[i].text, new Vector2(popups[x].popupRect.X, popups[x].popupRect.Y + 10), Color.Black);
                }
                    

            }
            
            if (hoveringBorder || clickingBar)
                spriteBatch.Draw(Game1.whitePixel, scrollbarRect, Color.White);
        }
    }
}
