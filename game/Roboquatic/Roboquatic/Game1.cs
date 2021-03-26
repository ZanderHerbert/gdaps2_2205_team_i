using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace Roboquatic
{
    enum GameState
    {
        Menu,
        Game,
        Pause,
        GameOver,
        Settings
    }

    public class Game1 : Game
    {


        //Declaring fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Player player;
        private bool keyboardControls;
        private Texture2D backdrop;
        private Texture2D backdropSwap;
        private Rectangle backdropPos;
        private Rectangle backdropSwapPos;
        private int viewportWidth;
        private int viewportHeight;
        private int timer;
        private List<Projectile> projectiles;
        private List<Enemy> enemies;
        private Texture2D baseEnemySprite;
        private Texture2D baseEnemyProjectileSprite;
        private Random rng;
        private GameState currentState;
        private KeyboardState previousKbState;
        private SpriteFont font;
        private GameState previousState;
        private EnemyManager enemyManager;
        private ProjectileManager projectileManager;
        private int mouseX;
        private int mouseY;

        // Buttons' fields
        private List<Button> buttons = new List<Button>();
        private Texture2D startButton;
        private Texture2D controlsButton;
        private Texture2D mouseButton;
        private Texture2D kbButton;
        private Texture2D backButton;
        private Texture2D menuButton;
        private Texture2D resumeButton;
        private Texture2D continueButton;

        // Checkpoints' fields
        private List<Checkpoint> checkpoints = new List<Checkpoint>();
        public string currentCheckpoint;
        private Texture2D checkpoint;

        // Title papge
        private Texture2D titlePage;

        //Get set property for enemies
        public List<Enemy> Enemies
        {
            get { return enemies; }
            set { enemies = value; }
        }

        //Get set property for projectiles
        public List<Projectile> Projectiles
        {
            get { return projectiles; }
            set { projectiles = value; }
        }

        //Get property for enemyManager
        public EnemyManager EnemyManager
        {
            get { return enemyManager; }
        }

        //Get property for projectileManager
        public ProjectileManager ProjectileManager
        {
            get { return projectileManager; }
        }

        //Get property for player
        public Player Player
        {
            get { return player; }
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // Initializing variables
            keyboardControls = false;
            viewportWidth = this.GraphicsDevice.Viewport.Width;
            viewportHeight = this.GraphicsDevice.Viewport.Height;
            backdropPos = new Rectangle(0, 0, viewportWidth * 2, viewportHeight);
            backdropSwapPos = new Rectangle(viewportWidth * 2, 0, viewportWidth * 2, viewportHeight);
            timer = 0;
            projectiles = new List<Projectile>(1);
            enemies = new List<Enemy>(1);
            rng = new Random();
            enemyManager = new EnemyManager(enemies);
            projectileManager = new ProjectileManager(projectiles);
            currentState = GameState.Menu;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Loading in textures and initializing the player
            player = new Player(1, 20, 10, new Rectangle(0, 0, 32, 32), 6, 1, Content.Load<Texture2D>("PlaceholderPlayerProjectile"));
            player.Sprite = Content.Load<Texture2D>("PlayerFishSprite");
            backdrop = Content.Load<Texture2D>("PlaceholderBackdrop");
            backdropSwap = Content.Load<Texture2D>("PlaceholderBackdropSwap");
            baseEnemySprite = Content.Load<Texture2D>("EnemyPlaceholder");
            baseEnemyProjectileSprite = Content.Load<Texture2D>("PlaceholderPlayerProjectile");
            font = Content.Load<SpriteFont>("text");
            startButton = Content.Load<Texture2D>("OnStart");
            controlsButton = Content.Load<Texture2D>("OnControls");
            kbButton = Content.Load<Texture2D>("kbButton");
            mouseButton = Content.Load<Texture2D>("mouseButton");
            titlePage = Content.Load<Texture2D>("title");
            backButton = Content.Load<Texture2D>("backButton");
            menuButton = Content.Load<Texture2D>("MenuButton");
            resumeButton = Content.Load<Texture2D>("ResumeButton");
            continueButton = Content.Load<Texture2D>("ContinueButton");
            checkpoint = Content.Load<Texture2D>("Checkpoint");

            // Load buttons

            // Menu
            buttons.Add(new Button(
                _graphics.GraphicsDevice,
                new Rectangle(_graphics.GraphicsDevice.Viewport.Width / 2 - 93, _graphics.GraphicsDevice.Viewport.Height / 2 - 49, 187, 65),
                startButton,
                startButton
                ));

            buttons.Add(new Button(
                _graphics.GraphicsDevice,
                new Rectangle(_graphics.GraphicsDevice.Viewport.Width / 2 - 93, _graphics.GraphicsDevice.Viewport.Height / 2 + 39, 187, 65),
                controlsButton,
                controlsButton
                ));

            // Controls
            buttons.Add(new Button(
                _graphics.GraphicsDevice,
                new Rectangle(_graphics.GraphicsDevice.Viewport.Width / 2 - 250, _graphics.GraphicsDevice.Viewport.Height / 2 - 100, 187, 150),
                kbButton,
                kbButton
                ));

            buttons.Add(new Button(
                _graphics.GraphicsDevice,
                new Rectangle(_graphics.GraphicsDevice.Viewport.Width / 2 + 93, _graphics.GraphicsDevice.Viewport.Height / 2 - 100, 187, 150),
                mouseButton,
                mouseButton
                ));

            buttons.Add(new Button(
                _graphics.GraphicsDevice,
                new Rectangle(_graphics.GraphicsDevice.Viewport.Width / 2 - 30, _graphics.GraphicsDevice.Viewport.Height / 2 + 100, 100, 100),
                backButton,
                backButton
                ));

            // Pause
            buttons.Add(new Button(
                _graphics.GraphicsDevice,
                new Rectangle(_graphics.GraphicsDevice.Viewport.Width / 2, 100, 100, 50),
                resumeButton,
                resumeButton
                ));

            buttons.Add(new Button(
                _graphics.GraphicsDevice,
                new Rectangle(_graphics.GraphicsDevice.Viewport.Width / 2, 200, 100, 50),
                menuButton,
                menuButton
                ));

            buttons.Add(new Button(
                _graphics.GraphicsDevice,
                new Rectangle(_graphics.GraphicsDevice.Viewport.Width / 2, 300, 187, 65),
                controlsButton,
                controlsButton
                ));

            // Gameover
            buttons.Add(new Button(
                _graphics.GraphicsDevice,
                new Rectangle(_graphics.GraphicsDevice.Viewport.Width / 2, 300, 187, 65),
                continueButton,
                continueButton
                ));

            buttons[0].OnLeftButtonClick += this.StartButton;
            buttons[1].OnLeftButtonClick += this.SettingsButton;
            buttons[2].OnLeftButtonClick += this.KeyboardControlButton;
            buttons[3].OnLeftButtonClick += this.MouseControlButton;
            buttons[4].OnLeftButtonClick += this.BackButton;
            buttons[5].OnLeftButtonClick += this.ResumeButton;
            buttons[6].OnLeftButtonClick += this.MenuButton;
            buttons[7].OnLeftButtonClick += this.SettingsButton;
            buttons[8].OnLeftButtonClick += this.ContinueButton;

            // Checkpoints

            checkpoints.Add(new Checkpoint("checkpoint1", checkpoint, new Rectangle(100, 100, 100, 100)));
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kbState = Keyboard.GetState();

            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            //Update based on the GameState
            switch (currentState)
            {
                case GameState.Menu:
                    // Reset the game
                    enemies.Clear();
                    projectiles.Clear();
                    player.Position = new Rectangle(0, 0, 32, 32);
                    player.Health = 6;
                    player.IsAlive = true;

                    previousState = currentState;

                    buttons[0].Update();
                    buttons[1].Update();

                    IsMouseVisible = true;


                    break;

                case GameState.Settings:
                    IsMouseVisible = true;

                    buttons[2].Update();
                    buttons[3].Update();
                    buttons[4].Update();
                    break;

                case GameState.Game:

                    IsMouseVisible = false;

                    if (SingleKeyPress(Keys.Escape, kbState))
                    {
                        currentState = GameState.Pause;
                    }

                    if (player.IsAlive)
                    {
                        //Updates all the projectiles and enemies
                        projectileManager.ManageProjectiles(this, gameTime);
                        enemyManager.ManageEnemies(this, gameTime);

                        //Updates the player object
                        player.Update(gameTime, this);

                        //Processes player input through the player object
                        if (keyboardControls)
                        {
                            player.ProcessInputKeyboard(Keyboard.GetState(), this);
                        }
                        else
                        {
                            player.ProcessInputMouse(Mouse.GetState(), this);
                        }

                        //MovesBackdrop
                        MoveBackdrop();

                        // Randomly creates enemies based on a timer at random positions
                        //
                        // Will need to be changed, only here for testing purposes
                        if (timer % 120 == rng.Next(0, 121))
                        {
                            enemies.Add(new BaseEnemy(baseEnemySprite, new Rectangle(viewportWidth, rng.Next(0, viewportHeight - 31), 32, 32), 2, 120, baseEnemyProjectileSprite));
                            enemies.Add(new AimingEnemy(baseEnemyProjectileSprite, new Rectangle(viewportWidth, rng.Next(0, viewportHeight - 31), 32, 32), 2, 120, baseEnemyProjectileSprite));
                        }

                        //Timers for time/update based actions
                        timer += 1;

                        if (player.Health <= 0)
                        {
                            player.IsAlive = false;
                            //Change state if the player dies 
                            currentState = GameState.GameOver;
                        }

                        // Update checkpoints
                        
                        
                         checkpoints[0].Update(this);
                        
                    }
                    break;

                case GameState.Pause:
                    IsMouseVisible = true;
                    previousState = currentState;

                    if (SingleKeyPress(Keys.Escape, kbState))
                    {
                        currentState = GameState.Game;
                    }

                    buttons[5].Update();
                    buttons[6].Update();
                    buttons[7].Update();
                    break;

                case GameState.GameOver:
                    IsMouseVisible = true;

                    buttons[8].Update();
                    break;
            }



            //Get the previous keyboard state
            previousKbState = kbState;



            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draws the all objects to the window
            _spriteBatch.Begin();

            switch (currentState)
            {
                case GameState.Menu:
                    _spriteBatch.Draw(titlePage, new Rectangle(0, 0, _graphics.GraphicsDevice.Viewport.Width, _graphics.GraphicsDevice.Viewport.Height), Color.White);
                    buttons[0].Draw(_spriteBatch);
                    buttons[1].Draw(_spriteBatch);
                    break;

                case GameState.Settings:
                    _spriteBatch.DrawString(font, "Settings", new Vector2(0, 0), Color.White);
                    buttons[2].Draw(_spriteBatch);
                    buttons[3].Draw(_spriteBatch);
                    buttons[4].Draw(_spriteBatch);
                    break;

                case GameState.Game:
                    _spriteBatch.Draw(backdrop, backdropPos, Color.White);
                    _spriteBatch.Draw(backdropSwap, backdropSwapPos, Color.White);

                    for (int i = 0; i < projectiles.Count; i++)
                    {
                        _spriteBatch.Draw(projectiles[i].Sprite, projectiles[i].Position, Color.White);
                    }
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (enemies[i] is BaseEnemy)
                        {
                            _spriteBatch.Draw(enemies[i].Sprite, enemies[i].Position, Color.White);
                        }
                        else if (enemies[i] is AimingEnemy)
                        {
                            _spriteBatch.Draw(enemies[i].Sprite, enemies[i].Position, Color.Red);
                        }
                    }
                    if (player != null)
                    {
                        _spriteBatch.Draw(player.Sprite, player.Position, Color.White);
                    }



                    checkpoints[0].Draw(_spriteBatch,this);

                    break;

                case GameState.Pause:
                    _spriteBatch.DrawString(font, "Pause", new Vector2(0, 0), Color.White);

                    buttons[5].Draw(_spriteBatch);
                    buttons[6].Draw(_spriteBatch);
                    buttons[7].Draw(_spriteBatch);
                    break;

                case GameState.GameOver:
                    _spriteBatch.DrawString(font, "GameOver", new Vector2(0, 0), Color.White);
                    buttons[8].Draw(_spriteBatch);
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        //Finds the current mouse position and stores the values in variables mouseX and mouseY
        protected void UpdateMouse()
        {
            MouseState currentMouse = Mouse.GetState();
            mouseX = currentMouse.X;
            mouseY = currentMouse.Y;
        }

        // Check for a single key press
        private bool SingleKeyPress(Keys key, KeyboardState kbState)
        {
            return previousKbState.IsKeyDown(key) && kbState.IsKeyUp(key);
        }

        // Buttons' methods, change state
        public void StartButton()
        {
            currentState = GameState.Game;
        }

        public void SettingsButton()
        {
            currentState = GameState.Settings;
        }

        public void MouseControlButton()
        {
            keyboardControls = false;
            buttons[3].SetButtonColor = Color.DeepSkyBlue;
            buttons[2].SetButtonColor = Color.White;
        }

        public void KeyboardControlButton()
        {
            keyboardControls = true;
            buttons[3].SetButtonColor = Color.White;
            buttons[2].SetButtonColor = Color.DeepSkyBlue;
        }

        public void BackButton()
        {
            currentState = previousState;
        }

        public void ResumeButton()
        {
            currentState = GameState.Game;
        }

        public void MenuButton()
        {
            currentState = GameState.Menu;
        }

        public void ContinueButton()
        {
            currentState = GameState.Menu;
        }

        //Moves the backdrop
        public void MoveBackdrop()
        {
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
        }
    }
}
