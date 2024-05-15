using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Projeto2_MG.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto2_MG.Main
{
    public class Camera
    {
        private Vector2 _windowSize;
        private Vector2 _worldSize;
        private Vector2 _ratio;
        private Vector2 _target;
        public Camera(GraphicsDeviceManager graphics, Vector2 worldSize) : this(graphics, worldSize.X, worldSize.Y) { }

        public Camera(GraphicsDeviceManager graphics, float width = 0f, float height = 0f)
        {
            _target = Vector2.Zero;

            _windowSize = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            if (width == 0 && height == 0)
            {
                _worldSize = _windowSize;
            }
            else if (width == 0)
            {
                _worldSize = new Vector2(_windowSize.X * height / _windowSize.Y, height);
            }
            else if (height == 0)
            {
                _worldSize = new Vector2(width, _windowSize.Y * width / _windowSize.X);
            }
            else
            {
                _worldSize = new Vector2(width, height);
            }

            _ratio = _windowSize / _worldSize;
        }


        public Vector2 Position2Pixels(Vector2 pos)
        {
            
                // Reposicionamento de acordo com o target
                Vector2 tmpPos = pos - (_target - _worldSize / 2f);

                // Virtual 2 pixels
                Vector2 pixels = tmpPos * _ratio;
                // Inverter o eixo Y
                return new Vector2(pixels.X, _windowSize.Y - pixels.Y);
            
        }

        public void LookAt(Vector2 target)
        {
            _target = target;
        }

        public void Zoom(int zoom)
        {
            if (zoom > 0)
            {
                _worldSize = MathF.Pow(0.9f, zoom) * _worldSize;
            }
            else
            {
                _worldSize = MathF.Pow(1.1f, -zoom) * _worldSize;
            }
            _ratio = _windowSize / _worldSize;
        }
        public Vector2 Length2Pixels(Vector2 len)
        {
            return len * _ratio;
        }

    }
}
