﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Projeto2_MG.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Projeto2_MG.Control.Guns;

namespace Projeto2_MG.Main
{
    public class Player : Entity
    {
        public float rotation;  // Adiciona a variável para armazenar a rotação
        public Gun Weapon { get; set; }
        private Gun _weapon1;
        private Gun _weapon2;
        protected float cooldown;
        protected float cooldownLeft;
        public static float TotalSeconds { get; set; }
        public bool Reloading { get; protected set; }

        public Player(Vector2 pos) : base(pos)
        {
            framesPerSecond = 10;
            Speed = 150;
        }

        public void Reset()
        {
            //_weapon1 = new Pistol();
            //_weapon2 = new Rifle();
            IsDead = false;
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
            sDirection *= Speed;
            sPosition += sDirection * deltaTime;

            // Obtém a posição do mouse
            MouseState mouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);

            // Calcula a direção entre o jogador e o mouse
            Vector2 direction = mousePosition - sPosition;

            // Calcula a rotação em radianos
            rotation = (float)Math.Atan2(direction.Y, direction.X);

            if (cooldownLeft > 0)
            {
                cooldownLeft -= TotalSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (Reloading)
            {
                Reloading = false;
            }

            base.Update(gameTime);
        }

        public void HandleInput(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.W))
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

        public void SwapWeapon()
        {
            //Gun = (Gun == _weapon1) ? _weapon2 : _weapon1;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Desenha o jogador com rotação
            spriteBatch.Draw(
                sTexture,
                sPosition,
                sRectangles[frameIndex],
                Color.White,
                rotation,  // Aplica a rotação
                new Vector2(sRectangles[frameIndex].Width / 2, sRectangles[frameIndex].Height / 2),  // Origem da rotação
                1.0f,
                SpriteEffects.None,
                0f
            );
        }
    }
}
