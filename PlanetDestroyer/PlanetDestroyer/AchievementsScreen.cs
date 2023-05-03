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
    public class Achievement : Popup
    {
        public string text;
        public bool completed;
        public int reward;
        public Rectangle rect;
        public Color rectColor;
        

        public Achievement(string t, int index, Rectangle r) : base()
        {
            text = t;
            completed = false;
            reward = index * 100;
            rect = r;
            rectColor = Color.White * .1f;
            
        }
        public void Update()
        {
            //Console.WriteLine(Game1.mouseRect + " " + rect);
            if (Game1.mouseRect.Intersects(rect))
            {

                rectColor = Color.White * .3f;
                shown = true;
                //calculatePopup();
                //popupRect.X = Game1.mouse.X
            }
            else
            {
                rectColor = Color.White * .1f;
                shown = false;
            }
        }
        public void calculatePopup(string direction)
        {
            if (direction.Equals("left"))
            {
                popupRect.X = rect.X - popupRect.Width - 10;
                popupRect.Y = Game1.mouse.Y - popupRect.Height / 2;
            }
            else if (direction.Equals("right"))
            {
                popupRect.X = rect.X - popupRect.Width - 10;
                popupRect.Y = Game1.mouse.Y - popupRect.Height / 2;
            }
            else if (direction.Equals("up"))
            {
                popupRect.X = rect.X - popupRect.Width - 10;
                popupRect.Y = Game1.mouse.Y - popupRect.Height / 2;
            }
            else if (direction.Equals("down"))
            {
                popupRect.X = rect.X - popupRect.Width - 10;
                popupRect.Y = Game1.mouse.Y - popupRect.Height / 2;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.whitePixel, rect, rectColor);
            if (completed)
            {
                spriteBatch.Draw(Game1.checkMark, rect, Color.White);
            }
            else
            {
                spriteBatch.Draw(Game1.questionMark, rect, Color.White);
            }
            
        }
        public void DrawPopup(SpriteBatch spriteBatch)
        {
            if (shown)
                spriteBatch.Draw(Game1.whitePixel, popupRect, Color.White * .7f);
        }
    }
    public class AchievementsScreen
    {
        public Rectangle border;
        public HashSet<Achievement> achievements;
        public List<Rectangle> rects;
        public ScrollView grid;
        //public 
        public AchievementsScreen()
        {
            border = new Rectangle((Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2 + (int)(Game1.screenW / 2.5) + 1, Game1.screenH / 2 + 1, (Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2, Game1.screenH / 2);
            achievements = new HashSet<Achievement>();
            rects = organizeRects(30);
            populateAchievements();
            List<Texture2D> temp = Enumerable.Repeat(Game1.questionMark, rects.Count).ToList();
            grid = new ScrollView(border, rects, temp);
        }
        public void populateAchievements()
        {
            //alternate planets destroyed, money collected, and things bought in shop
            //List<Rectangle> rects = organizeRects(15);
            //change this to a for loop
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[0]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[1]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[2]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[3]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[4]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[5]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[6]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[7]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[8]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[9]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[10]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[11])); 
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[12]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[13]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[14]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[15]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[16]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[17]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[18]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[19]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[20]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[21]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[22]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[23]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[24]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[25]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[26]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[27]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[28]));
            achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[29]));
        }
        public List<Rectangle> organizeRects(int amnt)
        {
            List<Rectangle> list = new List<Rectangle>();
            if (border.Width >= 320)
            {
                int y = border.Y + 25;
                //int x = 0;
                for (int i = 0, x = 0; i < amnt; i++, x++)
                {
                    if (i % 5 == 0)
                    {
                        y += border.Width / 8 + 10;
                        x = 0;
                    }

                    list.Add(new Rectangle(border.X + x * border.Width / 7 + (int)(border.Width / 6.5), y, border.Width / 8, border.Width / 8));
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
            //0, 3, 6 are planet achievements
            //1, 4, 7 are money achievements
            //2, 5, 8 are shop achievement
            for (int i = 0; i < achievements.Count; i++)
            {
                if (i % 3 == 0) //planet
                {

                }
                else if (i % 3 == 1) //money
                {

                }
                else //shop
                {

                }
                if (achievements.ElementAt(i).shown)
                    achievements.ElementAt(i).calculatePopup("right");
                achievements.ElementAt(i).Update();
            }
        }
        public Vector2 textPosition()
        {
            int diff = (int)(border.Width - Game1.fonts[4].MeasureString("ACHIEVEMENTS").X);
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

            //spriteBatch.DrawString(Game1.fonts[4], "ACHIEVEMENTS", textPosition(), Color.White);
        }
    }
}
