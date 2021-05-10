using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    class LaserBeam
    {
        private int timer;
        private Rectangle position;
        private Texture2D sprite;
        private int damage;
        private bool draw;

        public LaserBeam(Texture2D sprite, Rectangle position, int damage)
        {
            this.sprite = sprite;
            this.position = position;
            this.damage = damage;
            timer = 0;
            draw = false;
        }

        public void UpdateLaser(int bossY, Player player)
        {
            timer++;
            if(timer == 110)
            {
                position = new Rectangle(position.X, position.Y - position.Height / 2, position.Width, position.Height * 2);
            }
            else if (timer < 110)
            {
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
                draw = true;
                position.Y = bossY;
                if (position.Intersects(player.Position))
                {
                    player.TakeDamage(damage);
                }
            }
        }
        public void Draw(SpriteBatch sb)
        {
            if (draw)
            {
                sb.Draw(sprite, position, Color.Red);
            }
        }
    }
}
