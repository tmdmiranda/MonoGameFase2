using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Projeto2_MG.States;

namespace Projeto2_MG
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private const int GAME_WIDTH = 1300;
        private const int GAME_HEIGHT = 700;

        private State _currentState;

        private State _nextState;
        private Texture2D backgroundTexture;

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = GAME_WIDTH;
            graphics.PreferredBackBufferHeight = GAME_HEIGHT;
            graphics.ApplyChanges();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Adiciona o serviço SpriteBatch ao serviço de gráficos do jogo
            Services.AddService(spriteBatch);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _currentState = new MenuState(this, graphics.GraphicsDevice, Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (_nextState != null)
            {
                _currentState = _nextState;

                _nextState = null;
            }
            _currentState.Update(gameTime);
            _currentState.PostUpdate(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            spriteBatch.Begin();
            _currentState.Draw(gameTime, spriteBatch);
            //spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}