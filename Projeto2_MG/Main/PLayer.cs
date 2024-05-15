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

        public Player(Vector2 pos) : base(pos) 
        {
            framesPerSecond = 10;
        } 

        public void LoadContent(ContentManager content)
        {
            sTexture = content.Load<Texture2D>("PistolMove/survivor-move_handgun");
            AddAnimation(20);
        }

        public void HandleInput(KeyboardState keyState)
        {
            if(keyState.IsKeyDown(Keys.W)) 
            {

            }
            if (keyState.IsKeyDown(Keys.A))
            {

            }
            if (keyState.IsKeyDown(Keys.S))
            {

            }
            if (keyState.IsKeyDown(Keys.D))
            {

            }
        }
    }
}
