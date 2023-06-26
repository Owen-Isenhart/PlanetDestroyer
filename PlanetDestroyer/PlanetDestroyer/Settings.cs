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
        public int w, h, popupIndex;
        public bool resize;
       
        public Settings()
        {
            border = new Rectangle((Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2, Game1.screenH / 7 + Game1.screenH - Game1.screenH / 4 + 1, (int)(Game1.screenW / 2.5), Game1.screenH / 7 - 1);
            buttons = new List<Button>();
            popups = new List<ModalPopup>();
            string[] texts = { "Audio", "Video", "Stats" };
            for (int i = 0; i < 3; i++)
            {
                popups.Add(new ModalPopup());
                buttons.Add(new Button(new Rectangle(border.X + border.Width / 15 + border.Width / 3 * i, border.Center.Y - (int)(border.Height / 3.2), border.Width / 5, (int)(border.Height / 2.5)), texts[i]));
            }
            setupPopups();
            resize = false;
            popupIndex = 0;
        }
        public void resizeComponents()
        {
            resize = false;
            border = new Rectangle((Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2, Game1.screenH / 7 + Game1.screenH - Game1.screenH / 4 + 1, (int)(Game1.screenW / 2.5), Game1.screenH / 7 - 1);
            //popups.Clear();
            for (int i = 0; i < 3; i++)
            {
                popups[i].resizeComponents();
                buttons[i].rect = new Rectangle(border.X + border.Width / 15 + border.Width / 3 * i, border.Center.Y - (int)(border.Height / 3.2), border.Width / 5, (int)(border.Height / 2.5));
                buttons[i].font = Game1.getFont(6);
            }

            //audio
            int temp1 = popups[0].sliders[0].sliderValue;
            int temp2 = popups[0].sliders[1].sliderValue;

            popups[0].sliders[0] = (new Slider(new Rectangle(popups[0].window.X + popups[0].window.Width / 4, popups[0].window.Y + popups[0].window.Height / 5, popups[0].window.Width / 2, 1)));
            popups[0].sliders[1] = (new Slider(new Rectangle(popups[0].window.X + popups[0].window.Width / 4, popups[0].window.Y + (int)(popups[0].window.Height / 2.2), popups[0].window.Width / 2, 1)));
            popups[0].sliders[0].setByPercent(temp1);
            popups[0].sliders[1].setByPercent(temp2);
            for (int i = 0; i < 3; i++)
            {
                popups[0].buttons[i] = (new Rectangle(popups[0].window.X + (int)(popups[0].window.Width / 3 * (i + 1) - popups[0].window.Width / 4.69), popups[0].window.Y + (int)(popups[0].window.Height / 1.4), popups[0].window.Width / 10, popups[0].window.Width / 10));

            }
            popups[0].positions[0] = (new Vector2(popups[0].window.Center.X - popups[0].font.MeasureString("Audio").X / 2, popups[0].window.Y + 10));
            popups[0].positions[1] = (new Vector2(popups[0].sliders[0].line.X - popups[0].font.MeasureString("Music").X - 15, popups[0].sliders[0].line.Y + popups[0].sliders[0].line.Height / 2 - popups[0].font.MeasureString("Music").Y / 2));
            popups[0].positions[2] = (new Vector2(popups[0].sliders[1].line.X - popups[0].font.MeasureString("Sounds").X - 15, popups[0].sliders[1].line.Y + popups[0].sliders[1].line.Height / 2 - popups[0].font.MeasureString("Sounds").Y / 2));
            popups[0].positions[3] = (new Vector2(popups[0].buttons[0].Center.X - popups[0].font.MeasureString("Cursor Sounds").X / 2, popups[0].buttons[0].Y - popups[0].font.MeasureString("Cursor Sounds").Y - 10));
            popups[0].positions[4] = (new Vector2(popups[0].buttons[1].Center.X - popups[0].font.MeasureString("Clicking Sound").X / 2, popups[0].buttons[1].Y - popups[0].font.MeasureString("Clicking Sound").Y - 10));
            popups[0].positions[5] = (new Vector2(popups[0].buttons[2].Center.X - popups[0].font.MeasureString("Explosion Sounds").X / 2, popups[0].buttons[2].Y - popups[0].font.MeasureString("Explosion Sounds").Y - 10));

            //video
            List<Rectangle> rects = new List<Rectangle>();
            List<string> text = new List<string> { "Fullscreen", "1600 x 900", "1400 x 750", "1200 x 600", "1000 x 550" };
            for (int i = 0; i < 6; i++)
            {
                rects.Add(new Rectangle(popups[1].window.Center.X - popups[1].window.Width / 6 - popups[1].window.Width / 10, popups[1].window.Y + (int)(popups[1].window.Height / 3.5) + (popups[1].window.Width / 20 * i), popups[1].window.Width / 5, popups[1].window.Width / 20));
                //text.Add(i + "");
            }
            bool h = popups[1].dropdowns[0].hover;
            bool o = popups[1].dropdowns[0].opened;
            int hi = popups[1].dropdowns[0].hoverIndex;
            int si = popups[1].dropdowns[0].selectedIndex;
            popups[1].dropdowns[0] = (new Dropdown(rects, text));
            popups[1].dropdowns[0].hover = h; popups[1].dropdowns[0].opened = o; popups[1].dropdowns[0].hoverIndex = hi; popups[1].dropdowns[0].selectedIndex = si;
            popups[1].buttons[0] = (new Rectangle(popups[1].window.Center.X + popups[1].window.Width / 6 - popups[1].window.Width / 10, rects[0].Y, popups[1].window.Width / 5, popups[1].window.Width / 20));
            popups[1].positions[0] = (new Vector2(popups[1].window.Center.X - popups[1].font.MeasureString("Video").X / 2, popups[1].window.Y + 10));
            popups[1].positions[1] = (new Vector2(rects[0].Center.X - popups[1].font.MeasureString("Resolution").X / 2, rects[0].Y - popups[1].font.MeasureString("Resolution").Y - 10));
            popups[1].positions[2] = (new Vector2(popups[1].buttons[0].Center.X - popups[1].font.MeasureString("Performance").X / 2, popups[1].buttons[0].Y - popups[1].font.MeasureString("Performance").Y - 10));

            //stats
            //int y = popups[2].window.Y + popups[2].window.Height / 3;
            //int x;

            //for (int i = 0; i < 8; i++)
            //{
            //    if (i >= 4)
            //        x = popups[2].window.Right - (int)popups[2].font.MeasureString(popups[2].text[i + 1] + "0000K").X;
            //    else
            //        x = popups[2].window.Center.X - (int)popups[2].font.MeasureString(popups[2].text[i + 1] + "000000K").X;



            //    if (i != 0 && i % 4 == 0)
            //    {
            //        y = popups[2].window.Y + popups[2].window.Height / 3;
            //    }
            //    popups[2].positions.Add(new Vector2(x, y));

            //    y += (int)popups[2].font.MeasureString("TP").Y + 10;


            //}
            int y = popups[2].window.Y + popups[2].window.Height / 3;
            int x;
            popups[2].text[1] = ("Total Playtime (hrs): 0");
            popups[2].text[2] = ("Total Money Earned: $");
            popups[2].text[3] = ("Total Money Spent: $");
            popups[2].text[4] = ("Total Ships Bought: 0");
            popups[2].text[5] = ("Total Planets Destroyed: 0");
            popups[2].text[6] = ("Total Planet Clicks: 0");
            popups[2].text[7] = ("Total Upgrades Bought: 0");
            popups[2].text[8] = ("Total Prestiges: 0");
            popups[2].positions[0] = (new Vector2(popups[2].window.Center.X - popups[2].font.MeasureString("Stats").X / 2, popups[2].window.Y + 10));
            for (int i = 0; i < 8; i++)
            {
                if (i >= 4)
                    x = popups[2].window.Right - (int)popups[2].font.MeasureString(popups[2].text[i + 1] + "0000K").X;
                else
                    x = popups[2].window.Center.X - (int)popups[2].font.MeasureString(popups[2].text[i + 1] + "000000K").X;



                if (i != 0 && i % 4 == 0)
                {
                    y = popups[2].window.Y + popups[2].window.Height / 3;
                }
                popups[2].positions[i + 1] = (new Vector2(x, y));

                y += (int)popups[2].font.MeasureString("TP").Y + 10;


            }
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
            List<string> text = new List<string> { "Fullscreen", "1600 x 900", "1400 x 750", "1200 x 600", "1000 x 550" };
            for (int i = 0; i < 6; i++)
            {
                rects.Add(new Rectangle(popups[1].window.Center.X - popups[1].window.Width / 6 - popups[1].window.Width / 10, popups[1].window.Y + (int)(popups[1].window.Height / 3.5) + (popups[1].window.Width / 20 * i), popups[1].window.Width / 5, popups[1].window.Width / 20));
                //text.Add(i + "");
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
            
            popups[2].text.Add("Total Playtime (hrs): 0");
            popups[2].text.Add("Total Money Earned: $");
            popups[2].text.Add("Total Money Spent: $");
            popups[2].text.Add("Total Ships Bought: 0");

            popups[2].text.Add("Total Planets Destroyed: 0");
            popups[2].text.Add("Total Planet Clicks: 0");
            popups[2].text.Add("Total Upgrades Bought: 0");
            popups[2].text.Add("Total Prestiges: 0");

            int y = popups[2].window.Y + popups[2].window.Height / 3;
            int x;

            for (int i = 0; i < 8; i++)
            {
                if (i >= 4)
                    x = popups[2].window.Right - (int)popups[2].font.MeasureString(popups[2].text[i + 1] + "0000K").X;
                else
                    x = popups[2].window.Center.X - (int)popups[2].font.MeasureString(popups[2].text[i + 1] + "000000K").X;



                if (i != 0 && i % 4 == 0)
                {
                    y = popups[2].window.Y + popups[2].window.Height / 3;
                }
                popups[2].positions.Add(new Vector2(x, y));

                y += (int)popups[2].font.MeasureString("TP").Y + 10;
                

            }
        }
        public void Update()
        {
            resize = false;
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                buttons[i].Update();
                popups[i].Update();
                if (buttons[i].clicked && !Game1.activeSettingsModal)
                {
                    popups[i].active = true;
                    popupIndex = i;
                }
                
                if (popups[i].active)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                Game1.activeSettingsModal = true;
            }
            else
            {
                Game1.activeSettingsModal = false;
            }

            if (popups[0].active)
            {

            }
            if (popups[1].active && popups[1].dropdowns[0].newIndex)
            {
                resize = true;
                if (popups[1].dropdowns[0].selectedIndex == 0)
                {
                    w = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 5;
                    h = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 80;
                        
                }
                else if (popups[1].dropdowns[0].selectedIndex == 1)
                {
                    w = 1600;
                    h = 900;
                }
                else if (popups[1].dropdowns[0].selectedIndex == 2)
                {
                    w = 1400;
                    h = 750;
                }
                else if (popups[1].dropdowns[0].selectedIndex == 3)
                {
                    w = 1200;
                    h = 600;
                }
                else if (popups[1].dropdowns[0].selectedIndex == 4)
                {
                    w = 1000;
                    h = 550;
                }

            }
            if (popups[2].active)
                UpdateStats();
        }
        public void UpdateStats()
        {
            double hours = Math.Round(Game1.gT.TotalGameTime.TotalMinutes / 60, 1);
            string hrs = hours.ToString();
            if (hours > 999)
            {
                hours = Math.Round(hours / 1000, 1);
                hrs = ((int)hours).ToString() + "k";
            }
            double money = Money.lifetimeMoney;
            string mny = money.ToString();
            if (money > 999)
            {
                money = Math.Round(money / 1000, 1);
                mny = ((int)money).ToString() + "k";
            }
            double spent = Money.lifetimeSpent;
            string spt = spent.ToString();
            if (spent > 999)
            {
                spent = Math.Round(spent / 1000, 1);
                spt = ((int)spent).ToString() + "k";
            }
            double ships = Store.totalShips;
            string sps = ships.ToString();
            if (ships > 999)
            {
                ships = Math.Round(ships / 1000, 1);
                sps = ((int)ships).ToString() + "k";
            }
            double destroyed = Planet.totalDestroyed;
            string dest = destroyed.ToString();
            if (destroyed > 999)
            {
                destroyed = Math.Round(destroyed / 1000, 1);
                dest = ((int)destroyed).ToString() + "k";
            }
            double clicks = Planet.totalClicks;
            string cls = clicks.ToString();
            if (clicks > 999)
            {
                clicks = Math.Round(clicks / 1000, 1);
                cls = ((int)clicks).ToString() + "k";
            }
            double upgrades = Upgrades.totalUpgrades;
            string upgs = upgrades.ToString();
            if (upgrades > 999)
            {
                upgrades = Math.Round(upgrades / 1000, 1);
                upgs = ((int)upgrades).ToString() + "k";
            }
            double prestiges = Prestige.totalPrestiges;
            string prest = prestiges.ToString();
            if (prestiges > 999)
            {
                prestiges = Math.Round(prestiges / 1000, 1);
                prest = ((int)prestiges).ToString() + "k";
            }

            popups[2].text[1] = "Total Playtime (hrs): " + hrs; //done
            popups[2].text[2] = "Total Money Earned: $" + mny; //done
            popups[2].text[3] = "Total Money Spent: $" + spt; //done
            popups[2].text[4] = "Total Ships Bought: " + sps; //done
            popups[2].text[5] = "Total Planets Destroyed: " + dest; //done
            popups[2].text[6] = "Total Planet Clicks: " + cls; //done
            popups[2].text[7] = "Total Upgrades Bought: " + upgs; //done
            popups[2].text[8] = "Total Prestiges: " + prest;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.pixel, border, Color.White);
            
            for (int i = 0; i < 3; i++)
            {
                buttons[i].Draw(spriteBatch);                    
            }
            if (popups[popupIndex].active)
                popups[popupIndex].Draw(spriteBatch);
        }
    }
}
