﻿using Microsoft.Xna.Framework;
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
        public int Health, index, time;
        public Rectangle rect;
        public List<Explosion> explosions; //from clicking
        public string text;
        public Vector2 textSize;
        
        public Planet(int i) //index increases linearly, as does the amount of hits to blow up the planet
        {
            index = i;
            Health = 100 * index;
            int size = Game1.screenW / 5;
            rect = new Rectangle(Game1.screenW / 2 - size / 2, Game1.screenH / 2 - size / 2, size, size);
            explosions = new List<Explosion>();
            time = 180;
            text = Health + " Hits";
            textSize = Game1.healthFont.MeasureString(text);
        }

        public void Reset()
        {
            index++;
            Health = 100 * index;
            time = 180;
            explosions.Clear();
            text = Health + " Hits";
            textSize = Game1.healthFont.MeasureString(text);
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

                if (data[pixel] != Color.Black && data[pixel] != Color.Transparent && (data[pixel + 1] == Color.Black || data[pixel - 400] == Color.Black || data[pixel + 400] == Color.Black) && Game1.rnd.Next(0, Game1.planetGrit) == 0)
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
                if (time == 0)
                    Reset();
                else
                {
                    if (time > 120 && time % 2 == 0)
                        rect.X -= 3;
                    else if (time > 120 && time % 2 == 1)
                        rect.X += 3;
                    if (time == 120)
                        explosions.Add(new Explosion(rect, 2));

                    time--;
                }
            }
            else if (Health == 1)
            {
                textSize = Game1.healthFont.MeasureString(text);
                text = Health + " Hit";
            }
            else
            {
                textSize = Game1.healthFont.MeasureString(text);
                text = Health + " Hits";
            }
                
            if (Game1.mouseRect.Intersects(rect) && Game1.mouse.LeftButton == ButtonState.Pressed && Game1.oldMouse.LeftButton == ButtonState.Released)
            {
                Health--;
                explosions.Add(new Explosion(Game1.mouseRect, 0));
            }

            foreach (Explosion explosion in explosions)
                explosion.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Health > 0)
            {
                spriteBatch.Draw(Game1.planetTexture, rect, Color.White);
                spriteBatch.DrawString(Game1.healthFont, text, new Vector2(rect.X - (textSize.X - rect.Width)/2, rect.Y - textSize.Y), Color.Black);
            }
                
            else
                   
            foreach (Explosion explosion in explosions)
                explosion.Draw(spriteBatch);
        }
    }
}