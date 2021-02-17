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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Loading in (Placeholder) player sprite
            player.Sprite = Content.Load<Texture2D>("PlayerPlaceholder");
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
            _spriteBatch.Draw(player.Sprite, player.Position, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
