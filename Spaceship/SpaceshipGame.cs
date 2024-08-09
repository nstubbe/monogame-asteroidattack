using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spaceship.Asteroid;

namespace Spaceship
{
    public class SpaceshipGame : Game
    {
        private SpriteBatch _spriteBatch;

        private Texture2D _asteroidSprite;
        private Texture2D _shipSprite;
        private Texture2D _spaceSprite;

        private SpriteFont _spaceFont;
        private SpriteFont _timerFont;

        private AsteroidManager _asteroidManager = new AsteroidManager();
        private Ship _player;

        private Vector2 _playerDefaultPosition;

        private double _timer = 0;
        private int _highScore = 0;

        private bool _inGame = false;

        public SpaceshipGame()
        {
            Globals.Init(new GraphicsDeviceManager(this));
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Globals.Graphics.PreferredBackBufferWidth = 1280;
            Globals.Graphics.PreferredBackBufferHeight = 720;
            Globals.Graphics.ApplyChanges();

            _playerDefaultPosition = new Vector2(Globals.Graphics.PreferredBackBufferWidth / 2f,
                Globals.Graphics.PreferredBackBufferHeight / 2f);
            _player = new Ship(_playerDefaultPosition);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _asteroidSprite = Content.Load<Texture2D>("asteroid");
            _shipSprite = Content.Load<Texture2D>("ship");
            _spaceSprite = Content.Load<Texture2D>("space");

            _spaceFont = Content.Load<SpriteFont>("spaceFont");
            _timerFont = Content.Load<SpriteFont>("timerFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (_inGame)
            {
                _player.Update(gameTime);
                _asteroidManager.Update(gameTime);
                _timer += gameTime.ElapsedGameTime.TotalSeconds;
                
                foreach (var asteroid in _asteroidManager.Asteroids)
                {
                    int sumOfRadius = _player.Radius + asteroid.Radius;
                    if (Vector2.Distance(asteroid.Position, _player.Position) < sumOfRadius)
                    {
                        var currentScore = (int)Math.Ceiling(_timer);
                        _highScore = currentScore > _highScore ? currentScore : _highScore;
                        _inGame = false;
                    }
                }
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    _inGame = true;
                    _asteroidManager = new AsteroidManager();
                    _player = new Ship(_playerDefaultPosition);
                    _timer = 0;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(_spaceSprite, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(_shipSprite,
                new Vector2(_player.Position.X - _shipSprite.Width / 2f,
                    _player.Position.Y - _shipSprite.Height / 2f),
                Color.White);

            if (_inGame)
            {
                foreach (var asteroid in _asteroidManager.Asteroids)
                    _spriteBatch.Draw(_asteroidSprite,
                        new Vector2(asteroid.Position.X - _asteroidSprite.Width / 2f,
                            asteroid.Position.Y - _asteroidSprite.Height / 2f), Color.White);

                _spriteBatch.DrawString(_timerFont, $"Time: {Math.Ceiling(_timer)}s", new Vector2(10, 20), Color.White);
                _spriteBatch.DrawString(_timerFont, $"Speed: {Math.Floor(_asteroidManager.SpeedModifier)}",
                    new Vector2(10, 60), Color.White);
            }
            else
            {
                var menuTitle = "Asteroid Attack";
                var menuDescription = "Press [ENTER] to start!";
                var menuHighscore = $"Highscore: {_highScore}s";

                var titlePositionX = Globals.Graphics.PreferredBackBufferWidth / 2f -
                                     _spaceFont.MeasureString(menuTitle).X / 2f;
                var descriptionPositionX = Globals.Graphics.PreferredBackBufferWidth / 2f -
                                           _spaceFont.MeasureString(menuDescription).X / 2f;
                var HighscorePositionX = Globals.Graphics.PreferredBackBufferWidth / 2f -
                                         _spaceFont.MeasureString(menuHighscore).X / 2f;

                _spriteBatch.DrawString(_spaceFont, menuTitle, new Vector2(titlePositionX, 200), Color.White);
                _spriteBatch.DrawString(_spaceFont, menuDescription, new Vector2(descriptionPositionX, 250),
                    Color.White);

                if (_highScore > 0)
                    _spriteBatch.DrawString(_spaceFont, menuHighscore, new Vector2(HighscorePositionX, 450),
                        Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}