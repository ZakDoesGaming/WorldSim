using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WorldSim
{
    class NewsFeed
    {
        private Texture2D[] textures = new Texture2D[4];
        private Texture2D texture;
        private Vector2 pos = new Vector2(1100, 20);
        private int timeShown;
        private float transparency = 0;
        public void Update()
        {
            timeShown++;
            if (timeShown > 500)
            {
                pos.Y++;
                transparency -= 0.05f;
            }
            else
            {
                transparency += 0.01f;
            }

        }
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, "Test bulletin", pos, Color.White * transparency);
        }
        public void NewNewsBulletin(SpriteBatch spriteBatch, SpriteFont font, string text, string icon)
        {
            //switch (icon)
            //{
            //    case "info": texture = textures[0]; break;
            //    case "infectionWarning": texture = textures[1]; break;
            //    case "warWarning": texture = textures[2]; break;
            //    default: texture = textures[0]; break;
            //}
            spriteBatch.DrawString(font, text, new Vector2(1000, 20), Color.White);
        }
    }
}
