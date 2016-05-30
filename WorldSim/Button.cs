using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace WorldSim
{
    class Button
    {
        private Rectangle buttonRectangle;
        private Texture2D texture;
        private string buttonName;
        private string assetName;
        private bool clicked;
        public bool ButtonActive { get; set; }
        System.Timers.Timer timer = new System.Timers.Timer();
        public Button(Rectangle rectangle, string assetName, string buttonName)
        {
            this.buttonRectangle = rectangle;
            this.assetName = assetName;
            this.buttonName = buttonName;
        }
        public delegate void ButtonClicked(string button);

        public event ButtonClicked clickEvent;

        public void Update()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && buttonRectangle.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && this.ButtonActive == true && clicked == false)
            {
                clickEvent(buttonName);
                timer.Interval = 100;
                timer.Elapsed += timer_Tick;
                timer.Start();
                clicked = true;
            }
        }
        public void LoadContent (Game game)
        {
            this.texture = game.Content.Load<Texture2D>(assetName);
        }
        void timer_Tick(object sender, System.EventArgs e)
        {
            clicked = false;
            timer.Stop();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, buttonRectangle, Color.White);
        }
    }
}
