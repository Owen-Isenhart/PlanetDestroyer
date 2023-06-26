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
    public class Planet
    {
        public double Health;
        public int index, time, buffer;
        public Rectangle rect;
        public List<Explosion> explosions; //from clicking
        public string text;
        public Vector2 textSize, smallTextSize;
        public SpriteFont font, smallFont;
        public static int totalDestroyed, totalClicks;

        public Planet(int i) //index increases linearly, as does the amount of hits to blow up the planet
        {
            index = i;
            Health = 500 * index;
            buffer = 5;
            int size = Game1.screenW / 5;
            rect = new Rectangle(Game1.screenW / 2 - size / 2, Game1.screenH / 2 - size / 3, size, size);
            explosions = new List<Explosion>();
            time = 80;
            text = Health + " Hits";
            font = Game1.getFont(0);
            smallFont = Game1.getFont(7);
            textSize = font.MeasureString(text);
            smallTextSize = smallFont.MeasureString("dps: 0 hits/sec");
            Game1.planetGrit = Game1.rnd.Next(5, 50);
            Game1.temp = new Color(Game1.rnd.Next(0, 255), Game1.rnd.Next(0, 255), Game1.rnd.Next(0, 255));
            while (Darken(Game1.temp) == Color.Black || Darken(Darken(Game1.temp)) == Color.Black)
            {
                Game1.temp = new Color(Game1.rnd.Next(0, 255), Game1.rnd.Next(0, 255), Game1.rnd.Next(0, 255));
            }
            Game1.planetTexture = PlanetTextureGeneration();
            totalDestroyed = 0;
            totalClicks = 0;
        }
        public void resizeComponents()
        {
            rect = new Rectangle(Game1.screenW / 2 - Game1.screenW / 5 / 2, Game1.screenH / 2 - Game1.screenW / 5 / 3, Game1.screenW / 5, Game1.screenW / 5);
            font = Game1.getFont(0);
            textSize = font.MeasureString(text);
            smallFont = Game1.getFont(7);
            smallTextSize = smallFont.MeasureString("dps: 0 hits/sec");
        }

        public void Reset()
        {
            index++;
            Health = 500 * index;
            time = 80;
            //explosions.Clear();
            text = Math.Round(Health) + " Hits";
            textSize = font.MeasureString(text);
            Game1.planetGrit = Game1.rnd.Next(5, 50);
            Game1.temp = new Color(Game1.rnd.Next(0, 255), Game1.rnd.Next(0, 255), Game1.rnd.Next(0, 255));
            while (Darken(Game1.temp) == Color.Black || Darken(Darken(Game1.temp)) == Color.Black)
            {
                Game1.temp = new Color(Game1.rnd.Next(0, 255), Game1.rnd.Next(0, 255), Game1.rnd.Next(0, 255));
            }
            Game1.planetTexture = PlanetTextureGeneration();
        }
        public Texture2D PlanetTextureGeneration()
        {
            Texture2D texture = new Texture2D(Game1.gd, 400, 400);
            Color[] data = new Color[160000];
            Game1.planetTemplate.GetData(data);
            Color darkenedTemp = Darken(Game1.temp);
            Color darkenedDarkenedTemp = Darken(darkenedTemp);
            for (int pixel = 0; pixel < data.Count(); pixel++)
            {
                if (data[pixel] == Color.White)
                {
                    if (Game1.rnd.Next(0, Game1.planetGrit) == 0)
                        data[pixel] = darkenedTemp;
                    else
                        data[pixel] = Game1.temp;
                }
                else if (data[pixel] != Color.Transparent && data[pixel] != Color.Black)
                {
                    if (Game1.rnd.Next(0, Game1.planetGrit) == 0)
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
            Game1.planetTexture.GetData(data);

            Color[] tempData = new Color[160000];
            Game1.planetTemplate.GetData(tempData);

            HashSet<int> indexes = new HashSet<int>();
            Color darkenedTemp = Darken(Game1.temp);
            Color darkenedDarkenedTemp = Darken(darkenedTemp);

            for (int pixel = 0; pixel < data.Count(); pixel++)
            {
                while (data[pixel] == Color.Black || data[pixel] == Color.Transparent)
                {
                    if (pixel >= 1600) break;
                    pixel++;
                }

                if (data[pixel] != Color.Black && data[pixel] != Color.Transparent && (data[pixel + 1] == Color.Black || data[pixel - 400] == Color.Black || data[pixel + 400] == Color.Black) && Game1.rnd.Next(0, (int)(Game1.planetGrit/1.8)) == 0)
                {
                    if (tempData[pixel] == Color.White)
                        data[pixel] = darkenedTemp;
                    else
                        data[pixel] = darkenedDarkenedTemp;
                } 

                if (tempData[pixel] == Color.White)
                {
                    
                    if (data[pixel + 398] != darkenedTemp && data[pixel + 398] != Color.Transparent && data[pixel + 398] != Color.Black && data[pixel] == darkenedTemp && !indexes.Contains(pixel))
                    {
                        data[pixel + 398] = darkenedTemp;
                        indexes.Add(pixel + 398);
                        data[pixel] = Game1.temp;
                    }
                    else if (data[pixel + 398] != Color.Transparent && data[pixel + 398] != Color.Black && (data[pixel] == darkenedTemp || data[pixel] == darkenedDarkenedTemp) && !indexes.Contains(pixel))
                    {
                        data[pixel + 398] = darkenedDarkenedTemp;
                        indexes.Add(pixel + 398);
                        data[pixel] = Game1.temp;
                    }

                }
                else if (tempData[pixel] != Color.Transparent && tempData[pixel] != Color.Black)
                {
                    if (data[pixel + 398] != Color.Transparent && data[pixel] == darkenedDarkenedTemp && !indexes.Contains(pixel))
                    {
                        if (data[pixel + 398] == Color.Black)
                            data[pixel] = darkenedTemp;
                        else
                        {
                            indexes.Add(pixel + 398);
                            data[pixel + 398] = darkenedDarkenedTemp;
                            data[pixel] = darkenedTemp;
                        }
                    }
                }
            }
            Texture2D t = new Texture2D(Game1.gd, 400, 400);
            t.SetData(data);
            return t;
        }

        public Color Darken(Color c)
        {
            return new Color(c.R - 50, c.G - 50, c.B - 50);
        }

        public void Update()
        {

            if (Health <= 0)
            {
                Health = 0;
                text = Math.Round(Health) + " Hits";
                textSize = font.MeasureString(text);
                if (time == 0)
                {
                    Reset();

                }

                else
                {
                    if (time > 20 && time % 2 == 0)
                        rect.X -= 5;
                    else if (time > 20 && time % 2 == 1)
                        rect.X += 5;
                    else if (time == 20)
                    {
                        explosions.Add(new Explosion(rect, "large"));
                        explosions[explosions.Count - 1].framesInbetween = 9;
                        Game1.money.IncreaseMoney(index);
                        totalDestroyed++;
                    }
                    time--;
                }
            }
            else
            {
                if (Math.Round(Health) == 1) text = Math.Round(Health) + " Hit";
                else text = Math.Round(Health) + " Hits";
                textSize = font.MeasureString(text);

                if (!Game1.activeSettingsModal && !Game1.activePrestigeModal && Game1.mouseRect.Intersects(rect) && IntersectsPixel(Game1.mouseRect, rect, Game1.planetTexture) && Game1.mouse.LeftButton == ButtonState.Pressed && Game1.oldMouse.LeftButton == ButtonState.Released)
                {
                    Health -= Game1.clickDamage;
                    explosions.Add(new Explosion(new Rectangle(Game1.mouseRect.X - 30, Game1.mouseRect.Y - 20, 40, 40), "small"));
                    totalClicks++;
                    if (Health < 0) Health = 0;
                    //ClickAnimation();
                }
            }

            for (int i = 0; i < explosions.Count; i++)
            {
                explosions[i].Update();
                if (explosions[i].finished)
                {
                    explosions.RemoveAt(i);
                    i--;
                }
            }
            //if (buffer != 5)
            //ClickAnimation();
            smallTextSize = smallFont.MeasureString("dps: " + Game1.store.totalDmg + " hits/sec");
        }
        public void ClickAnimation()
        {
            rect.Width -= buffer; rect.Height -= buffer;
            buffer--;
            if (buffer == 0)
            {
                int size = Game1.screenW / 5;
                rect = new Rectangle(Game1.screenW / 2 - size / 2, Game1.screenH / 2 - size / 2, size, size);
                buffer = 5;
            }
        }
        static bool IntersectsPixel(Rectangle hitbox1, Rectangle hitbox2, Texture2D texture2)
        {
            if (!hitbox1.Intersects(hitbox2))
                return false;
            if (Game1.screenW >= 1400) //for some reason the algorithm breaks down at smaller resolutions
            {
                Color[] colorData2 = new Color[texture2.Width * texture2.Height];
                texture2.GetData(colorData2);

                int top = Math.Max(hitbox1.Top, hitbox2.Top);
                int left = Math.Max(hitbox1.Left, hitbox2.Left);


                if (colorData2[(left - hitbox2.Left) + (top - hitbox2.Top) * texture2.Width] != Color.Transparent)
                    return true;

                return false;
            }
            return true;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.planetTexture, rect, Color.White);
            spriteBatch.DrawString(font, text, new Vector2(rect.X - (textSize.X - rect.Width) / 2, rect.Y - textSize.Y - rect.Height / 10), Color.White);
            spriteBatch.DrawString(smallFont, "dps: " + Game1.store.totalDmg + " hits/sec", new Vector2(rect.X - (smallTextSize.X - rect.Width) / 2, rect.Y - smallTextSize.Y - rect.Height / 18), Color.White);
            foreach (Explosion explosion in explosions)
                explosion.Draw(spriteBatch);
        }
    }
}
