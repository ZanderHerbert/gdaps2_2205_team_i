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
        private bool increment;
        private List<Projectile> projectiles;

        private Hud hud;
        private int pastCheckpoints = 0;

        private Upgrades upgrade;

        //Test for FileIO
        private FileIO fileIO;
        List<Enemy> enemiesToAdd;
        private bool addedFormation1;
        private bool addedFormation2;
        private bool addedBoss;

        private List<Enemy> enemies;
        private Texture2D baseEnemySprite;
        private Texture2D bossEnemySprite;
        private Texture2D baseEnemyProjectileSprite;
        private Texture2D aimedEnemySprite;
        private Texture2D staticEnemySprite;
        private Texture2D homingEnemySprite;
        private Random rng;
        private GameState currentState;
        private KeyboardState previousKbState;
        private SpriteFont font;
        private GameState previousState;
        private EnemyManager enemyManager;
        private ProjectileManager projectileManager;

        private SpriteFont text;

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
        private List<Checkpoint> deactivedCheckpoints = new List<Checkpoint>();
        private Checkpoint activeCheckpoint;
        private Checkpoint currentCheckpoint;
        private Texture2D checkpoint;
        private bool spawnEnemy;

        // Game time
        private float time;

        // Title page
        private Texture2D titlePage;
        private Texture2D titleBubblesLoop;
        private Texture2D titleBubbles;
        private Rectangle titleBubblesPos;
        private Rectangle titleBubblesSwapPos;
        private Texture2D logo;
        private Texture2D teamName;
        private Vector2 logoVect;
        private Vector2 origin;
        private Vector2 scale;
        private int sizeChanges = 0;
        private float rotation;
        private float rotationAmount = .0005f;
        //Upgrade fields
        private Texture2D healthUpgrade;
        private Texture2D speedUpgrade;
        private Texture2D damageUpgrade;

        public bool Increment
        {
            get { return increment; }
            set { increment = value; }
        }

        public Checkpoint CurrentCheckpoint
        {
            get { return currentCheckpoint; }
            set { currentCheckpoint = value; }
        }

        //Get property for total game time in seconds
        public float Time
        {
            get { return time; }
            set { time = value; }
        }

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
            set { player = value; }
        }

        // Get set property for player health
        public int PlayerHealth
        {
            get { return player.Health; }
            set { player.Health = value; }
        }

        // Get property for player speed
        public int PlayerSpeed
        {
            get { return player.Speed; }
        }

        // Get property for player damage
        public int PlayerDamage
        {
            get { return player.ProjectileDamage; }
        }

        //Get property for health upgrade
        public int UpHealth
        {
            get { return upgrade.UpHealth; }
        }

        //Get property for speed upgrade
        public int UpSpeed
        {
            get { return upgrade.UpSpeed; }
        }

        //Get property for damage upgrade
        public int UpDamage 
        {
            get { return upgrade.UpDamage; }
        }

        // Get property for player position
        public Rectangle PlayerPosition
        {
            get { return player.Position; }
        }

        // Get set Property for stop enemies from spawning when player reached a checkpoint
        public bool SpawnEnemy
        {
            get { return spawnEnemy; }
            set { spawnEnemy = value; }
        }

        //Get property for viewport width
        public int ViewportWidth
        {
            get { return viewportWidth; }
        }

        //Get property for viewport width
        public int ViewportHeight
        {
            get { return viewportHeight; }
        }

        //Get property for font
        public SpriteFont Font
        {
            get { return font; }
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
            spawnEnemy = true;
            addedFormation1 = false;
            addedFormation2 = false;
            addedBoss = false;
            currentCheckpoint = null;
            hud = new Hud(this);
            increment = true;

            logoVect = new Vector2(400, viewportHeight - 390);
            origin = new Vector2(400, 130);
            scale = new Vector2(.78f,.78f);

            titleBubblesPos = new Rectangle(0, 0, viewportWidth, viewportHeight);
            titleBubblesSwapPos = new Rectangle(0, viewportHeight, viewportWidth, viewportHeight);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Loading in textures and initializing the player
            player = new Player(1, 20, 10, new Rectangle(0, 0, 48, 48), 7, 1, Content.Load<Texture2D>("bubble"));
            player.Sprite = Content.Load<Texture2D>("PlayerFishSprite");

            // Load background 
            backdrop = Content.Load<Texture2D>("PlaceholderBackdrop");
            backdropSwap = Content.Load<Texture2D>("PlaceholderBackdropSwap");

            // Load enemies 
            bossEnemySprite = Content.Load<Texture2D>("BossSprite1");
            baseEnemySprite = Content.Load<Texture2D>("EnemyFishSprite2");
            baseEnemyProjectileSprite = Content.Load<Texture2D>("bubble");
            aimedEnemySprite = Content.Load<Texture2D>("EnemyFishSprite1");
            staticEnemySprite = Content.Load<Texture2D>("EnemyFishSprite3");
            homingEnemySprite = Content.Load<Texture2D>("EnemyFishSprite4");
            font = Content.Load<SpriteFont>("text");

            // Load Buttons
            startButton = Content.Load<Texture2D>("OnStart");
            controlsButton = Content.Load<Texture2D>("OnControls");
            kbButton = Content.Load<Texture2D>("KeyboardControls");
            mouseButton = Content.Load<Texture2D>("MouseControls");
            titlePage = Content.Load<Texture2D>("titlebg");
            titleBubbles = Content.Load<Texture2D>("titleBubbles");
            titleBubblesLoop = Content.Load<Texture2D>("titleBubbles");
            logo = Content.Load<Texture2D>("titleLogo");
            teamName = Content.Load<Texture2D>("titleteamname");
            backButton = Content.Load<Texture2D>("backButton");
            menuButton = Content.Load<Texture2D>("MenuButton");
            resumeButton = Content.Load<Texture2D>("ResumeButton");
            continueButton = Content.Load<Texture2D>("ContinueButton");

            // Load checkpoint
            checkpoint = Content.Load<Texture2D>("CheckpointFlag");
            
            //Initializes the fileIO class with all the data and assets that it needs
            fileIO = new FileIO(rng, viewportHeight, viewportWidth, baseEnemySprite, baseEnemyProjectileSprite, aimedEnemySprite, staticEnemySprite, homingEnemySprite);
            //Loads in the file
            fileIO.LoadFormation("../../../EnemyFormations.txt");
            
            // Add buttons
            // Menu
            buttons.Add(new Button(
                _graphics.GraphicsDevice,
                new Rectangle(_graphics.GraphicsDevice.Viewport.Width / 2 - 93, _graphics.GraphicsDevice.Viewport.Height / 2 - 49, 187, 65),
                startButton,
                startButton
                ));

            // Settings
            buttons.Add(new Button(
                _graphics.GraphicsDevice,
                new Rectangle(_graphics.GraphicsDevice.Viewport.Width / 2 - 93, _graphics.GraphicsDevice.Viewport.Height / 2 + 39, 187, 65),
                controlsButton,
                controlsButton
                ));

            // Settings Keyboard
            buttons.Add(new Button(
                _graphics.GraphicsDevice,
                new Rectangle(_graphics.GraphicsDevice.Viewport.Width / 2 - 250, _graphics.GraphicsDevice.Viewport.Height / 2 - 100, 187, 187),
                kbButton,
                kbButton
                ));

            // Settings Mouse
            buttons.Add(new Button(
                _graphics.GraphicsDevice,
                new Rectangle(_graphics.GraphicsDevice.Viewport.Width / 2 + 93, _graphics.GraphicsDevice.Viewport.Height / 2 - 100, 187, 187),
                mouseButton,
                mouseButton
                ));

            // Back to menu
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

            // Assign methods to the buttons' event 
            buttons[0].OnLeftButtonClick += this.StartButton;
            buttons[1].OnLeftButtonClick += this.SettingsButton;
            buttons[2].OnLeftButtonClick += this.KeyboardControlButton;
            buttons[3].OnLeftButtonClick += this.MouseControlButton;
            buttons[4].OnLeftButtonClick += this.BackButton;
            buttons[5].OnLeftButtonClick += this.ResumeButton;
            buttons[6].OnLeftButtonClick += this.MenuButton;
            buttons[7].OnLeftButtonClick += this.SettingsButton;
            buttons[8].OnLeftButtonClick += this.ContinueButton;

            // Add Checkpoints
            deactivedCheckpoints.Add(new Checkpoint("checkpoint1", checkpoint, new Rectangle(viewportWidth, viewportHeight / 2 - 50, 100, 100), 60));
            deactivedCheckpoints.Add(new Checkpoint("checkpoint2", checkpoint, new Rectangle(viewportWidth, viewportHeight / 2 - 50, 100, 100), 120));
            deactivedCheckpoints.Add(new Checkpoint("checkpoint3", checkpoint, new Rectangle(viewportWidth, viewportHeight / 2 - 50, 100, 100), 180));

            //Adds the Upgrades

            healthUpgrade = Content.Load<Texture2D>("heart upgrade");
            speedUpgrade = Content.Load<Texture2D>("speedometer");
            damageUpgrade = Content.Load<Texture2D>("bubble");
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
                    GameReset();

                    previousState = currentState;
                    
                    //Update the size and rotation of the logo on the main menu
                    if (sizeChanges > 0)
                    {    
                        scale.X -= .0005f;
                        scale.Y -= .0005f;
                                            
                        sizeChanges--;
                    }
                    
                    if (sizeChanges == 200 || sizeChanges == -200 )
                    {
                        rotationAmount = rotationAmount * -1;
                        sizeChanges--;
                    }

                    else if (sizeChanges <= 0 && sizeChanges > -400)
                    {
                        scale.X += .0005f;
                        scale.Y += .0005f;
                        sizeChanges--;                        
                    }
                    if (sizeChanges == -400)
                    {
                        
                        sizeChanges = 400;                
                    }

                    rotation += rotationAmount;

                    //Moves the backdrop
                    MoveBackdrop(1, -1);

                    // Buttons Update
                    buttons[0].Update();
                    buttons[1].Update();

                    IsMouseVisible = true;
                    break;

                case GameState.Settings:
                    IsMouseVisible = true;

                    // Buttons Update
                    buttons[2].Update();
                    buttons[3].Update();
                    buttons[4].Update();
                    break;

                case GameState.Game:
                    // Update actual game time
                    time += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    IsMouseVisible = false;

                    // Press ESC to pause the game
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
                        MoveBackdrop(2, -3);

                        // Randomly creates enemies based on a timer at random positions
                        //
                        // Will need to be changed, only here for testing purposes
                        if (spawnEnemy)// If a checkpoint appears, then stop ememies from spawning 
                        {
                            
                            // un-comment if you want to fight the boss :D, also, remember to comment out the other
                            // enemy spawns if you want to try it, or else you'll have a bad time lol
                            /*
                            if(timer == 0)
                            {
                                enemies.Add(new Boss(bossEnemySprite, new Rectangle(viewportWidth - 128, viewportHeight / 2 - 64, 256, 128), 0, baseEnemyProjectileSprite, -10, -20, 6, 40, rng, 3, 1));
                            }
                            
                            /*
                            if (timer % 360 == rng.Next(0, 361))
                            {
                                enemies.Add(new BaseEnemy(baseEnemySprite, new Rectangle(viewportWidth, rng.Next(0, viewportHeight - 63), 64, 64), 2, 120, baseEnemyProjectileSprite));
                            }
                            if (timer % 360 == rng.Next(0, 361))
                            {
                                enemies.Add(new AimingEnemy(aimedEnemySprite, new Rectangle(viewportWidth, rng.Next(0, viewportHeight - 63), 64, 64), 2, 120, baseEnemyProjectileSprite));
                            }
                            if (timer % 360 == rng.Next(0, 361))
                            {
                                enemies.Add(new StaticEnemy(staticEnemySprite, new Rectangle(viewportWidth, rng.Next(0, viewportHeight - 63), 64, 64), 4));
                            }
                            if (timer % 360 == rng.Next(0, 361))
                            {
                                enemies.Add(new RangedHomingEnemy(homingEnemySprite, new Rectangle(viewportWidth, rng.Next(0, viewportHeight - 63), 64, 64), 2, 240, baseEnemyProjectileSprite));
                            }
                            */


                            //FileIO
                            
                            if (deactivedCheckpoints[2].Contact == true)
                            {
                                if (!addedBoss)
                                {
                                    enemies.Add(new Boss(bossEnemySprite, new Rectangle(viewportWidth - 128, viewportHeight / 2 - 64, 256, 128), 0, baseEnemyProjectileSprite, -10, -20, 6, 50, rng, 3, 1));
                                    addedBoss = true;
                                }
                            }
                            /*
                            else if (deactivedCheckpoints[1].Contact == true)
                            {
                                if (!addedFormation2)
                                {
                                    enemiesToAdd = fileIO.AddFormation(1, 10);
                                    addedFormation2 = true;
                                }
                                for (int i = 0; i < enemiesToAdd.Count; i++)
                                {
                                    enemies.Add(enemiesToAdd[i]);
                                    enemiesToAdd.RemoveAt(i);
                                }
                            }
                            else if (deactivedCheckpoints[0].Contact == true)
                            {
                                if (!addedFormation1)
                                {
                                    enemiesToAdd = fileIO.AddFormation(0, 10);
                                    addedFormation1 = true;
                                }
                                for (int i = 0; i < enemiesToAdd.Count; i++)
                                {
                                    enemies.Add(enemiesToAdd[i]);
                                    enemiesToAdd.RemoveAt(i);
                                }
                            }
                            */
                            else
                            {
                            if (timer % 240 == rng.Next(0, 240))
                                {
                                    enemies.Add(new BaseEnemy(baseEnemySprite, new Rectangle(viewportWidth, rng.Next(0, viewportHeight - 63), 64, 64), 2, 120, baseEnemyProjectileSprite));
                                }
                                if (timer % 240 == rng.Next(0, 240))
                                {
                                    enemies.Add(new AimingEnemy(aimedEnemySprite, new Rectangle(viewportWidth, rng.Next(0, viewportHeight - 63), 64, 64), 2, 120, baseEnemyProjectileSprite));
                                }
                                if (timer % 240 == rng.Next(0, 240))
                                {
                                    enemies.Add(new StaticEnemy(staticEnemySprite, new Rectangle(viewportWidth, rng.Next(0, viewportHeight - 63), 64, 64), 4));
                                }
                                if (timer % 240 == rng.Next(0, 240))
                                {
                                    enemies.Add(new RangedHomingEnemy(homingEnemySprite, new Rectangle(viewportWidth, rng.Next(0, viewportHeight - 63), 64, 64), 2, 240, baseEnemyProjectileSprite));
                                }
                            }
                        }

                        if(enemies.Count == 0 && addedBoss && player.Health > 0)
                        {
                            currentState = GameState.Menu;
                        }

                        //Timers for time/update based actions
                        if (increment)
                        {
                            timer += 1;
                        }

                        if (player.Health <= 0)
                        {
                            player.IsAlive = false;
                            //Change state if the player dies 
                            currentState = GameState.GameOver;
                        }

                        // Find the next uncontacted checkpoint
                        CheckpointManager();

                        // Update the activated checkpoint
                        if(activeCheckpoint != null)
                        {
                            activeCheckpoint.Update(this);
                        }
                    }
                    break;

                case GameState.Pause:
                    IsMouseVisible = true;
                    previousState = currentState;

                    // Press ESC to resume 
                    if (SingleKeyPress(Keys.Escape, kbState))
                    {
                        currentState = GameState.Game;
                    }

                    // Buttons Update
                    buttons[5].Update();
                    buttons[6].Update();
                    buttons[7].Update();
                    break;

                case GameState.GameOver:
                    IsMouseVisible = true;

                    // Buttons Update
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
                    // Draw title page
                    _spriteBatch.Draw(titlePage, new Rectangle(0, 0, _graphics.GraphicsDevice.Viewport.Width, _graphics.GraphicsDevice.Viewport.Height), Color.White);
                    _spriteBatch.Draw(titleBubbles, titleBubblesPos, Color.White);
                    _spriteBatch.Draw(titleBubblesLoop, titleBubblesSwapPos, Color.White);
                    _spriteBatch.Draw(logo, logoVect, null, Color.White, rotation, origin, scale, SpriteEffects.None, 0f);
                    _spriteBatch.Draw(teamName, new Rectangle(0,0, _graphics.GraphicsDevice.Viewport.Width, _graphics.GraphicsDevice.Viewport.Height), Color.White);

                    // Draw buttons
                    buttons[0].Draw(_spriteBatch); // Start
                    buttons[1].Draw(_spriteBatch); // Settings 
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

                    // Draw a timer 
                    //_spriteBatch.DrawString(font, string.Format("{0:f0}", time), new Vector2(10, 10), Color.White);
                    //_spriteBatch.DrawString(font, string.Format(currentCheckpoint.GetName), new Vector2(50, 10), Color.White);

                    // Draw the HUD
                    hud.Draw(_spriteBatch, player, viewportHeight, pastCheckpoints, (((timer - 1) % 3601.0) / 3601.0));

                    // Draw projectiles
                    for (int i = 0; i < projectiles.Count; i++)
                    {
                        if (projectiles[i] is PlayerProjectile)
                        {
                            _spriteBatch.Draw(projectiles[i].Sprite, projectiles[i].Position, Color.White);
                        }
                        else
                        {
                            _spriteBatch.Draw(projectiles[i].Sprite, projectiles[i].Position, Color.Red);
                        }
                    }

                    // Draw enemies
                    enemyManager.Draw(_spriteBatch);

                    // Draw player
                    if (player != null)
                    {
                        if(player.IFrameTimer % 10 > 4 || player.IFrameTimer == 0)
                        {
                            _spriteBatch.Draw(player.Sprite, player.Position, Color.White);
                        }
                    }

                    // Find the next uncontacted checkpoint
                    CheckpointManager();

                    // Draw checkpoints
                    if(activeCheckpoint != null)
                    {
                        activeCheckpoint.Draw(_spriteBatch, this);
                    }

                    // Print "game saved" message
                    foreach(Checkpoint c in deactivedCheckpoints)
                    {
                        c.PrintMessage(_spriteBatch,this);
                    }
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

        // Buttons' methods for state changing 
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
        public void MoveBackdrop(int speed, int multiplier)
        {
            //Sorry Zander, I stole your method and expanded it for the main menu :) - Quinn

            //Moves the backdrop, and if the backdrop goes past a certain x or y value, gets placed at the right edge of
            //the screen.
            //There are two backdrops because if you were to suddenly reposition the first once it reaches the end
            //it is really jarring, also I mirrored the image for the second backdrop so it looks a bit better

            switch (currentState)
            {
                case (GameState.Game):

                    backdropPos.X -= speed;
                    backdropSwapPos.X -= speed;

                    if (backdropPos.X == multiplier * viewportWidth)
                    {
                        backdropPos.X = viewportWidth;
                    }
                    if (backdropSwapPos.X == multiplier * viewportWidth)
                    {
                        backdropSwapPos.X = viewportWidth;
                    }

                    break;

                case (GameState.Menu):

                    titleBubblesPos.Y -= speed;
                    titleBubblesSwapPos.Y -= speed;

                    if (titleBubblesPos.Y == multiplier * viewportHeight)
                    {
                        titleBubblesPos.Y = viewportHeight;
                    }
                    if (titleBubblesSwapPos.Y == multiplier * viewportHeight)
                    {
                        titleBubblesSwapPos.Y = viewportHeight;
                    }

                    break;
                          
            } 
        }


        /// <summary>
        /// Reset the game in menu state
        /// </summary>
        private void GameReset()
        {
            // Reset the game
            enemies.Clear();
            projectiles.Clear();
            player.Position = new Rectangle(0, 0, 48, 48);
            player.Health = player.MaxHP;
            player.IsAlive = true;
            time = 0;
            timer = 0;
            addedBoss = false;
            currentCheckpoint = deactivedCheckpoints[0];
            pastCheckpoints = 0;

            // reset checkpoints
            foreach (Checkpoint c in deactivedCheckpoints)
            {
                c.Contact = false;
                c.Position = new Rectangle(viewportWidth, viewportHeight / 2 - 50, 100, 100);
            }
        }

        // Find the first unctontacted checkpoint 
        private void CheckpointManager()
        {
            for (int i = 0; i < deactivedCheckpoints.Count; i++)
            {
                if (!deactivedCheckpoints[i].Contact)
                {
                    activeCheckpoint = deactivedCheckpoints[i];
                    break;
                }
                else
                {
                    pastCheckpoints = i + 1;
                }
            }
            if(pastCheckpoints == deactivedCheckpoints.Count)
            {
                activeCheckpoint = null;
            }
        }
    }
}
