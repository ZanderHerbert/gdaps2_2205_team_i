using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    class Player
    {
        private int speed;
        private Rectangle position;
        private Texture2D sprite;

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public Texture2D Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }
        public int PositionX
        {
            get { return position.X; }
            set { position.X = value; }
        }
        public int PositionY
        {
            get { return position.Y; }
            set { position.Y = value; }
        }
        public Rectangle Position
        {
            get { return position; }
        }

        public Player(int speed, Rectangle position)
        {
            this.speed = speed;
            this.position = position;
        }

        public void MoveUp()
        {
            position.Y = position.Y - speed * 2;
        }
        public void MoveDown()
        {
            position.Y = position.Y + speed * 2;
        }
        public void MoveLeft()
        {
            position.X = position.X - speed * 2;
        }
        public void MoveRight()
        {
            position.X = position.X + speed * 2;
        }
    }
}
