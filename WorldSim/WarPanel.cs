using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WorldSim
{
    class WarPanel
    {
        public Country selectedCountry;
        public bool IsEnabled { get; set; }
        private string assetName;
        private Rectangle rectangle;
        private Texture2D texture;
        public WarPanel(string assetName, Rectangle rectangle)
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
            spriteBatch.DrawString(font, "At  war  with:", new Vector2(rectangle.X + 20, rectangle.Y + 20), Color.Pink);
            int i = 50;
            foreach (Country enemy in selectedCountry.Enemies)
            {
                spriteBatch.DrawString(font, "- " + enemy.Name, new Vector2(rectangle.X + 20, rectangle.Y + i), Color.White);
                i += 30;
            }
            spriteBatch.DrawString(font, "Allies  with:", new Vector2(rectangle.X + 20, rectangle.Y + i), Color.LightBlue);
            i += 30;
            foreach (Country ally in selectedCountry.Allies)
            {
                spriteBatch.DrawString(font, "- " + ally.Name, new Vector2(rectangle.X + 20, rectangle.Y + i), Color.White);
                i += 30;
            }
        }
    }
}
