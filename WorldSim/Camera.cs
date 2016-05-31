using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WorldSim
{
    class Camera
    {
        protected float _zoom;
        public Matrix _transform;
        public Vector2 _lastPos;
        public Vector2 _pos;
        public Vector2 Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }
 
        public Camera()
        {
            _zoom = 1.0f;
            _pos = new Vector2(0, 0);
        }

        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; if (_zoom < 0.1f) _zoom = 0.1f; }
        }

        public void Move(Vector2 amount)
        {
            _pos += amount;
            if (_pos.X > 240)
                _pos.X = 240;
            else if (_pos.X < -240)
                _pos.X = -240;
            if (_pos.Y > 240)
                _pos.Y = 240;
            else if (_pos.Y < -240)
                _pos.Y = -240;
        }

        public Matrix get_transformation(GraphicsDevice graphicsDevice)
        {
            _transform = Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
                                         Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.0f, graphicsDevice.Viewport.Height * 0.0f, 0));
            return _transform;
        }
    }
}
