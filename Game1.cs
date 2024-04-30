using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mono_Topic_4_Time_and_Sound
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D bombTexture;
        Rectangle bombRectangle;
        Texture2D explosion;
        Rectangle explosionRect;
        Rectangle windowRectangle;
        SpriteFont timeFont;
        MouseState mouseState;
        float seconds;
        SoundEffect explode;
        bool exploded;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            seconds = 10;
            exploded = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            explosion = Content.Load<Texture2D>("Kaboom");
            explosionRect = new Rectangle(0, 0, 800, 500);
            windowRectangle = new Rectangle(0, 0, 800, 500);
            _graphics.PreferredBackBufferWidth = windowRectangle.Width;
            _graphics.PreferredBackBufferHeight = windowRectangle.Height;
            _graphics.ApplyChanges();
            // TODO: use this.Content to load your game content here
            bombTexture = Content.Load<Texture2D>("bomb"); 
            bombRectangle = new Rectangle(50, 50, 700, 400);
            timeFont = Content.Load<SpriteFont>("Time");
            explode = Content.Load<SoundEffect>("explosion");
            
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            seconds -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (seconds < 0) // Why not ‘seconds == 10’?
                bombRectangle.X = 1000;         
            if (mouseState.LeftButton == ButtonState.Pressed && seconds > 0)
                seconds = 10f;
            if (seconds <= 0 && !exploded)
            {
                explode.Play();
                seconds = 0f;
                exploded = true;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (!exploded)
            {
                _spriteBatch.Draw(bombTexture, bombRectangle, Color.White);
                _spriteBatch.DrawString(timeFont, seconds.ToString("00.0"), new Vector2(270, 200), Color.Black);

            }
            else
                _spriteBatch.Draw(explosion, explosionRect, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
