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
        public void LoadContent(Game game)
        {
            texture = game.Content.Load<Texture2D>(assetName);
        }
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
            spriteBatch.DrawString(font, "Science Rating: " + selectedCountry.ScienceRating, new Vector2(rectangle.X + 20, rectangle.Y + 20), Color.White);
        }
    }
}
