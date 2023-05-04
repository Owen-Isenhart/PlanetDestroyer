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
    //public class Achievement : Popup
    //{
    //    public string text;
    //    public bool completed;
    //    public int reward;
    //    public Rectangle rect;
    //    public Color rectColor;
        

    //    public Achievement(string t, int index, Rectangle r) : base()
    //    {
    //        text = t;
    //        completed = false;
    //        reward = index * 100;
    //        rect = r;
    //        rectColor = Color.White * .1f;
            
    //    }
    //    public void Update()
    //    {
    //        //Console.WriteLine(Game1.mouseRect + " " + rect);
    //        if (Game1.mouseRect.Intersects(rect))
    //        {

    //            rectColor = Color.White * .3f;
    //            shown = true;
    //            //calculatePopup();
    //            //popupRect.X = Game1.mouse.X
    //        }
    //        else
    //        {
    //            rectColor = Color.White * .1f;
    //            shown = false;
    //        }
    //    }
        
    //    public void Draw(SpriteBatch spriteBatch)
    //    {
    //        spriteBatch.Draw(Game1.whitePixel, rect, rectColor);
    //        if (completed)
    //        {
    //            spriteBatch.Draw(Game1.checkMark, rect, Color.White);
    //        }
    //        else
    //        {
    //            spriteBatch.Draw(Game1.questionMark, rect, Color.White);
    //        }
            
    //    }
    //    public void DrawPopup(SpriteBatch spriteBatch)
    //    {
    //        if (shown)
    //            spriteBatch.Draw(Game1.whitePixel, popupRect, Color.White * .7f);
    //    }
    //}
    public class AchievementsScreen
    {
        public Rectangle border;
        //public HashSet<Achievement> achievements;
        public List<Rectangle> rects;
        public ScrollView grid;
        //public 
        public AchievementsScreen()
        {
            border = new Rectangle((Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2 + (int)(Game1.screenW / 2.5) + 1, Game1.screenH / 2 + 1, (Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2, Game1.screenH / 2);
            //achievements = new HashSet<Achievement>();
            rects = organizeRects(60);
            populateAchievements();
            List<Texture2D> temp = new List<Texture2D>() ;
            Texture2D tex = Game1.questionMark;
            for (int i = 0; i < rects.Count; i++)
            {
                if (i % 5 == 0)
                {
                    if (tex == Game1.checkMark)
                    {
                        tex = Game1.questionMark;
                    }
                    
                    else
                    {
                        tex = Game1.checkMark;
                    }
                    
                }
                    

                temp.Add(tex);
            }
            grid = new ScrollView(border, rects, temp, 5);
        }
        public void populateAchievements()
        {
            //alternate planets destroyed, money collected, and things bought in shop
            //List<Rectangle> rects = organizeRects(15);
            //change this to a for loop
            //for (int i = 0; i < rects.Count; i++)
            //{
                //achievements.Add(new Achievement("Destroy 5 Planets", 1, rects[i]));
            //}
            
        }
        public List<Rectangle> organizeRects(int amnt) //have to change this to reset after 3 rows
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
            //0, 3, 6 are planet achievements
            //1, 4, 7 are money achievements
            //2, 5, 8 are shop achievement
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

            spriteBatch.DrawString(Game1.fonts[4], "ACHIEVEMENTS", textPosition(), Color.White);
        }
    }
}
