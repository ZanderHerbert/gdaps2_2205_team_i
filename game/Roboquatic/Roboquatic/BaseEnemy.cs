using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    class BaseEnemy : Enemy
    {
        private int framesToFire;
        private int projectileSpeed;
        private Texture2D projectileSprite;

        public int FramesToFire
        {
            get { return framesToFire; }
        }
        public BaseEnemy(Texture2D sprite, Rectangle position, int speed, int framesToFire, Texture2D projectileSprite)
            : base(sprite, position, speed)
        {
            this.framesToFire = framesToFire;
            this.projectileSprite = projectileSprite;
            projectileSpeed = -8;
            health = 2;
        }
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
        public override bool CanShoot()
        {
            if (shootingTimer >= framesToFire)
            {
                return true;
            }
            return false;
        }
        public override EnemyProjectile Shoot()
        {
            return new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - position.Width, position.Y, 32, 32));
        }
    }
}
