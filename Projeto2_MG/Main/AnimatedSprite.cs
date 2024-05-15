using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto2_MG.Main
{
    abstract class AnimatedSprite
    {
        protected Vector2 sPosition;
        protected Texture2D sTexture;
        private Rectangle[] sRectangles;
        private double TimeElapsed;
        private double timeToUpdate;
        private int frameIndex;
        protected Vector2 sDirection = Vector2.Zero;



        public int framesPerSecond
        {
            set { timeToUpdate = (1f / value); }
        }

        public AnimatedSprite(Vector2 position)
        {
            sPosition = position;
        }

        public void AddAnimation(int frames)
        {
            int width = sTexture.Width / frames;
            sRectangles = new Rectangle[frames];
            for(int i = 0; i < frames; i++)
            {
                sRectangles[i] = new Rectangle(i * width, 0, width, sTexture.Height);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            TimeElapsed += gameTime.ElapsedGameTime.TotalSeconds;

            if(TimeElapsed > timeToUpdate) 
            {
                TimeElapsed -= timeToUpdate;

                if(frameIndex < sRectangles.Length-1)
                {
                    frameIndex++;
                }
                else
                {
                    frameIndex = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sTexture, sPosition, sRectangles[frameIndex], Color.White);
        }

    }
}
