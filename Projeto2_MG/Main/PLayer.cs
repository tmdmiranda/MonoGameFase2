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
     class Player : AnimatedSprite
    {
        float mySpeed = 150;

        public Player(Vector2 pos) : base(pos) 
        {
            framesPerSecond = 10;
        } 

        public void LoadContent(ContentManager content)
        {
            sTexture = content.Load<Texture2D>("PistolMove/survivor-move_handgun");
            AddAnimation(20);
        }

        public override void Update(GameTime gameTime)
        {
            sDirection = Vector2.Zero;
            HandleInput(Keyboard.GetState());

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            sDirection *= mySpeed;
            sPosition += (sDirection * deltaTime);

            base.Update(gameTime);
        }

        public void HandleInput(KeyboardState keyState)
        {
            if(keyState.IsKeyDown(Keys.W)) 
            {
                sDirection += new Vector2(0, -1);
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                sDirection += new Vector2(-1, 0);
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                sDirection += new Vector2(0, 1);
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                sDirection += new Vector2(1, 0);
            }
        }
    }
}
