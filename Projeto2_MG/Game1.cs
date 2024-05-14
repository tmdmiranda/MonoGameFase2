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
        TextureHandler textureHandler;

        private int GAME_WIDTH = 1300;
        private int GAME_HEIGHT = 700;
        private int _virtualWidth = 1300;
        private int _virtualHeight = 700;
        private MapLevel map = new MapLevel();
        private State _currentState;
        private int tilSize = 64;
        private State _nextState;
        private Texture2D[] textures = new Texture2D[10];
        private Texture2D backgroundTexture;
        private static TextureHandler slicer;
        public Matrix _screenScaleMatrix;
        private bool _isResizing;
        private Viewport _viewport;
        Rectangle[] xxyy;


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
            Window.ClientSizeChanged += OnClientSizeChanged;
            textureHandler = new TextureHandler(Content, GraphicsDevice);

        }

        private void OnClientSizeChanged(object sender, EventArgs e)
        {
            if(!_isResizing && Window.ClientBounds.Width > 0 && Window.ClientBounds.Height > 0)
            {
                _isResizing = true;
                updateScreenScaleMatrix();
                _isResizing = false;
            }
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
            updateScreenScaleMatrix();
            xxyy = new Rectangle[10];
            xxyy[0] = new Rectangle(384, 320,tilSize * 10,tilSize * 10);
            xxyy[1] = new Rectangle(192, 0, tilSize, tilSize);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textures[0] = getTexture("tiles");
            
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
            GraphicsDevice.Clear(Color.Black);

            GraphicsDevice.Viewport = _viewport;

            spriteBatch.Begin(transformMatrix: _screenScaleMatrix);
            _currentState.Draw(gameTime, spriteBatch);
          //  for (int i = 0; i < GAME_WIDTH; i += tilSize)
          //  {
          //      for (int j = 0; j < GAME_HEIGHT; j += tilSize)
          //      {
          //          spriteBatch.Draw(textures[0], new Vector2(i, j), xxyy[0], Color.White);
          //     }
          //  }
          // spriteBatch.Draw(textures[0], new Vector2(256, 256), xxyy[1], Color.White);
           //spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void updateScreenScaleMatrix()
        {
            float screenHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;
            float screenWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;

            if (screenWidth / _virtualWidth > screenHeight / _virtualHeight)
            {
                float aspect = screenHeight/ GAME_HEIGHT;
                _virtualWidth = (int)(aspect *GAME_WIDTH);
                _virtualHeight = (int)screenHeight;
            }
            else
            {
                float aspect = screenWidth / GAME_WIDTH;
                _virtualWidth = (int)screenWidth;
                _virtualHeight = (int)(aspect * GAME_HEIGHT);
            }   
            _screenScaleMatrix = Matrix.CreateScale(_virtualWidth / (float)GAME_WIDTH);

            _viewport = new Viewport
            {
                X = (int)(screenWidth / 2 - _virtualWidth / 2),
                Y = (int)(screenHeight / 2 - _virtualHeight / 2),
                Width = _virtualWidth,
                Height = _virtualHeight,
                MinDepth = 0,
                MaxDepth = 1
            };
        }
    }
}