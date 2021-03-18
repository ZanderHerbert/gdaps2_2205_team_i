using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    class HomingProjectile : Projectile
    {
        private double deltaX;
        private double deltaY;
        private int xChange;
        private int prevXChange;
        private int scndPrevXChange;
        private int yChange;
        private int prevYChange;
        private int scndPrevYChange;

        //EnemyProjectile constructor, uses its parent Projectile constructor
        public HomingProjectile(Texture2D sprite, int speed, Rectangle position)
            : base(sprite, speed, position)
        {
            damage = 1;

            deltaX = 0;
            deltaY = 0;
            xChange = 0;
            yChange = 0;
            prevXChange = 0;
            prevYChange = 0;
            scndPrevXChange = 0;
            angle = Math.Acos((((deltaX)) / ((Math.Abs(deltaX)) + (Math.Abs(deltaY)))));
        }

        //Checks if the projectile is in contact with the player
        public bool PlayerContact(Player player)
        {
            if (position.Intersects(player.Position))
            {
                return true;
            }
            return false;
        }

        //Updates the projectile.
        //
        //Checks if the projectile is in contact with the player, and if so, damages the player and then sets the hit
        //variable to true to denote that it made contact, then moves the projectile.
        public override void Update(GameTime gameTime, Game1 game)
        {
            Rectangle playerPos = game.Player.Position;

            
            deltaX = (position.X + position.Width / 2) - playerPos.X;
            deltaY = (position.Y + position.Height / 2) - playerPos.Y;
            scndPrevXChange = prevXChange;
            prevXChange = xChange;
            xChange = (int)(((deltaX) * speed) / ((Math.Abs(deltaX)) + (Math.Abs(deltaY))));
            yChange = (int)(((deltaY) * speed) / ((Math.Abs(deltaX)) + (Math.Abs(deltaY))));

            if (PlayerContact(game.Player))
            {
                game.Player.TakeDamage(damage);
                hit = true;
            }
            position.X += xChange + prevXChange / 2 + scndPrevXChange / 4;
            position.Y += yChange + prevYChange / 2 + scndPrevYChange / 4;

            
        }
    }
}
