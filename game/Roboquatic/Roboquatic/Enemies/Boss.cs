using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Roboquatic
{
    //The Boss enemy which moves around and have multiple attack patterns based on rng
    class Boss : Enemy
    {
        //Fields
        private int shotPattern;
        private Texture2D projectileSprite;
        private int projectileSpeed;
        private int aimedProjectileSpeed;
        private int homingProjectileSpeed;
        private int timer;
        private Random rng;
        private bool canMoveUp = false;
        private int screenMiddleY;
        private int ySpeed;
        private LaserBeam laser;
        private Texture2D laserSprite;

        //Constructor
        public Boss(Texture2D sprite, Rectangle position, int speed, Texture2D projectileSprite, int projectileSpeed, int aimedProjectileSpeed, int homingProjectileSpeed, int health, Random rng, int ySpeed, int contactDamage, Texture2D laserSprite, Rectangle hitBox)
            : base(sprite, position, speed, hitBox)
        {
            this.projectileSprite = projectileSprite;
            this.projectileSpeed = projectileSpeed;
            this.aimedProjectileSpeed = aimedProjectileSpeed;
            this.homingProjectileSpeed = homingProjectileSpeed;
            this.health = health;
            this.rng = rng;
            this.ySpeed = ySpeed;
            this.contactDamage = contactDamage;
            this.laserSprite = laserSprite;
            timer = 0;
            shotPattern = 4;
            screenMiddleY = position.Y + position.Height / 2;
            laser = null;
        }

        //Methods

        //Updates the Boss
        public override void Update(GameTime gametime, Game1 game)
        {
            //Syncs the hitbox with the position(This is done in the beginning because one attack pattern alters the position)
            hitBox.X = position.X;
            hitBox.Y = position.Y;

            //Incrementing timer for logic
            timer++;

            //Chooses which shot pattern through randomness
            if (timer % 360 == 359)
            {
                shotPattern = rng.Next(0, 4);
            }

            //Moves the boss up and down
            if (timer % 252 < 126 && canMoveUp)
            {
                position.Y += ySpeed;
            }
            else if (timer % 252 >= 126)
            {
                position.Y -= ySpeed;
            }

            //Lets the boss move up after a little bit so that the Boss is centered around the middle of the screen
            if (timer % 250 == 63)
            {
                canMoveUp = true;
            }

            //Enters a method based on the shotpattern
            switch (shotPattern)
            {
                case 0:
                    ShotPatternOne(timer, game);
                    break;

                case 1:
                    ShotPatternTwo(timer, game);
                    break;

                case 2:
                    ShotPatternThree(timer, game);
                    break;

                case 3:
                    ShotPatternFour(timer, game);
                    break;

                default:
                    break;
            }
            //Hit timer which increments, and while the hit timer is below 5, the Boss will be invisible (seen in enemy draw method) to show damage
            if (hit)
            {
                hitTimer++;
                if (hitTimer == 5)
                {
                    hit = false;
                    hitTimer = 0;
                }
            }
        }

        //Shot pattern which shoots a V of bullets, followed by 3 sets of 2 aimed bullets, along with one homing bullet with the second set of aimed bullets
        public void ShotPatternOne(int timer, Game1 game)
        {
            if (timer % 360 == 0)
            {
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY - 16, 32, 32)));
            }
            if (timer % 360 == 20)
            {
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY + 16, 32, 32)));
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY - 48, 32, 32)));
            }
            if (timer % 360 == 40)
            {
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY + 48, 32, 32)));
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY - 80, 32, 32)));
            }
            if (timer % 360 == 60)
            {
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY + 80, 32, 32)));
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY - 112, 32, 32)));
            }
            if (timer % 360 == 80)
            {
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY + 112, 32, 32)));
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY - 144, 32, 32)));
            }
            if (timer % 360 == 100)
            {
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY + 144, 32, 32)));
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY - 176, 32, 32)));
            }
            if (timer % 360 == 120)
            {
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY + 176, 32, 32)));
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY - 208, 32, 32)));
            }
            if (timer % 360 == 140)
            {
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY + 208, 32, 32)));
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY - 240, 32, 32)));
                game.Projectiles.Add(new AimedEnemyProjectile(projectileSprite, aimedProjectileSpeed, new Rectangle(position.X - 32, screenMiddleY + 208, 32, 32), game.Player.Position));
                game.Projectiles.Add(new AimedEnemyProjectile(projectileSprite, aimedProjectileSpeed, new Rectangle(position.X - 32, screenMiddleY - 240, 32, 32), game.Player.Position));
            }
            if (timer % 360 == 180)
            {
                game.Projectiles.Add(new AimedEnemyProjectile(projectileSprite, aimedProjectileSpeed, new Rectangle(position.X - 32, screenMiddleY + 208, 32, 32), game.Player.Position));
                game.Projectiles.Add(new AimedEnemyProjectile(projectileSprite, aimedProjectileSpeed, new Rectangle(position.X - 32, screenMiddleY - 240, 32, 32), game.Player.Position));
                game.Projectiles.Add(new HomingProjectile(projectileSprite, homingProjectileSpeed, new Rectangle(position.X - 32, screenMiddleY - 16, 32, 32), 180));
            }
            if (timer % 360 == 220)
            {
                game.Projectiles.Add(new AimedEnemyProjectile(projectileSprite, aimedProjectileSpeed, new Rectangle(position.X - 32, screenMiddleY + 208, 32, 32), game.Player.Position));
                game.Projectiles.Add(new AimedEnemyProjectile(projectileSprite, aimedProjectileSpeed, new Rectangle(position.X - 32, screenMiddleY - 240, 32, 32), game.Player.Position));
            }

        }

        //Moves the boss quickly to the left in a charge attack, and then slowly moves it back to the right
        public void ShotPatternTwo(int timer, Game1 game)
        {
            if (timer % 360 <= 40 && position.X > 0 - position.Width)
            {
                position.X -= 24;
            }
            else if (timer % 360 > 40 && position.X < game.GraphicsDevice.Viewport.Width - position.Width / 2)
            {
                position.X += 3;
            }
        }

        //Regularly shoots a right to left projectile in front of the boss, while also spawning 3 sets of 2 homing projectiles from the right corners of 
        //the screen
        public void ShotPatternThree(int timer, Game1 game)
        {
            if (timer % 40 == 39)
            {
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, position.Y + position.Height / 2 - 16, 32, 32)));
            }
            if (timer % 360 == 59 || timer % 360 == 119 || timer % 360 == 179)
            {
                game.Projectiles.Add(new HomingProjectile(projectileSprite, homingProjectileSpeed, new Rectangle(position.X - 32, screenMiddleY - 148, 32, 32), 180));
                game.Projectiles.Add(new HomingProjectile(projectileSprite, homingProjectileSpeed, new Rectangle(position.X - 32, screenMiddleY + 116, 32, 32), 180));
            }
        }

        //Creates two lasers, and updates them
        public void ShotPatternFour(int timer, Game1 game)
        {
            if(timer % 360 == 10)
            {
                laser = new LaserBeam(laserSprite, new Rectangle(0, position.Y - position.Y / 2, game.ViewportWidth, position.Height / 2), 1);
            }
            else if(timer % 360 >= 10 && timer % 360 < 140)
            {
                laser.UpdateLaser(position.Y, game.Player);
            }
            else if (timer % 360 == 150)
            {
                laser = new LaserBeam(laserSprite, new Rectangle(0, position.Y - position.Y / 2, game.ViewportWidth, position.Height / 2), 1);
            }
            else if (timer % 360 >= 150 && timer % 360 < 280)
            {
                laser.UpdateLaser(position.Y, game.Player);
            }
            else
            {
                laser = null;
            }
        }

        //Draws the laser, needed since the laser has no reference in Game1(I think)
        public override void DrawLaser(SpriteBatch sb)
        {
            if(laser != null)
            {
                laser.Draw(sb);
            }
        }
    }
}
