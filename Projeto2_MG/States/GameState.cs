using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto2_MG.Map;

namespace Projeto2_MG.States
{
    public class GameState : State
    {
        private Floor floor;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
        : base(game, graphicsDevice, content)
        {
            // Carrega uma textura grande o suficiente para cobrir toda a tela
            var floorTexture = _content.Load<Texture2D>("Tiles/image3");

            // Posiciona o chão na parte inferior da tela
            var floorPosition = new Vector2(0, _graphicsDevice.Viewport.Height - floorTexture.Height);

            // Cria uma instância da classe Floor
            floor = new Floor(_game, floorTexture, floorPosition);

            // Adiciona o chão como um componente do jogo
            _game.Components.Add(floor);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}