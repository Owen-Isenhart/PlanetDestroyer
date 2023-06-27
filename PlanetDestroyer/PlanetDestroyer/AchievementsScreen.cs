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
    
    public class AchievementsScreen
    {
        public Rectangle border;
        //public HashSet<Achievement> achievements;
        public List<Rectangle> rects, sourceRects;
        public List<bool> completed;
        public ScrollView grid;
        public Vector2 titlePos;
        public SpriteFont font;
        //public 
        public AchievementsScreen()
        {
            border = new Rectangle((Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2 + (int)(Game1.screenW / 2.5) + 1, Game1.screenH / 2 + 1, (Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2, Game1.screenH / 2);
            //achievements = new HashSet<Achievement>();
            rects = organizeRects(100);
            //sourceRects = Game1.rectsBySheet(1, 3, 300, 300, 3);
            List<Texture2D> temp = new List<Texture2D>() ;
            Texture2D tex;
            string s;
            //List<Color> colors = Enumerable.Repeat(Color.White, rects.Count).ToList();
            List<Color> colors = new List<Color>();
            completed = Enumerable.Repeat(false, rects.Count).ToList();
            List<string> sTemp = new List<string>();
            for (int i = 0; i < rects.Count; i++)
            {
                if (i % 3 == 1)
                {
                    tex = Game1.aMoney;
                    int d = i * 15000;
                    s = "Achievement " + (i + 1) + "\n\nCollect $" + d;
                    colors.Add(new Color(255, 255 - i * 2, 255 - i));
                }
                else if (i % 3 == 0)
                {
                    tex = Game1.aPlanet;
                    int d = (i + 1) * 20;
                    s = "Achievement " + (i + 1) + "\n\nDestroy " + d + " planets";
                    colors.Add(new Color(255 - i, 255, 255 - i * 2));
                }
                else
                {
                    tex = Game1.aShips;
                    int d = i * 15;
                    s = "Achievement " + (i + 1) + "\n\nBuy " + d + " ships";
                    colors.Add(new Color(255 - i * 2, 255 - i, 255));
                }


                sTemp.Add(s);
                temp.Add(tex);
            }
            grid = new ScrollView(border, null, rects, temp, colors, sTemp, 5, 8);
            font = Game1.getFont(2);
            titlePos = textPosition();
        }
        public void resizeComponents()
        {
            border = new Rectangle((Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2 + (int)(Game1.screenW / 2.5) + 1, Game1.screenH / 2 + 1, (Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2, Game1.screenH / 2);
            //achievements = new HashSet<Achievement>();
            rects = organizeRects(100);
            List<Texture2D> temp = new List<Texture2D>();
            Texture2D tex;
            string s;
            List<Color> colors = new List<Color>();
            List<string> sTemp = new List<string>();
            for (int i = 0; i < rects.Count; i++)
            {
                if (i % 3 == 1)
                {
                    tex = Game1.aMoney;
                    int d = i * 15000;
                    s = "Achievement " + (i + 1) + "\n\nCollect $" + d;
                    colors.Add(new Color(255, 255 - i * 2, 255 - i));
                }
                else if (i % 3 == 0)
                {
                    tex = Game1.aPlanet;
                    int d = (i + 1) * 20;
                    s = "Achievement " + (i + 1) + "\n\nDestroy " + d + " planets";
                    colors.Add(new Color(255 - i, 255, 255 - i * 2));
                }
                else
                {
                    tex = Game1.aShips;
                    int d = i * 15;
                    s = "Achievement " + (i + 1) + "\n\nBuy " + d + " ships";
                    colors.Add(new Color(255 - i * 2, 255 - i, 255));
                }
                if (completed[i])
                    s += " - CLAIMED";

                sTemp.Add(s);
                temp.Add(tex);
            }
            grid = new ScrollView(border, null, rects, temp, colors, sTemp, 5, 8);
            font = Game1.getFont(2);
            titlePos = textPosition();
        }
        
        public List<Rectangle> organizeRects(int amnt) //have to change this to reset after 3 rows
        {
            List<Rectangle> list = new List<Rectangle>();
            if (border.Width >= 0)
            {
                int y = border.Y + border.Height / 10;
                //int x = 0;
                for (int i = 0, x = 0; i < amnt; i++, x++)
                {
                    if (i % 5 == 0)
                    {
                        y += (int)(border.Width / 7.5 + 10);
                        x = 0;
                    }

                    list.Add(new Rectangle(border.X + (int)(1.15*x * border.Width / 8) + (int)(border.Width / 6.5), y, border.Width / 8, border.Width / 8));
                }
            }
            return list;
        }
        public string calculateOrientation(int i)
        {
            if (i % 5 == 0)
            {
                return "right";
            }
            if (i % 5 == 4)
            {
                return "left";
            }
            return "";
        }
        public void Update()
        {
            grid.Update();

            for (int i = grid.lastRow * 5 - 15, x = 0; i < grid.lastRow * 5; i++, x++)
            {
                if (grid.hoveringIndex == i)
                {
                    grid.popups[i].shown = true;
                    grid.calculatePopup("right", x);
                }
                else
                {
                    grid.popups[i].shown = false;
                }
                
                if (!completed[i])
                {
                    if (i % 3 == 0) //planet
                    {
                        if (Planet.totalDestroyed >= (i + 1) * 50)
                        {
                            completed[i] = true;
                            grid.popups[i].text += " - CLAIM";
                        }
                    }
                    else if (i % 3 == 1) //money
                    {
                        if (Money.lifetimeMoney >= i * 15000)
                        {
                            completed[i] = true;
                            grid.popups[i].text += " - CLAIM";
                        }
                    }
                    else //shop
                    {
                        if (Store.totalShips >= i * 15)
                        {
                            completed[i] = true;
                            grid.popups[i].text += " - CLAIM";
                        }
                    }
                }
                if (completed[i] && grid.hoveringIndex == i && Game1.mouse.LeftButton == ButtonState.Pressed && Game1.oldMouse.LeftButton == ButtonState.Released && grid.popups[i].text.IndexOf("CLAIMED") == -1)
                {
                    grid.popups[i].text += "ED";
                    Game1.money.IncreaseMoney(i);
                }
                
            }
        }
        public Vector2 textPosition()
        {
            int diff = (int)(border.Width - font.MeasureString("ACHIEVEMENTS").X);
            return new Vector2(border.X + diff/2, border.Y + Game1.fonts[4].MeasureString("ACHIEVEMENTS").Y / 5);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.pixel, border, Color.White);
            grid.Draw(spriteBatch);
            //foreach (Achievement achievement in achievements)
            //{
            //    achievement.Draw(spriteBatch);
            //}

            spriteBatch.DrawString(font, "ACHIEVEMENTS", titlePos, Color.White);
        }
    }
}
