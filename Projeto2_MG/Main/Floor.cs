using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projeto2_MG.Root
{
    public class Floor : DrawableGameComponent
    {
        private Texture2D floorTexture;
        private Vector2 position;

        public Floor(Game game, Texture2D texture, Vector2 position) : base(game)
        {
            floorTexture = texture;
            this.position = position;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();
            spriteBatch.Begin();
            spriteBatch.Draw(floorTexture, position, Color.White);
            spriteBatch.End();
        }
    }
}
