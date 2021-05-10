using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    //Defines an enemy which shoots a bullet which is aimed at the player but travels in a straight line
    class AimingEnemy : Enemy
    {
        //Fields
        private int framesToFire;
        private int projectileSpeed;
        private Texture2D projectileSprite;
        private int shootingTimer;

        //Properties

        //Get property for framesToFire
        public int FramesToFire
        {
            get { return framesToFire; }
        }

        //Get property for shootingTimer
        public int ShootingTimer
        {
            get { return shootingTimer; }
        }

        //BaseEnemy Constructor, uses Enemy constructor
        public AimingEnemy(Texture2D sprite, Rectangle position, int speed, int framesToFire, Texture2D projectileSprite, Rectangle hitBox)
            : base(sprite, position, speed, hitBox)
        {
            this.framesToFire = framesToFire;
            this.projectileSprite = projectileSprite;
            projectileSpeed = -16;
            health = 1;
            shootingTimer = 0;
            contactDamage = 1;
        }

        //Methods

        //Checks if the enemy's shooting timer is great enough for it to be able to shoot
        public bool CanShoot()
        {
            if (shootingTimer >= framesToFire)
            {
                return true;
            }
            return false;
        }

        //Creates an enemy projectile and returns it
        public AimedEnemyProjectile Shoot(Player player)
        {
            return new AimedEnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, position.Y + position.Height / 2 - 16, 32, 32), player.Position);
        }

        //Updates the enemy
        public override void Update(GameTime gameTime, Game1 game)
        {
            //Moves the enemy, then checks the enemy position to see if it needs to change the speed of the enemy, and moves the hitBox to the position.
            if (position.X <= game.ViewportWidth)
            {
                position.X -= speed;
            }
            else
            {
                position.X -= 4;
            }
            hitBox.X = position.X + 2;
            hitBox.Y = position.Y + 8;
            if (position.X <= game.GraphicsDevice.Viewport.Width * 3 / 4)
            {
                speed = 0;
            }
            //Increments the shooting timer, checks if the enemy can shoot, and shoots a projectile if it can.
            if (position.X <= game.ViewportWidth + position.Width)
            {
                shootingTimer++;
            }
            if (CanShoot())
            {
                shootingTimer = 0;
                game.Projectiles.Add(Shoot(game.Player));
            }
            //Increments a hit timer if it was hit, so that it becomes invisible for 5 frames to indicate being hit
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
    }
}
