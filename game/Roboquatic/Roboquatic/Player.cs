using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    //The player class is the class that is used to create and control the player object
    class Player
    {
        //Declaring Fields
        private int speed;
        private Rectangle position;
        private Texture2D sprite;

        //Get and set properties for speed (as speed may change due to upgrades)
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        //Get and set for the sprite (Unsure if I should store the sprite within the class or not)
        public Texture2D Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }

        //Get property for the Rectangle position
        public Rectangle Position
        {
            get { return position; }
        }

        //Player Constructor
        public Player(int speed, Rectangle position)
        {
            this.speed = speed;
            this.position = position;
        }

        //Moves the player up
        public void MoveUp()
        {
            position.Y = position.Y - speed * 2;
        }

        //Moves the player down
        public void MoveDown()
        {
            position.Y = position.Y + speed * 2;
        }

        //Moves the player left
        public void MoveLeft()
        {
            position.X = position.X - speed * 2;
        }

        //Moves the player right
        public void MoveRight()
        {
            position.X = position.X + speed * 2;
        }
    }
}
