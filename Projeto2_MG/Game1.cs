using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Projeto2_MG.Main;
using Projeto2_MG.Map;
using Projeto2_MG.States;
using System;

namespace Projeto2_MG
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Point _oldMousePosition;
        private float _mouseSpeed = 0.05f;
        private Vector2 _cameraTarget = Vector2.Zero;
        private int _oldMouseZoom;

        Texture2D[] runningTextures;
        int counter;
        int activeframe;
        private int GAME_WIDTH = 1920;
        private int GAME_HEIGHT = 1080;
        private int _virtualWidth = 1920;
        private int _virtualHeight = 1080;
        private MapLevel map = new MapLevel();
        private State _currentState;
        private int tilSize = 64;
        private State _nextState;
        private Texture2D[] textures = new Texture2D[20];
        private Texture2D backgroundTexture;
        private Player _player;
        public Matrix _screenScaleMatrix;
        private bool _isResizing;
        private Viewport _viewport;
        private Camera _camera;
        Rectangle[] xxyy;
        Player player;

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
        }

        private void OnClientSizeChanged(object sender, EventArgs e)
        {
            if (!_isResizing && Window.ClientBounds.Width > 0 && Window.ClientBounds.Height > 0)
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
            map.LoadMap("level1.txt");
            //_camera = new Camera(graphics, new Vector2(20, 15));
            _camera = new Camera(graphics, width: 40f);
            _oldMousePosition = Mouse.GetState().Position;
            _oldMouseZoom = Mouse.GetState().ScrollWheelValue;




            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            backgroundTexture = Content.Load<Texture2D>("background");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textures[0] = getTexture("tiles");
            xxyy[0] = new Rectangle(384, 320,tilSize / 3,tilSize / 3);
            xxyy[1] = new Rectangle(0, 768, tilSize / 3, tilSize / 3);
            xxyy[2] = new Rectangle(256, 192, tilSize / 3, tilSize / 3);
            xxyy[3] = new Rectangle(512, 576, tilSize / 3, tilSize / 3);
            _currentState = new MenuState(this, graphics.GraphicsDevice, Content);
            player = new Player(new Vector2(100,100));
            player.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }

            
            Point mousePosition = Mouse.GetState().Position;
            Vector2 mouseMovement = mousePosition.ToVector2() - _oldMousePosition.ToVector2();
            _oldMousePosition = mousePosition;
            _cameraTarget += mouseMovement * _mouseSpeed * new Vector2(-1f, 1f);
            _camera.LookAt(_cameraTarget);

            int zoomValue = Mouse.GetState().ScrollWheelValue;
            int delta = (zoomValue - _oldMouseZoom) / 120;
            if (delta != 0)
            {
                Console.WriteLine(delta);
            }
            _oldMouseZoom = zoomValue;
            _camera.Zoom(delta);



            _currentState.Update(gameTime);
            _currentState.PostUpdate(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Atualiza o player
            player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(transformMatrix: _screenScaleMatrix);
            GraphicsDevice.Clear(Color.Black);

            GraphicsDevice.Viewport = _viewport;
            Vector2 pos = new Vector2(10, 7.5f);
            Vector2 size = Vector2.One;
            pos = _camera.Position2Pixels(pos);
            size = _camera.Length2Pixels(size);

            _camera.LookAt(new Vector2(20, 7.5f));
            // _currentState.Draw(gameTime, spriteBatch);

            for (int i = 0; i < GAME_WIDTH; i += tilSize / 3)
           {
               for (int j = 0; j < GAME_HEIGHT; j += tilSize / 3)
               {
                   spriteBatch.Draw(textures[0], new Vector2(i, j), xxyy[2], Color.White);
               }
           }
            map.drawMap(spriteBatch, textures[0], xxyy[0],xxyy[1], xxyy[3]);
            // spriteBatch.Draw(textures[0], new Vector2(256, 256), xxyy[1], Color.White);
            //spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);



            player.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void updateScreenScaleMatrix()
        {
            float screenHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;
            float screenWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;

            if (screenWidth / _virtualWidth > screenHeight / _virtualHeight)
            {
                float aspect = screenHeight / GAME_HEIGHT;
                _virtualWidth = (int)(aspect * GAME_WIDTH);
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
