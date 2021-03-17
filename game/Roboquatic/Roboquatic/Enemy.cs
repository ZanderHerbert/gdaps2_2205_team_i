using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    //Parent class to all future enemy classes.
    public class Enemy
    {
        //Decalring fields
        protected Texture2D sprite;
        protected Rectangle position;
        protected int speed;
        protected int health;

        //Get property for health
        public int Health
        {
            get { return health; }
        }

        //Get property for sprite
        public Texture2D Sprite
        {
            get { return sprite; }
        }

        //Get property for position
        public Rectangle Position
        {
            get { return position; }
        }

        //Enemy class constructor
        public Enemy(Texture2D sprite, Rectangle position, int speed)
        {
            this.sprite = sprite;
            this.speed = speed;
            this.position = position;
        }

        //Method which reduces the health of the enemy based on a passed in damage value
        //Method is virtual in case some enemies take damage in different ways
        public virtual void TakeDamage(int damage)
        {
            health -= damage;
        }

        public virtual void Update(GameTime gametime, Game1 game)
        {

        }
    }
}
