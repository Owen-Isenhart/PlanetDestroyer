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
    public class ModalPopup
    {
        public Rectangle fullScreen, window, exit;
        public bool active, hoveringExit, mouseInBounds;
        public List<Rectangle> buttons;
        public List<bool> buttonStates;
        public List<bool> buttonHovers;
        public List<Color> colors;
        public List<Slider> sliders;
        public List<Vector2> positions;
        public List<string> text;
        public List<Dropdown> dropdowns;
        public SpriteFont font;
        public ModalPopup()
        {
            fullScreen = new Rectangle(0, 0, Game1.screenW, Game1.screenH);
            window = new Rectangle(Game1.screenW / 3, Game1.screenH / 3, Game1.screenW / 3, Game1.screenH / 3);
            exit = new Rectangle(window.Right - 25, window.Y + 5, 20, 20);
            buttons = new List<Rectangle>();
            buttonStates = new List<bool>();
            buttonHovers = new List<bool>();
            sliders = new List<Slider>();
            colors = new List<Color> { Color.Green, Color.DarkGreen, Color.Red, Color.DarkRed };
            active = false;
            mouseInBounds = true;
            text = new List<string>();
            positions = new List<Vector2>();
            dropdowns = new List<Dropdown>();
            font = Game1.getFont(6);
        }
        public void resizeComponents()
        {
            fullScreen = new Rectangle(0, 0, Game1.screenW, Game1.screenH);
            window = new Rectangle(Game1.screenW / 3, Game1.screenH / 3, Game1.screenW / 3, Game1.screenH / 3);
            exit = new Rectangle(window.Right - 25, window.Y + 5, 20, 20);
            font = Game1.getFont(6);
            //resize actual buttons and sliders and stuff
        }

        public void Update()
        {
            if (Game1.mouseRect.Intersects(exit))
            {
                hoveringExit = true;
                if (Game1.mouse.LeftButton == ButtonState.Pressed && Game1.oldMouse.LeftButton == ButtonState.Released)
                {
                    active = false;
                }
            }
            else
                hoveringExit = false;

            for (int i = 0; i < buttons.Count; i++)
            {
                if (Game1.mouseRect.Intersects(buttons[i]))
                {
                    buttonHovers[i] = true;
                    if (Game1.mouse.LeftButton == ButtonState.Pressed && Game1.oldMouse.LeftButton == ButtonState.Released)
                    {
                        buttonStates[i] = !buttonStates[i];
                    }
                }
                else
                {
                    buttonHovers[i] = false;
                }
                
            }
            for (int i = 0; i < sliders.Count; i++)
            {
                sliders[i].Update();
            }
            for (int i = 0; i < dropdowns.Count; i++)
            {
                dropdowns[i].Update();
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.pixel, fullScreen, Color.White * .4f);
            spriteBatch.Draw(Game1.whitePixel, window, Color.White * .8f);
            if (hoveringExit)
                spriteBatch.Draw(Game1.pixel, new Rectangle(exit.X - 1, exit.Y - 1, exit.Width + 2, exit.Height + 2), Color.White) ;
            spriteBatch.Draw(Game1.whitePixel, exit, Color.Red);
            //spriteBatch.DrawString(font, "x", new Vector2(exit.X + exit.Width / 2 - font.MeasureString("x").X / 2, exit.Y + exit.Height / 2 - font.MeasureString("x").Y / 2), Color.Black);

            foreach (Slider s in sliders)
            {
                s.Draw(spriteBatch);
            }
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttonStates[i])
                {
                    if (buttonHovers[i])
                        spriteBatch.Draw(Game1.whitePixel, buttons[i], colors[1]);
                    else
                        spriteBatch.Draw(Game1.whitePixel, buttons[i], colors[0]);
                }
                    
                else
                {
                    if (buttonHovers[i])
                        spriteBatch.Draw(Game1.whitePixel, buttons[i], colors[3]);
                    else
                        spriteBatch.Draw(Game1.whitePixel, buttons[i], colors[2]);
                }
                    
            }
            for (int i = 0; i < text.Count; i++)
            {
                spriteBatch.DrawString(font, text[i], positions[i], Color.Black);
            }
            for (int i = 0; i < dropdowns.Count; i++)
            {
                dropdowns[i].Draw(spriteBatch);
            }
        }
    }
}
