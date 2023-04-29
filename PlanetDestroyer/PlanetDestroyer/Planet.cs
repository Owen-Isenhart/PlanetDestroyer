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
    class Planet : Animation
    {
        public int Health, index, time;
        public Rectangle rect;
        public List<Explosion> explosions; //from clicking
        
        public Planet(int i) : base("linear", Game1.planetRects) //index increases linearly, as does the amount of hits to blow up the planet
        {
            index = i;
            Health = 100 * index;
            repeat = true;
            rect = new Rectangle(Game1.screenW / 2 - rect.Width / 2, Game1.screenH / 2 - rect.Height / 2, Game1.screenW / 5, Game1.screenH / 5);
            explosions = new List<Explosion>();
            time = 180;
        }

        public void Reset()
        {
            index++;
            Health = 100 * index;
            repeat = true;
            time = 180;
            explosions.Clear();
        }



        public void Update()
        {
            base.Update();

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
                spriteBatch.Draw(Game1.planetTextures[index], rect, Game1.planetRects[frameIndex], Color.White);
            else
                   
            foreach (Explosion explosion in explosions)
                explosion.Draw(spriteBatch);
        }
    }
}
