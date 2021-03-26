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
        private int yChange;
        private double xVelocity;
        private double yVelocity;
        private double xAcceleration;
        private double yAcceleration;
        private int lifeSpan;

        //EnemyProjectile constructor, uses its parent Projectile constructor
        public HomingProjectile(Texture2D sprite, int speed, Rectangle position, int lifeSpan)
            : base(sprite, speed, position)
        {
            damage = 1;

            this.lifeSpan = lifeSpan;
            xChange = 0;
            yChange = 0;
            xVelocity = 0;
            yVelocity = 0;
            xAcceleration = 0;
            yAcceleration = 0;
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

            
            deltaX = (position.X + position.Width / 2) - (playerPos.X + playerPos.Width / 2);
            deltaY = (position.Y + position.Height / 2) - (playerPos.Y + playerPos.Height / 2);
            xAcceleration = ((deltaX)) / ((Math.Abs(deltaX)) + (Math.Abs(deltaY)));
            yAcceleration = ((deltaY)) / ((Math.Abs(deltaX)) + (Math.Abs(deltaY)));
            xVelocity = (xVelocity + xAcceleration / 3);
            yVelocity = (yVelocity + yAcceleration / 3);
            if(Math.Abs(xVelocity) + Math.Abs(yVelocity) > speed)
            {
                xVelocity = (xVelocity / (Math.Abs(xVelocity) + Math.Abs(yVelocity))) * (double)speed;
                yVelocity = (yVelocity / (Math.Abs(xVelocity) + Math.Abs(yVelocity))) * (double)speed;
            }
            xChange = (int)(xVelocity);
            yChange = (int)(yVelocity);

            if (PlayerContact(game.Player))
            {
                game.Player.TakeDamage(damage);
                hit = true;
            }

            if(lifeSpan <= 0)
            {
                hit = true;
            }

            position.X -= xChange;
            position.Y -= yChange;
            lifeSpan--;
        }
    }
}
