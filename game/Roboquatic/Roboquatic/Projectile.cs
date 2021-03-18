using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    //Basic class which contains the basic information and methods a projectile needs
    public class Projectile
    {
        // Declaring fields
        protected Texture2D sprite;
        protected int speed;
        protected Rectangle position;
        protected int damage;
        protected bool hit;
        protected double angle;

        public double Angle
        {
            get { return angle; }
        }

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
        
        //Get propert for hit
        public bool Hit
        {
            get { return hit; }
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
            hit = false;
        }

        //Virtual update method to be used by child classes
        public virtual void Update(GameTime gameTime, Game1 game)
        {

        }
    }
}
