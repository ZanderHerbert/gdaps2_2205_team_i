using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    //The player class is the class that is used to create and control the player object
    public class Player
    {
        //Fields
        private float speed;
        private Rectangle position;
        private Texture2D sprite;
        private int projectileDamage;
        private int framesToFire;
        private int projectileSpeed;
        private int maxHP;
        private int health;
        private int iFrameTimer;
        private int shootingTimer;
        private Texture2D projectileSprite;
        private bool isAlive;
        private Rectangle hitBox;
        private Vector2 posF;

        //Properties

        //Get set property for isAlive
        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        //Get set property for iFrameTimer
        public int IFrameTimer
        {
            get { return iFrameTimer; }
            set { iFrameTimer = value; }
        }

        //Get set property for health
        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        // Get/Set property for the player's max health points
        public int MaxHP
        {
            get { return maxHP; }
            set { maxHP = value; }
        }

        //Get set property for projectileDamage
        public int ProjectileDamage
        {
            get { return projectileDamage; }
            set { projectileDamage = value; }
        }

        // Get and set property for projectileSpeed
        public int ProjectileSpeed
        {
            get { return projectileSpeed; }
            set { projectileSpeed = value; }
        }

        // Get property for framesToFire
        public int FramesToFire
        {
            get { return framesToFire; }
        }

        //Get and set properties for speed (as speed may change due to upgrades)
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        //Get and set for the sprite (Unsure if I should store the sprite within the class or not)
        public Texture2D Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }

        //Get property for the Rectangle position
        public Rectangle Position
        {
            get { return position; }
            set { position = value; }
        }

        //Get set property for projectileSprite
        public Texture2D ProjectileSprite
        {
            get { return projectileSprite; }
            set { projectileSprite = value; }
        }

        //Get property for hitBox
        public Rectangle HitBox
        {
            get { return hitBox; }
        }

        //Player Constructor
        public Player(int speed, int framesToFire, int projectileSpeed, Rectangle position, int health, int projectileDamage, Texture2D projectileSprite, Rectangle hitBox)
        {
            this.speed = (int)(speed * (GlobalScalars.x + GlobalScalars.y) / 2);
            this.framesToFire = framesToFire;
            this.projectileSpeed = projectileSpeed;
            this.position = GlobalScalars.scaleRect(position);
            maxHP = health;
            this.health = maxHP;
            this.projectileDamage = projectileDamage;
            this.projectileSprite = projectileSprite;
            iFrameTimer = 0;
            shootingTimer = framesToFire;
            this.hitBox = GlobalScalars.scaleRect(hitBox);
            posF = new Vector2(position.X, position.Y);
        }

        //Methods

        //Processes player input under keyboard controls
        public void ProcessInputKeyboard(KeyboardState kbState, Game1 game)
        {
            //Moves the player based on WASD input
            if (kbState.IsKeyDown(Keys.S))
            {
                posF.Y = posF.Y + speed * 4;
            }
            if (kbState.IsKeyDown(Keys.W))
            {
                posF.Y = posF.Y - speed * 4;
            }
            if (kbState.IsKeyDown(Keys.A))
            {
                posF.X = posF.X - speed * 4;
            }
            if (kbState.IsKeyDown(Keys.D))
            {
                posF.X = posF.X + speed * 4;
            }
                        position.X = (int)posF.X;
            position.Y = (int)posF.Y;

            // Checks if the player pressed space and if the player can shoot and then adds a new player projectile 
            // to the list of projectiles and resets the shooting timer
            if (kbState.IsKeyDown(Keys.Space))
            {
                if (shootingTimer >= FramesToFire)
                {
                    game.Projectiles.Add(new PlayerProjectile(projectileSprite, ProjectileSpeed, new Rectangle(Position.X + position.Width, Position.Y + position.Height / 2 - 16, 32, 32), this));
                    shootingTimer = 0;
                }
            }
            
        }

        //Processes player input under mouse controls
        public void ProcessInputMouse(MouseState mouseState, Game1 game)
        {
            //Declares and initializes two variables to hold the mouses x and y position
            int x = mouseState.X;
            int y = mouseState.Y;

            //Variables which hold the difference in position between the midpoint of the player and the mouse
            double deltaX = (position.X + position.Width/2) - x;
            double deltaY = (position.Y + position.Height/2) - y;

            //Moves the player a set distance in the direction of the mouse.
            //
            //The first if is if the player is within a "speed" radius of the mouse, in which case it will go directly
            //to the mouse, otherwise the player would flicker back and forth on either side of the mouse.
            //
            //The second if is so that there are no divide by zero errors, and the code in there makes the player
            //object change it's position to basically the closest position to the mouse within a radius of "speed"
            //pixels(I believe it measures it in pixels)
            //
            //NOTE: The speed needs to be balanced between keyboard and mouse controls still if we plan on
            //      implementing both.
            if(Math.Sqrt((deltaX * deltaX + deltaY * deltaY)) <= speed * 10)
            {
                posF.X = x - position.Width/2;
                posF.Y = y - position.Height/2;
            }
            else if ((deltaX) + (deltaY) != 0)
            {
                posF.X -= (float)(((deltaX) * speed * 10) / ((Math.Abs(deltaX)) + (Math.Abs(deltaY))));
                posF.Y -= (float)(((deltaY) * speed * 10) / ((Math.Abs(deltaX)) + (Math.Abs(deltaY))));
            }
            position.X = (int)posF.X;
            position.Y = (int)posF.Y;

            //Shoots a projectile if the player pressed left mouse button, and if they are able to shoot
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (shootingTimer >= FramesToFire)
                {
                    game.Projectiles.Add(new PlayerProjectile(projectileSprite, ProjectileSpeed, new Rectangle(Position.X + position.Width, Position.Y + position.Height / 2 - (int)(GlobalScalars.y * 16), 32, 32), this));
                    shootingTimer = 0;
                }
            }
        }

        //Reduces the health of the player based on damage, as long as the player isn't invincible,
        //and if the player is damaged, makes the invincible for 1 second
        public void TakeDamage(int damage)
        {
            if(iFrameTimer == 0)
            {
                health -= damage;
                iFrameTimer = 60;
            }
        }

        //Updates the player
        //
        //Increments both timers related to the player
        public void Update(GameTime gametime, Game1 game)
        {
            int viewWidth = game.GraphicsDevice.Viewport.Width;
            int viewHeight = game.GraphicsDevice.Viewport.Height;
            shootingTimer++;
            if (IFrameTimer != 0)
            {
                IFrameTimer--;
            }
            // 18 x 31
            hitBox.X = position.X + (int)(GlobalScalars.x);
            hitBox.Y = position.Y + (int)(9 * GlobalScalars.y);
            if (position.X + position.Width > viewWidth)
            {
                position.X = viewWidth - position.Width;
            }
            if (position.X < 0)
            {
                position.X = 0;
            }
            if (position.Y + position.Height > viewHeight)
            {
                position.Y = viewHeight - position.Height;
            }
            if (position.Y < 0)
            {
                position.Y = 0;
            }
        }
    }
}
