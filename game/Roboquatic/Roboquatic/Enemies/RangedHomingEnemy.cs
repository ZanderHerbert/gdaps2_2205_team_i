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
        public RangedHomingEnemy(Texture2D sprite, Rectangle position, int speed, int framesToFire, Texture2D projectileSprite, Rectangle hitBox)
            : base(sprite, position, speed, hitBox)
        {
            this.framesToFire = framesToFire;
            this.projectileSprite = projectileSprite;
            projectileSpeed = 6;
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
        public HomingProjectile Shoot()
        {
            return new HomingProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - (int)(GlobalScalars.x * 32), position.Y + position.Height / 2 - (int)(GlobalScalars.y * 16), 32, 32), 240);
        }

        //Updates the enemy
        public override void Update(GameTime gameTime, Game1 game)
        {
            //Moves the enemy, then checks the enemy position to see if it needs to change the speed of the enemy, and moves the hitBox to the position.
            if (position.X <= game.ViewportWidth)
            {
                floatPos.X -= speed;
            }
            else
            {
                floatPos.X -= 4 * GlobalScalars.x;
            }
            position.X = (int)floatPos.X;
            position.Y = (int)floatPos.Y;
            hitBox.X = position.X + (int)(2 * GlobalScalars.x);
            hitBox.Y = position.Y + (int)(14 * GlobalScalars.y);
            if (position.X <= game.ViewportWidth * 5 / 8)
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
                game.Projectiles.Add(Shoot());
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
