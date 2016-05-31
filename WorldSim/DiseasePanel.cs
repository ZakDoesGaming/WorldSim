using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WorldSim
{
    class DiseasePanel
    {
        public Country selectedCountry;
        public bool IsEnabled { get; set; }
        private string assetName;
        private Rectangle rectangle;
        private Texture2D texture;
        public DiseasePanel(string assetName, Rectangle rectangle)
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
            spriteBatch.DrawString(font, "Diseases:", new Vector2(rectangle.X + 20, rectangle.Y + 20), Color.Plum);
            int i = 50;
            foreach (Disease disease in selectedCountry.Diseases)
            {
                spriteBatch.DrawString(font, "- " + disease.name, new Vector2(rectangle.X + 20, rectangle.Y + i), Color.White);
                i += 30;
            }
        }
    }
}
