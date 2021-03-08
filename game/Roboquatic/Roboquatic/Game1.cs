using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roboquatic
{
    public class Game1 : Game
    {
        enum GameState
        {
            Menu,
            Game,
            Pause,
            GameOver,
            Settings
        }

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
        private int shootingTimer;
        private List<Projectile> projectiles;
        private List<Enemy> enemies;
        private Texture2D baseEnemySprite;
        private Texture2D baseEnemyProjectileSprite;
        private Random rng;
        private Enemy hitEnemy;
        private GameState currentState;
        private KeyboardState previousKbState;
        private SpriteFont text;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // Initializing variables
            player = new Player(1, 20, 10, new Rectangle(0, 0, 32, 32), 6, 1);
            keyboardControls = false;
            viewportWidth = this.GraphicsDevice.Viewport.Width;
            viewportHeight = this.GraphicsDevice.Viewport.Height;
            backdropPos = new Rectangle(0, 0, viewportWidth * 2, viewportHeight);
            backdropSwapPos = new Rectangle(viewportWidth * 2, 0, viewportWidth * 2, viewportHeight);
            timer = 0;
            shootingTimer = player.FramesToFire;
            projectiles = new List<Projectile>(1);
            enemies = new List<Enemy>(1);
            rng = new Random();
            currentState = GameState.Menu;//It should be in the Menu state, but this is just for checking if game state works

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Loading in (Placeholder) textures
            player.Sprite = Content.Load<Texture2D>("PlayerFishSprite");
            backdrop = Content.Load<Texture2D>("PlaceholderBackdrop");
            backdropSwap = Content.Load<Texture2D>("PlaceholderBackdropSwap");
            baseEnemySprite = Content.Load<Texture2D>("EnemyPlaceholder");
            baseEnemyProjectileSprite = Content.Load<Texture2D>("PlaceholderPlayerProjectile");
            text = Content.Load<SpriteFont>("text");
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kbState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Update based on the GameState
            switch (currentState)
            {
                case GameState.Menu:
                    if (SingleKeyPress(Keys.S, kbState))
                    {
                        currentState = GameState.Settings;
                    }
                    if (SingleKeyPress(Keys.Enter, kbState))
                    {
                        currentState = GameState.Game;
                    }
                    break;

                case GameState.Settings:
                    if (SingleKeyPress(Keys.M, kbState))
                    {
                        currentState = GameState.Menu;
                    }
                    break;

                case GameState.Game:
                    if (SingleKeyPress(Keys.Escape, kbState))
                    {
                        currentState = GameState.Pause;
                    }

                    if (player != null)
                    {
                        for (int i = 0; i < projectiles.Count; i++)
                        {
                            if (projectiles[i] is EnemyProjectile)
                            {
                                if (projectiles[i].PlayerContact(player))
                                {
                                    player.TakeDamage(projectiles[i].Damage);
                                    projectiles.RemoveAt(i);
                                    i--;
                                }
                            }
                            if (projectiles[i] is PlayerProjectile)
                            {
                                hitEnemy = projectiles[i].EnemyContact(enemies);
                                if (hitEnemy != null)
                                {
                                    hitEnemy.TakeDamage(projectiles[i].Damage);
                                    if (hitEnemy.Health <= 0)
                                    {
                                        enemies.Remove(hitEnemy);
                                    }
                                    projectiles.Remove(projectiles[i]);
                                    i--;
                                }
                            }
                        }
                        //Moves all projectiles on screen
                        for (int i = 0; i < projectiles.Count; i++)
                        {
                            projectiles[i].Move();
                        }

                        for (int i = 0; i < enemies.Count; i++)
                        {
                            enemies[i].Move(this);
                            enemies[i].ShootingTimer += 1;
                            if (enemies[i].CanShoot())
                            {
                                enemies[i].ShootingTimer = 0;
                                projectiles.Add(enemies[i].Shoot());
                            }
                        }

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
                            // Checks if the player pressed space and if the player can shoot and then adds a new player projectile 
                            // to the list of projectiles and resets the shooting timer
                            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                            {
                                if (shootingTimer >= player.FramesToFire)
                                {
                                    projectiles.Add(new PlayerProjectile(Content.Load<Texture2D>("PlaceholderPlayerProjectile"), player.ProjectileSpeed, new Rectangle(player.Position.X + player.Position.Width, player.Position.Y, 32, 32), player));
                                    shootingTimer = 0;
                                }
                            }
                        }
                        else
                        {
                            //Gets the position of the mouse, and causes the player object to move towards that position
                            UpdateMouse();
                            player.Move(mouseX, mouseY);
                            // Checks if the player pressed left mouse button and can shoot and then adds a new player projectile  
                            // to the list of projectiles and resets the shooting timer
                            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                            {
                                if (shootingTimer >= player.FramesToFire)
                                {
                                    projectiles.Add(new PlayerProjectile(Content.Load<Texture2D>("PlaceholderPlayerProjectile"), player.ProjectileSpeed, new Rectangle(player.Position.X + player.Position.Width, player.Position.Y, 32, 32), player));
                                    shootingTimer = 0;
                                }
                            }
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


                        if (timer % 120 == rng.Next(0, 121))
                        {
                            enemies.Add(new BaseEnemy(baseEnemySprite, new Rectangle(viewportWidth, rng.Next(0, viewportHeight - 31), 32, 32), 2, 120, baseEnemyProjectileSprite));
                            enemies.Add(new BaseEnemy(baseEnemySprite, new Rectangle(viewportWidth, rng.Next(0, viewportHeight - 31), 32, 32), 2, 120, baseEnemyProjectileSprite));
                        }


                        //Timers for time/update based actions
                        timer += 1;
                        shootingTimer += 1;
                        if (player.IFrameTimer != 0)
                        {
                            player.IFrameTimer -= 1;
                        }

                        if (player.Health <= 0)
                        {
                            player = null;
                            //Change state if the player dies 
                            currentState = GameState.GameOver;
                        }
                    }
                    break;

                case GameState.Pause:
                    if (SingleKeyPress(Keys.Enter, kbState))
                    {
                        currentState = GameState.Game;
                    }
                    break;

                case GameState.GameOver:
                    if (SingleKeyPress(Keys.Enter, kbState))
                    {
                        currentState = GameState.Menu;
                    }
                    break;
            }

            

            //Get the previous keyboard state
            previousKbState = kbState;

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

            // Draws the all objects to the window
            _spriteBatch.Begin();

            switch (currentState)
            {
                case GameState.Menu:
                    _spriteBatch.DrawString(text, "Menu", new Vector2(0, 0), Color.White);
                    break;

                case GameState.Settings:
                    _spriteBatch.DrawString(text, "Settings", new Vector2(0, 0), Color.White);
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
                        _spriteBatch.Draw(enemies[i].Sprite, enemies[i].Position, Color.White);
                    }
                    if (player != null)
                    {
                        _spriteBatch.Draw(player.Sprite, player.Position, Color.White);
                    }
                    break;

                case GameState.Pause:
                    _spriteBatch.DrawString(text, "Pause", new Vector2(0, 0), Color.White);
                    break;

                case GameState.GameOver:
                    _spriteBatch.DrawString(text, "GameOver", new Vector2(0, 0), Color.White);
                    break;
            }
            

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        // Check for a single key press
        private bool SingleKeyPress(Keys key, KeyboardState kbState)
        {
            return previousKbState.IsKeyDown(key) && kbState.IsKeyUp(key);
        }
    }
}
