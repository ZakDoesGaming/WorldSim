using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WorldSim
{
    class SciencePanel
    {
        public Country selectedCountry;
        public bool IsEnabled { get; set; }
        private string assetName;
        private Rectangle rectangle;
        private Texture2D texture;
        public SciencePanel(string assetName, Rectangle rectangle)
        {
            this.assetName = assetName;
            this.rectangle = rectangle;
        }
        public void TogglePanel()
        {
            IsEnabled = !IsEnabled;
        }
        public void LoadContent(Game game)
        {
            texture = game.Content.Load<Texture2D>(assetName);
        }
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
            spriteBatch.DrawString(font, "Science  Rating: " + selectedCountry.ScienceRating, new Vector2(rectangle.X + 20, rectangle.Y + 20), Color.White);
            spriteBatch.DrawString(font, "Current  Research:", new Vector2(rectangle.X + 20, rectangle.Y + 50), Color.LightBlue);
            int i = 80;
            foreach (ScienceEvent research in selectedCountry.ScientificResearch)
            {
                spriteBatch.DrawString(font, "- " + research.Name, new Vector2(rectangle.X + 20, rectangle.Y + i), Color.White);
                i += 30;
            }
        }
    }
}
