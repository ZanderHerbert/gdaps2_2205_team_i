using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    //A "laser" or long horizontal rectangle which flashes, until it expands, at which point it deals contact damage to the player
    class LaserBeam
    {
        //Fields
        private int timer;
        private Rectangle position;
        private Texture2D sprite;
        private int damage;
        private bool draw;

        //Constructor
        public LaserBeam(Texture2D sprite, Rectangle position, int damage)
        {
            this.sprite = sprite;
            this.position = position;
            this.damage = damage;
            timer = 0;
            draw = false;
        }

        //Methods

        //Updates the laser
        public void UpdateLaser(int bossY, Player player)
        {
            //Increments timer
            timer++;

            //Updates the position of the laser
            if(timer == 110)
            {
                position = new Rectangle(position.X, position.Y - position.Height / 2, position.Width, position.Height * 2);
            }
            else if (timer < 110)
            {
                //Sets draw to true and false to have a flashing effect
                position.Y = bossY + position.Height / 2;
                if (timer % 10 <= 5)
                {
                    draw = true;
                }
                else
                {
                    draw = false;
                }
            }
            else if(timer > 110 && timer < 130)
            {
                //Checks for collision at end of life
                draw = true;
                position.Y = bossY;
                if (position.Intersects(player.Position))
                {
                    player.TakeDamage(damage);
                }
            }
        }

        //Draws the laser
        public void Draw(SpriteBatch sb)
        {
            if (draw)
            {
                sb.Draw(sprite, position, Color.Red);
            }
        }
    }
}
