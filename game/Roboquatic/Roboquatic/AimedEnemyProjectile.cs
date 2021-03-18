using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    class AimedEnemyProjectile : Projectile
    {
        private double deltaX;
        private double deltaY;
        private int xChange;
        private int yChange;

        //EnemyProjectile constructor, uses its parent Projectile constructor
        public AimedEnemyProjectile(Texture2D sprite, int speed, Rectangle position, Rectangle playerPosition)
            : base(sprite, speed, position)
        {
            damage = 1;

            //Sets values that will be used to determine how the projectile moves
            deltaX = (position.X + position.Width / 2) - playerPosition.X;
            deltaY = (position.Y + position.Height / 2) - playerPosition.Y;
            xChange = (int)(((deltaX) * speed) / ((Math.Abs(deltaX)) + (Math.Abs(deltaY))));
            yChange = (int)(((deltaY) * speed) / ((Math.Abs(deltaX)) + (Math.Abs(deltaY))));
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
            if (PlayerContact(game.Player))
            {
                game.Player.TakeDamage(damage);
                hit = true;
            }
            position.X += xChange;
            position.Y += yChange;
            
        }
    }
}
