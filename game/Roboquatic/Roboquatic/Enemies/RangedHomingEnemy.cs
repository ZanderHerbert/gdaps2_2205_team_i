using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    class RangedHomingEnemy : Enemy
    {
        //Declaring fields
        private int framesToFire;
        private int projectileSpeed;
        private Texture2D projectileSprite;
        private int shootingTimer;

        //Get property for framesToFire
        public int FramesToFire
        {
            get { return framesToFire; }
        }

        public int ShootingTimer
        {
            get { return shootingTimer; }
        }

        //BaseEnemy Constructor, uses Enemy constructor
        public RangedHomingEnemy(Texture2D sprite, Rectangle position, int speed, int framesToFire, Texture2D projectileSprite)
            : base(sprite, position, speed)
        {
            this.framesToFire = framesToFire;
            this.projectileSprite = projectileSprite;
            projectileSpeed = 6;
            health = 1;
            shootingTimer = 0;
            contactDamage = 1;
        }

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
        public HomingProjectile Shoot()
        {
            return new HomingProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, position.Y + position.Height / 2 - 16, 32, 32), 240);
        }

        //Updates the enemy
        //
        //Moves the enemy, then checks the enemy position to see if it needs to change the speed of the enemy,
        //increments the shooting timer, checks if the enemy can shoot, and shoots a projectile if it can.
        public override void Update(GameTime gameTime, Game1 game)
        {
            position.X -= speed;
            if (position.X <= game.GraphicsDevice.Viewport.Width * 5 / 8)
            {
                speed = 0;
            }
            shootingTimer++;
            if (CanShoot())
            {
                shootingTimer = 0;
                game.Projectiles.Add(Shoot());
            }
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
