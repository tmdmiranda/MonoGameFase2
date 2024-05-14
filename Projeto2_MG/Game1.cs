using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Projeto2_MG.Main;
using Projeto2_MG.States;
using System;

namespace Projeto2_MG
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private int GAME_WIDTH = 1300;
        private int GAME_HEIGHT = 700;
        private MapLevel map = new MapLevel();
        private State _currentState;
        private int tilSize = 64;
        private State _nextState;
        private Texture2D[] textures = new Texture2D[10];
        private Texture2D backgroundTexture;
        private static TextureHandler slicer;
        Rectangle[,] xxyy;

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        public Texture2D getTexture(string textureName)
        {
            return Content.Load<Texture2D>("Textures/" + textureName);
        }
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
           

        }

        public void OnResize(Object sender, EventArgs e)
        {

            if ((graphics.PreferredBackBufferWidth != graphics.GraphicsDevice.Viewport.Width) ||
                (graphics.PreferredBackBufferHeight != graphics.GraphicsDevice.Viewport.Height))
            {
                graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.Viewport.Width;
                graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.Viewport.Height;
                graphics.ApplyChanges();

            }
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = GAME_WIDTH;
            graphics.PreferredBackBufferHeight = GAME_HEIGHT;
            graphics.ApplyChanges();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Adiciona o serviço SpriteBatch ao serviço de gráficos do jogo
            Services.AddService(spriteBatch);
            slicer = new TextureHandler(map.tileSize);
            slicer.getSlices();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textures[8] = getTexture("tiles");
            xxyy = slicer.getSlices();
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

            GAME_WIDTH = graphics.GraphicsDevice.Viewport.Width;
            GAME_HEIGHT = graphics.GraphicsDevice.Viewport.Height;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            spriteBatch.Begin();
            _currentState.Draw(gameTime, spriteBatch);
            for (int i = 0; i < GAME_WIDTH; i += tilSize)
            {
                for (int j = 0; j < GAME_HEIGHT; j += tilSize)
                {
                    spriteBatch.Draw(textures[8], new Vector2(i, j), xxyy[3,6], Color.White);
                }
            }

            //spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}