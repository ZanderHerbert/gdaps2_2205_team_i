using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    public class Game1 : Game
    {
        //Declaring fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private int mouseX;
        private int mouseY;
        private bool keyboardControls;
        private Texture2D backdrop;
        private Texture2D backdropSwap;
        private Rectangle backdropPos;
        private Rectangle backdropSwapPos;
        private int viewportWidth;
        private int viewportHeight;
        private int timer;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Initializing variables
            player = new Player(2, new Rectangle(0, 0, 20, 20));
            keyboardControls = false;
            viewportWidth = this.GraphicsDevice.Viewport.Width;
            viewportHeight = this.GraphicsDevice.Viewport.Height;
            backdropPos = new Rectangle(0, 0, viewportWidth * 2, viewportHeight);
            backdropSwapPos = new Rectangle(viewportWidth * 2, 0, viewportWidth * 2, viewportHeight);
            timer = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Loading in (Placeholder) textures
            player.Sprite = Content.Load<Texture2D>("PlayerPlaceholder");
            backdrop = Content.Load<Texture2D>("PlaceholderBackdrop");
            backdropSwap = Content.Load<Texture2D>("PlaceholderBackdropSwap");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Moves a player down if the S key is pressed, up if the W key is pressed, left if the A key is pressed
            // and right if the D key is pressed
            if (keyboardControls)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    player.MoveDown();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    player.MoveUp();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    player.MoveLeft();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    player.MoveRight();
                }
            }
            else
            {
                //Gets the position of the mouse, and causes the player object to move towards that position
                UpdateMouse();
                player.Move(mouseX, mouseY);
            }

            //Moves the backdrop, and if the backdrop goes past a certain x value, gets placed at the right edge of
            //the screen.
            //There are two backdrops because if you were to suddenly reposition the first once it reaches the end
            //it is really jarring, also I mirrored the image for the second backdrop so it looks a bit better
            backdropPos.X -= 2;
            backdropSwapPos.X -= 2;

            if (backdropPos.X == ((-3) * viewportWidth))
            {
                backdropPos.X = viewportWidth;
            }
            if (backdropSwapPos.X == ((-3) * viewportWidth))
            {
                backdropSwapPos.X = viewportWidth;
            }

            //Incrementing timer for later in the project
            timer += 1;

            base.Update(gameTime);
        }
        
        //Finds the current mouse position and stores the values in variables mouseX and mouseY
        protected void UpdateMouse()
        {
            MouseState currentMouse = Mouse.GetState();
            mouseX = currentMouse.X;
            mouseY = currentMouse.Y;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draws the player to the window
            _spriteBatch.Begin();
            _spriteBatch.Draw(backdrop, backdropPos, Color.White);
            _spriteBatch.Draw(backdropSwap, backdropSwapPos, Color.White);
            _spriteBatch.Draw(player.Sprite, player.Position, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
