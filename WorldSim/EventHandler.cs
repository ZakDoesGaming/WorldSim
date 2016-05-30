using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WorldSim
{
    class EventHandler
    {
        MouseState prevMouseState;
        MouseState mouseState = Mouse.GetState();

        public bool DetectClick(Rectangle clickableArea)
        {
            bool clicked = false;
            if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                // We now know the left mouse button is down and it wasn't down last frame
                // so we've detected a click
                // Now find the position 
                Point mousePos = new Point(mouseState.X, mouseState.Y);
                if (clickableArea.Contains(mousePos))
                {
                    clicked = true;
                }
            }

            // Store the mouse state so that we can compare it next frame
            // with the then current mouse state
            prevMouseState = mouseState;
            return clicked;
        }
    }
}
