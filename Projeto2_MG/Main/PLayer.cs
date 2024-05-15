using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Projeto2_MG.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics.Metrics;

namespace Projeto2_MG.Main
{
    public class Player: Game
    {
        GraphicsDevice graphicsDevice;
        private Texture2D _texture;
        private Vector2 _position;
        private float _rotation;
        private float _speed;
        Texture2D[] runningTextures;
        int counter;
        int activeframe;


        public Player(Texture2D texture, Vector2 startPosition, float speed)
        {
            _texture = texture;
            _position = startPosition;
            _speed = speed;
            _rotation = 0f;
        }

        public virtual void Initialize()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            // Obtém a posição do mouse
            var mouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);


            // Calcula a direção do mouse em relação ao player
            Vector2 direction = mousePosition - _position;
            if (direction.Length() > 0)
            {
                direction.Normalize();

                // Calcula a rotação do player
                _rotation = (float)Math.Atan2(direction.Y, direction.X);

                // Move o player na direção do mouse se a tecla W for pressionada
                var keyboardState = Keyboard.GetState();
                if (keyboardState.IsKeyDown(Keys.W))
                {
                    _position += direction * _speed;
                    
                }
            }
        }

        public virtual void LoadContent()
        {
            runningTextures = new Texture2D[20];
            for (int i = 0; i < 20; i++)
            {
                runningTextures[i] = Content.Load<Texture2D>($"PlayerMovePistol/survivor-move_handgun_{i}");
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            // Desenha o player na posição e rotação calculadas
            spriteBatch.Draw(_texture, _position, null, Color.White, _rotation, new Vector2(_texture.Width / 2, _texture.Height / 2), 1.0f, SpriteEffects.None, 0f);
            counter++;
            if (counter > 50)
            {
                counter = 0;
                activeframe++;

                if (activeframe > runningTextures.Length - 1)
                {
                    activeframe = 0;
                }
                spriteBatch.Draw(runningTextures[activeframe], new Rectangle(100, 100, 100, 100), Color.White);
            }
            spriteBatch.End();
        }
    }
}
