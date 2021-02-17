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

        //Moves the player in the direction of the mouse
        public void Move(int x, int y)
        {
            //Variables which hold the difference in position between the midpoint of the player and the mouse
            double deltaX = (position.X + position.Width/2) - x;
            double deltaY = (position.Y + position.Height/2) - y;

            //Moves the player a set distance in the direction of the mouse.
            //
            //The first if is if the player is within a "speed" radius of the mouse, in which case it will go directly
            //to the mouse, otherwise the player would flicker back and forth on either side of the mouse.
            //
            //The second if is so that there are no divide by zero errors, and the code in there makes the player
            //object change it's position to basically the closest position to the mouse within a radius of "speed"
            //pixels(I believe it measures it in pixels)
            //
            //NOTE: The speed needs to be balanced between keyboard and mouse controls still if we plan on
            //      implementing both.
            if(Math.Sqrt((deltaX * deltaX + deltaY * deltaY)) <= speed * 10)
            {
                position.X = x - position.Width/2;
                position.Y = y - position.Height/2;
            }
            else if ((deltaX) + (deltaY) != 0)
            {
                position.X -= (int)(((deltaX) * speed * 10) / ((Math.Abs(deltaX)) + (Math.Abs(deltaY))));
                position.Y -= (int)(((deltaY) * speed * 10) / ((Math.Abs(deltaX)) + (Math.Abs(deltaY))));
            }
        }
    }
}
