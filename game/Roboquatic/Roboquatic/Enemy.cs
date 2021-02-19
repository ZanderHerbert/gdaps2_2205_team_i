using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    class Enemy
    {
        protected Texture2D sprite;
        protected Rectangle position;
        protected int speed;
        protected int shootingTimer;
        protected int health;

        public int Health
        {
            get { return health; }
        }
        public Texture2D Sprite
        {
            get { return sprite; }
        }
        public Rectangle Position
        {
            get { return position; }
        }
        public int ShootingTimer
        {
            get { return shootingTimer; }
            set { shootingTimer = value; }
        }
        public Enemy(Texture2D sprite, Rectangle position, int speed)
        {
            this.sprite = sprite;
            this.speed = speed;
            this.position = position;
            shootingTimer = 0;
        }
        public virtual void Move(Game1 game)
        {
            position.X -= speed;
        }
        public virtual bool CanShoot()
        {
            return false;
        }
        public virtual EnemyProjectile Shoot()
        {
            return null;
        }
        public virtual void TakeDamage(int damage)
        {
            health -= damage;
        }
    }
}
