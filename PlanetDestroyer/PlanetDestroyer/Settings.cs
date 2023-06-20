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
    public class Settings
    {
        public Rectangle border;
        public List<Button> buttons;
        public List<ModalPopup> popups;
        public Settings()
        {
            border = new Rectangle((Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2, Game1.screenH / 7 + Game1.screenH - Game1.screenH / 4 + 1, (int)(Game1.screenW / 2.5), Game1.screenH / 7 - 1);
            buttons = new List<Button>();
            popups = new List<ModalPopup>();
            string[] texts = { "Audio", "Video", "Stats" };
            for (int i = 0; i < 3; i++)
            {
                popups.Add(new ModalPopup());
                buttons.Add(new Button(new Rectangle(border.X + border.Width / 15 + border.Width / 3 * i, border.Y + 25, border.Width / 5, 50), texts[i]));
            }
            setupPopups();
        }
        public void setupPopups()
        {
            //audio
            
            popups[0].sliders.Add(new Slider(new Rectangle(popups[0].window.X + popups[0].window.Width / 4, popups[0].window.Y + popups[0].window.Height / 5, popups[0].window.Width / 2, 1)));
            popups[0].sliders.Add(new Slider(new Rectangle(popups[0].window.X + popups[0].window.Width / 4, popups[0].window.Y + (int)(popups[0].window.Height / 2.2), popups[0].window.Width / 2, 1)));
            for (int i = 0; i < 3; i++)
            {
                popups[0].buttons.Add(new Rectangle(popups[0].window.X + (int)(popups[0].window.Width / 3 * (i + 1) - popups[0].window.Width / 4.69), popups[0].window.Y + (int)(popups[0].window.Height / 1.4), popups[0].window.Width / 10, popups[0].window.Width / 10));
                popups[0].buttonHovers.Add(false);
                popups[0].buttonStates.Add(true);
            }

            popups[0].text.Add("Audio"); popups[0].positions.Add(new Vector2(popups[0].window.Center.X - popups[0].font.MeasureString("Audio").X / 2, popups[0].window.Y + 10));
            popups[0].text.Add("Music"); popups[0].positions.Add(new Vector2(popups[0].sliders[0].line.X - popups[0].font.MeasureString("Music").X  - 15, popups[0].sliders[0].line.Y + popups[0].sliders[0].line.Height / 2 - popups[0].font.MeasureString("Music").Y / 2));
            popups[0].text.Add("Sounds"); popups[0].positions.Add(new Vector2(popups[0].sliders[1].line.X - popups[0].font.MeasureString("Sounds").X - 15, popups[0].sliders[1].line.Y + popups[0].sliders[1].line.Height / 2 - popups[0].font.MeasureString("Sounds").Y / 2));
            popups[0].text.Add("Cursor Sounds"); popups[0].positions.Add(new Vector2(popups[0].buttons[0].Center.X - popups[0].font.MeasureString("Cursor Sounds").X / 2, popups[0].buttons[0].Y - popups[0].font.MeasureString("Cursor Sounds").Y - 10));
            popups[0].text.Add("Clicking Sound"); popups[0].positions.Add(new Vector2(popups[0].buttons[1].Center.X - popups[0].font.MeasureString("Clicking Sound").X / 2, popups[0].buttons[1].Y - popups[0].font.MeasureString("Clicking Sound").Y - 10));
            popups[0].text.Add("Explosion Sounds"); popups[0].positions.Add(new Vector2(popups[0].buttons[2].Center.X - popups[0].font.MeasureString("Explosion Sounds").X / 2, popups[0].buttons[2].Y - popups[0].font.MeasureString("Explosion Sounds").Y - 10));

            //video
            List<Rectangle> rects = new List<Rectangle>();
            List<string> text = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                rects.Add(new Rectangle(popups[1].window.Center.X - popups[1].window.Width / 6 - popups[1].window.Width / 10, popups[1].window.Y + popups[1].window.Height / 3 + (popups[1].window.Width / 20 * i), popups[1].window.Width / 5, popups[1].window.Width / 20));
                text.Add(i + "");
            }
            popups[1].dropdowns.Add(new Dropdown(rects, text));
            popups[1].buttons.Add(new Rectangle(popups[1].window.Center.X + popups[1].window.Width / 6 - popups[1].window.Width / 10, rects[0].Y, popups[1].window.Width / 5, popups[1].window.Width / 20));
            popups[1].buttonHovers.Add(false);
            popups[1].buttonStates.Add(false);

            popups[1].text.Add("Video"); popups[1].positions.Add(new Vector2(popups[1].window.Center.X - popups[1].font.MeasureString("Video").X / 2, popups[1].window.Y + 10));
            popups[1].text.Add("Resolution"); popups[1].positions.Add(new Vector2(rects[0].Center.X - popups[1].font.MeasureString("Resolution").X / 2, rects[0].Y - popups[1].font.MeasureString("Resolution").Y - 10));
            popups[1].text.Add("Performance"); popups[1].positions.Add(new Vector2(popups[1].buttons[0].Center.X - popups[1].font.MeasureString("Performance").X / 2, popups[1].buttons[0].Y - popups[1].font.MeasureString("Performance").Y - 10));

            //stats
            popups[2].text.Add("Stats"); popups[2].positions.Add(new Vector2(popups[2].window.Center.X - popups[2].font.MeasureString("Stats").X / 2, popups[2].window.Y + 10));
            
            popups[2].text.Add("Total Playtime: 0");
            popups[2].text.Add("Total Money Earned: 0");
            popups[2].text.Add("Total Money Spent: 0");
            popups[2].text.Add("Total Ships Bought: 0");

            popups[2].text.Add("Total Planets Destroyed: 0");
            popups[2].text.Add("Total Planet Clicks: 0");
            popups[2].text.Add("Total Upgrades Bought: 0");
            popups[2].text.Add("Total Prestiges: 0");

            int y = popups[2].window.Y + popups[2].window.Height / 3;
            int x = popups[2].window.Center.X - (int)popups[2].font.MeasureString("Total Planets Destroyed: 0").X - 12;
            for (int i = 0; i < 8; i++)
            {
                if (i != 0 && i % 4 == 0)
                {
                    y = popups[2].window.Y + popups[2].window.Height / 3;
                    x = popups[2].window.Center.X;
                }
                popups[2].positions.Add(new Vector2(x, y));
                y += (int)popups[2].font.MeasureString("TP").Y + 10;
            }
        }
        public void Update()
        {
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                buttons[i].Update();
                popups[i].Update();
                if (buttons[i].clicked && !Game1.activeModal)
                {
                    popups[i].active = true;
                }
                
                if (popups[i].active)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                Game1.activeModal = true;
            }
            else
            {
                Game1.activeModal = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.pixel, border, Color.White);
            
            for (int i = 0; i < 3; i++)
            {
                buttons[i].Draw(spriteBatch);
                if (popups[i].active)
                    popups[i].Draw(spriteBatch);
            }
        }
    }
}
