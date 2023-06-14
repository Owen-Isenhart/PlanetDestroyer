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
    public class Slider
    {
        public Rectangle knob, line;
        public bool knobHeld, mouseInBounds;
        public int sliderValue;
        public Slider(Rectangle pos)
        {
            line = new Rectangle(pos.X, pos.Y, Game1.screenW / 6, 5);
            knob = new Rectangle(line.Right - 10, line.Y - 3, 10, 11);
            knobHeld = false;
            mouseInBounds = true;
            sliderValue = 100;
        }
        public void Update()
        {
            if (Game1.mouse.X > 0 && Game1.mouse.X < Game1.screenW)
                mouseInBounds = true;
            else
                mouseInBounds = false;

            if (knobHeld && mouseInBounds)
            {
                knob.X = Game1.mouse.X - 5;
                if (knob.X < line.X)
                    knob.X = line.X;
                else if (knob.X > line.Right - knob.Width)
                    knob.X = line.Right - knob.Width;
            }

            if (knobHeld && Game1.mouse.LeftButton == ButtonState.Released)
            {
                knobHeld = false;
            }
            else if (Game1.mouseRect.Intersects(knob) && Game1.oldMouse.LeftButton == ButtonState.Released && Game1.mouse.LeftButton == ButtonState.Pressed)
            {
                knobHeld = true;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.pixel, line, Color.White);
            spriteBatch.Draw(Game1.whitePixel, knob, Color.Green);
        }
    }
}
