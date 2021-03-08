using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    //Basic class which contains the basic information and methods a projectile needs
    class Projectile
    {
        // Declaring fields
        protected Texture2D sprite;
        protected int speed;
        protected Rectangle position;
        protected int damage;

        //Get property for damage
        public int Damage
        {
            get { return damage; }
        }

        // Get property for sprite
        public Texture2D Sprite
        {
            get { return sprite; }
        }

        // Get property for position
        public Rectangle Position
        {
            get { return position; }
        }

        // Projectile constructor
        public Projectile(Texture2D sprite, int speed, Rectangle position)
        {
            this.sprite = sprite;
            this.speed = speed;
            this.position = position;
        }

        //Moves the projectile
        public void Move()
        {
            position.X += speed;
        }

        //Virtual method to be overriden by an enemy projectile
        public virtual bool PlayerContact(Player player)
        {
            return false;
        }

        //Virtual method to be overriden by a player projectile
        public virtual Enemy EnemyContact(List<Enemy> enemies)
        {
            return null;
        }
    }
}
