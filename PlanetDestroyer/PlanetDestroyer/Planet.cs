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
    class Planet
    {
        public int Health;
        public Planet(int index) //index increases linearly, as does the amount of hits to blow up the planet
        {
            Health = 100 * index;
        }

        public void Update()
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
