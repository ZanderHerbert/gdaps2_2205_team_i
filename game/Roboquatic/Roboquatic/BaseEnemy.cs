using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    //The most basic enemy which moves left to right and shoots
    class BaseEnemy : Enemy
    {
        //Declaring fields
        private int framesToFire;
        private int projectileSpeed;
        private Texture2D projectileSprite;

        //Get property for framesToFire
        public int FramesToFire
        {
            get { return framesToFire; }
        }

        //BaseEnemy Constructor, uses Enemy constructor
        public BaseEnemy(Texture2D sprite, Rectangle position, int speed, int framesToFire, Texture2D projectileSprite)
            : base(sprite, position, speed)
        {
            this.framesToFire = framesToFire;
            this.projectileSprite = projectileSprite;
            projectileSpeed = -8;
            health = 2;
        }

        //Moves the enemy back and forth from left to right
        public override void Move(Game1 game)
        {
            position.X -= speed;
            if (position.X <= game.GraphicsDevice.Viewport.Width/2)
            {
                speed = -1;
            }
            if(position.X >= game.GraphicsDevice.Viewport.Width*3/4 && speed < 0)
            {
                speed = 1;
            }
        }

        //Checks if the enemy's shooting timer is great enough for it to be able to shoot
        public override bool CanShoot()
        {
            if (shootingTimer >= framesToFire)
            {
                return true;
            }
            return false;
        }

        //Creates an enemy projectile and returns it
        public override EnemyProjectile Shoot()
        {
            return new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - position.Width, position.Y, 32, 32));
        }
    }
}
