using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WorldSim
{
    class UserInterface
    {
        private Texture2D uiTexture;
        private Texture2D uiTextureNoCountry;
        private Texture2D currentFlag;
        private Texture2D[] flags = new Texture2D[2];
        public void LoadContent(Game game)
        {
            uiTexture = game.Content.Load<Texture2D>("UI/uiTexture");
            uiTextureNoCountry = game.Content.Load<Texture2D>("UI/uiTexture_noCountry");
            flags[0] = game.Content.Load<Texture2D>("UI/Flags/flag_RU");
            flags[1] = game.Content.Load<Texture2D>("UI/Flags/flag_AU");
        }
        public void Draw(SpriteBatch spriteBatch, string selectedCountry)
        {
            if (selectedCountry != "")
            {
                switch (selectedCountry)
                {
                    case "Australia": currentFlag = flags[1]; break;
                    case "Russia": currentFlag = flags[0]; break;
                }
                spriteBatch.Draw(uiTexture, new Rectangle(0, 0, 1280, 720), Color.White);
                spriteBatch.Draw(currentFlag, new Rectangle(0, 0, 1280, 720), Color.White);
            }
            else
            {
                spriteBatch.Draw(uiTextureNoCountry, new Rectangle(0, 0, 1280, 720), Color.White);
            }
        }
    }
}
