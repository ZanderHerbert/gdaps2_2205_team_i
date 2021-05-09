using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    //Enemy projectile which only moves left
    public class EnemyProjectile : Projectile
    {
        //EnemyProjectile constructor, uses its parent Projectile constructor
        public EnemyProjectile(Texture2D sprite, int speed, Rectangle position)
            : base(sprite, speed, position)
        {
            damage = 1;
        }

        //Checks if the projectile is in contact with the player
        public bool PlayerContact(Player player)
        {
            if(position.Intersects(player.HitBox))
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
            position.X += speed;
        }
    }
}
