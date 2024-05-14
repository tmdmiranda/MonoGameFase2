using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto2_MG.Control;

namespace Projeto2_MG.States
{
    internal class CreditState : State
    {
        private SpriteFont font;
        //private string _displayText = "Texto dos creditos"; // Texto inicial
        private Button backButton;
        private List<Component> _components;

        public CreditState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            //font = _content.Load<SpriteFont>("Fonts/Font"); // Carrega a fonte
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            backButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(_graphicsDevice.Viewport.Width - 140, 5), // Posição no canto superior direito
                Text = "Back", // Texto do botão
            };
            backButton.Click += BackButton_Click;

            _components = new List<Component>()
            {
                backButton,
             };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.Black);
            // Desenha o texto no centro da tela
            /*Vector2 textSize = font.MeasureString(_displayText);
            Vector2 textPosition = new Vector2((_graphicsDevice.Viewport.Width - textSize.X) / 2, (_graphicsDevice.Viewport.Height - textSize.Y) / 2);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, _displayText, textPosition, Color.White);
            spriteBatch.End();*/
            backButton.Draw(gameTime, spriteBatch); 
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            // Retorna para o menu principal
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }


        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);

            // Lógica de atualização aqui, se necessário
        }
    }
}
