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
        //Fields
        protected Texture2D sprite;
        protected Rectangle position;
        protected int speed;
        protected int health;
        protected int contactDamage;
        protected bool hit;
        protected int hitTimer;
        protected Rectangle hitBox;

        //Properties

        //Get property for contactDamage
        public int ContactDamage
        {
            get { return contactDamage; }
        }

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

        // Get property for hitBox
        public Rectangle HitBox
        {
            get { return hitBox; }
        }

        //Enemy class constructor
        public Enemy(Texture2D sprite, Rectangle position, int speed, Rectangle hitBox)
        {
            this.sprite = sprite;
            this.speed = speed;
            this.position = position;
            this.hitBox = hitBox;
            hit = false;
        }

        //Method which reduces the health of the enemy based on a passed in damage value
        //Method is virtual in case some enemies take damage in different ways
        public virtual void TakeDamage(int damage)
        {
            health -= damage;
            hit = true;
        }

        //Virtual method for update for all enemies to use
        public virtual void Update(GameTime gametime, Game1 game)
        {

        }

        //Draw method which draws the enemy if their hit bool is false
        public void Draw(SpriteBatch _spriteBatch)
        {
            if (!hit)
            {
                _spriteBatch.Draw(sprite, position, null, Color.White, 0, new Vector2(), SpriteEffects.FlipHorizontally, 0);
            }
        }

        //Draw laser virtual method for the Boss to use, since it is in a list of enemies when draw functions are called on it
        public virtual void DrawLaser(SpriteBatch sb)
        {

        }
    }
}
